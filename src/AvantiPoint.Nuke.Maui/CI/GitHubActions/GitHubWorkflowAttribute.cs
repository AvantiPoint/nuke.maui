using AvantiPoint.Nuke.Maui.CI.Configuration;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.GitHubActions;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GitHubWorkflowAttribute : CIBuildAttribute
{
    public GitHubWorkflowAttribute(Type type)
        : base(type)
    {
    }

    public override Type HostType => typeof(global::Nuke.Common.CI.GitHubActions.GitHubActions);
    public override string ConfigurationFile => NukeBuild.RootDirectory / ".github" / "workflows" / $"{_name}.yml";
    public override IEnumerable<string> GeneratedFiles => new[] { ConfigurationFile };

    protected override ConfigurationEntity BuildConfiguration(NukeBuild build, IEnumerable<ExecutableTarget> relevantTargets)
    {
        var configuration = new GitHubActionsConfiguration
        {
            Name = _name,
            ShortTriggers = Array.Empty<GitHubActionsTrigger>(),
            DetailedTriggers = GetTriggers().ToArray(),
            Jobs = GetJobs(relevantTargets).ToArray()
        };

        return configuration;
    }

    private IEnumerable<GitHubActionsDetailedTrigger> GetTriggers()
    {
        if (Build.OnPush is not null && Build.OnPush.Branches.Any())
        {
            yield return new GitHubActionsVcsTrigger
            {
                Kind = GitHubActionsTrigger.Push,
                Branches = Build.OnPush.Branches.ToArray(),
                BranchesIgnore = Build.OnPush.BranchesIgnore.ToArray(),
                Tags = Build.OnPush.Tags.ToArray(),
                TagsIgnore = Build.OnPush.TagsIgnore.ToArray(),
                IncludePaths = Build.OnPush.IncludePaths.ToArray(),
                ExcludePaths = Build.OnPush.ExcludePaths.ToArray()
            };
        }

        if (Build.OnPull is not null && Build.OnPull.Branches.Any())
        {
            yield return new GitHubActionsVcsTrigger
            {
                Kind = GitHubActionsTrigger.PullRequest,
                Branches = Build.OnPull.Branches.ToArray(),
                BranchesIgnore = Array.Empty<string>(),
                Tags = Array.Empty<string>(),
                TagsIgnore = Array.Empty<string>(),
                IncludePaths = Build.OnPull.IncludePaths.ToArray(),
                ExcludePaths = Build.OnPull.ExcludePaths.ToArray()
            };
        }

        if (Build.ManualTrigger is not null)
        {
            yield return new GitHubActionsWorkflowDispatchTrigger
            {
                OptionalInputs = Build.ManualTrigger.OptionalInputs.ToArray(),
                RequiredInputs = Build.ManualTrigger.RequiredInputs.ToArray()
            };
        }

        if (!string.IsNullOrEmpty(Build.OnCronSchedule))
        {
            yield return new GitHubActionsScheduledTrigger { Cron = Build.OnCronSchedule };
        }
    }

    private IEnumerable<GitHubActionsJob> GetJobs(IEnumerable<ExecutableTarget> relevantTargets)
    {
        var previousStage = Array.Empty<string>();
        foreach(var stage in Build.Stages)
        {
            foreach(var job in stage.Jobs)
            {
                var needs = new List<string>(previousStage);
                if(job.Needs.Any())
                    needs.AddRange(job.Needs);

                yield return new GitHubWorkflowJob
                {
                    Job = job,
                    Needs = needs.ToArray(),
                    Steps = GetSteps(job, relevantTargets).ToArray()
                };
            }

            previousStage = stage.Jobs.Select(x => x.JobName()).ToArray();
        }
    }

    private IEnumerable<GitHubActionsStep> GetSteps(ICIJob job, IEnumerable<ExecutableTarget> relevantTargets)
    {
        yield return new GitHubActionsCheckoutStep
        {
            Submodules = Build.Submodules switch
            {
                CheckoutSubmodules.Recursive => GitHubActionsSubmodules.Recursive,
                CheckoutSubmodules.True => GitHubActionsSubmodules.True,
                _ => GitHubActionsSubmodules.False
            },
            FetchDepth = (uint)Build.FetchDepth
        };

        if (job.DownloadArtifacts.Any())
        {
            foreach (var artifact in job.DownloadArtifacts)
            {
                yield return new GitHubActionsDownloadArtifactStep
                {
                    ArtifactName = artifact
                };
            }
        }

        yield return new GitHubActionsUseDotNetVersionStep
        {
            Sdks = job.DotNetSdks.ToArray()
        };

        if (job.CacheIncludePatterns.Any())
        {
            yield return new GitHubActionsCacheStepV3
            {
                JobName = job.JobName(),
                IncludePatterns = job.CacheIncludePatterns.ToArray(),
                ExcludePatterns = job.CacheExcludePatterns.ToArray(),
                KeyFiles = job.CacheKeyFiles.ToArray()
            };
        }

        yield return new GitHubActionsRunStep
        {
            Command = job.GetRunCommand(BuildCmdPath),
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
                yield return new GitHubActionsUploadArtifactV3
                {
                    Name = name,
                    Path = NukeBuild.RootDirectory.GetUnixRelativePathTo(artifacts[0])
                };
            }
            else if (artifacts.Count > 1)
            {
                foreach (var artifact in artifacts)
                {
                    if (artifact is null) continue;

                    yield return new GitHubActionsUploadArtifactV3
                    {
                        Name = artifact.ToString().TrimStart(artifact.Parent.ToString()).TrimStart('/', '\\'),
                        Path = NukeBuild.RootDirectory.GetUnixRelativePathTo(artifact)
                    };
                }
            }
        }
    }

    protected virtual IEnumerable<(string Key, string Value)> GetImports(ICIJob job)
    {
        if(Build.ManualTrigger is not null)
        {
            foreach (var input in Build.ManualTrigger.OptionalInputs.Concat(Build.ManualTrigger.RequiredInputs))
                yield return (input, $"${{{{ github.event.inputs.{input} }}}}");
        }

        static string GetSecretValue(string value) => $"${{{{ secrets.{value} }}}}";

        foreach (var secret in job.ImportSecrets.OfType<WorkflowSecret>())
            yield return (secret.Name, GetSecretValue(secret.Secret));

        if (Build.EnableToken)
            yield return ("GITHUB_TOKEN", GetSecretValue("GITHUB_TOKEN"));
    }
}
