using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;
using AvantiPoint.Nuke.Maui.CI.Configuration;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class AzurePipelinesAttribute : CIBuildAttribute
{
    public AzurePipelinesAttribute(Type type)
        : base(type)
    {
    }

    public bool IsDefault { get; set; } = true;

    public bool Batch { get; set; }

    public bool TriggerDisabled { get; set; }

    public bool IncludeLargeFileStorage { get; set; }

    public override string IdPostfix => IsDefault ? string.Empty : base.IdPostfix;

    public override Type HostType => typeof(global::Nuke.Common.CI.AzurePipelines.AzurePipelines);
    public override string ConfigurationFile => NukeBuild.RootDirectory / ConfigurationFileName;
    public override IEnumerable<string> GeneratedFiles => new[] { ConfigurationFile };

    private string ConfigurationFileName => !IsDefault ? $"azure-pipelines.{IdPostfix}.yml" : "azure-pipelines.yml";

    protected override ConfigurationEntity BuildConfiguration(NukeBuild build, IEnumerable<ExecutableTarget> relevantTargets)
    {
        var stages = Build.Stages.ToList();
        return new AzurePipelinesConfiguration
        {
            Stages = stages.Select(x => new AzurePipelinesStage
            {
                DisplayName = x.DisplayName(stages.IndexOf(x)),
                Name = x.StageName(stages.IndexOf(x)),
                Environment = x.Environment,
                Jobs = GetJobs(x, relevantTargets)
            }),
            Trigger = new AzurePipelinesTrigger
            {
                Build = Build,
            },
            Variables = Build.Variables
        };
    }

    private IEnumerable<AzurePipelinesJob> GetJobs(ICIStage stage, IEnumerable<ExecutableTarget> relevantTargets)
    {
        return stage.Jobs.Select(x => new AzurePipelinesJob
        {
            Name = x.JobName(),
            DisplayName = x.DisplayName(),
            Environment = x.Environment,
            Steps = GetSteps(x, relevantTargets)
        });
    }

    private IEnumerable<AzurePipelinesStep> GetSteps(ICIJob job, IEnumerable<ExecutableTarget> relevantTargets)
    {
        yield return new AzurePipelinesCheckoutStep
        {
            Clean = Build.Clean,
            FetchDepth = Build.FetchDepth,
            IncludeLargeFileStorage = IncludeLargeFileStorage,
            InclueSubmodules = Build.Submodules
        };

        if(job.DownloadArtifacts.Any())
        {
            foreach (var artifact in job.DownloadArtifacts)
                yield return new AzurePipelinesDownloadStep
                {
                    ArtifactName = artifact,
                    DownloadPath = $"artifacts/{artifact}"
                };
        }

        if(job.CacheKeyFiles.Any())
        {
            foreach (var cachePath in job.CacheIncludePatterns)
                yield return new AzurePipelinesCacheStep
                {
                    Agent = job.Image,
                    KeyFiles = job.CacheKeyFiles,
                    Path = cachePath
                };
        }

        yield return new AzurePipelinesScriptStep
        {
            Run = job.GetRunCommand(BuildCmdPath),
            Imports = GetImports(job).ToDictionary(x => x.Key, x => x.Value)
        };

        if (job.PublishArtifacts)
        {
            var artifacts = job.GetArtifacts(relevantTargets).ToList();

            if (artifacts.Count == 1)
            {
                var name = string.IsNullOrEmpty(job.ArtifactName)
                    ? artifacts[0]!.ToString().TrimStart(artifacts[0]!.Parent.ToString()).TrimStart('/', '\\')
                    : job.ArtifactName;
                yield return new AzurePipelinesPublishStep
                {
                    ArtifactName = name,
                    PathToPublish = NukeBuild.RootDirectory.GetUnixRelativePathTo(artifacts[0])
                };
            }
            else if (artifacts.Count > 1)
            {
                foreach (var artifact in artifacts)
                {
                    if (artifact is null) continue;

                    yield return new AzurePipelinesPublishStep
                    {
                        ArtifactName = artifact.ToString().TrimStart(artifact.Parent.ToString()).TrimStart('/', '\\'),
                        PathToPublish = NukeBuild.RootDirectory.GetUnixRelativePathTo(artifact)
                    };
                }
            }
        }
    }

    private IEnumerable<(string Key, string Value)> GetImports(ICIJob job)
    {
        static string GetSecretValue(string secret) => $"$({secret})";

        if (Build.EnableToken)
            yield return ("SYSTEM_ACCESSTOKEN", GetSecretValue("System.AccessToken"));

        foreach (var secret in job.ImportSecrets.OfType<WorkflowSecret>())
            yield return (secret.Name, GetSecretValue(secret.Secret));
    }
}
