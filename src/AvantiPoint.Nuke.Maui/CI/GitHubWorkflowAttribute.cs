using System.Reflection;
using System.Xml.Linq;
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

namespace AvantiPoint.Nuke.Maui.CI;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class GitHubWorkflowAttribute : ConfigurationAttributeBase
{
    private readonly string _name;
    private GitHubActionsSubmodules? _submodules;
    private uint? _fetchDepth;

    public GitHubWorkflowAttribute(string name)
    {
        _name = name.Replace(oldChar: ' ', newChar: '_');
    }

    public override string IdPostfix => _name;
    public override Type HostType => typeof(GitHubActions);
    public override string ConfigurationFile => NukeBuild.RootDirectory / ".github" / "workflows" / $"{_name}.yml";
    public override IEnumerable<string> GeneratedFiles => new[] { ConfigurationFile };

    public override IEnumerable<string> RelevantTargetNames => Jobs.SelectMany(x => x.InvokedTargets).Distinct();
    public override IEnumerable<string> IrrelevantTargetNames => Array.Empty<string>();
    public IEnumerable<WorkflowJobAttribute> Jobs => BuildType.GetCustomAttributes<WorkflowJobAttribute>();

    public string[] JobNames { get; set; } = Array.Empty<string>();
    public GitHubActionsTrigger[] On { get; set; } = Array.Empty<GitHubActionsTrigger>();
    public string[] OnPushBranches { get; set; } = Array.Empty<string>();
    public string[] OnPushBranchesIgnore { get; set; } = Array.Empty<string>();
    public string[] OnPushTags { get; set; } = Array.Empty<string>();
    public string[] OnPushTagsIgnore { get; set; } = Array.Empty<string>();
    public string[] OnPushIncludePaths { get; set; } = Array.Empty<string>();
    public string[] OnPushExcludePaths { get; set; } = Array.Empty<string>();
    public string[] OnPullRequestBranches { get; set; } = Array.Empty<string>();
    public string[] OnPullRequestTags { get; set; } = Array.Empty<string>();
    public string[] OnPullRequestIncludePaths { get; set; } = Array.Empty<string>();
    public string[] OnPullRequestExcludePaths { get; set; } = Array.Empty<string>();
    public string[] OnWorkflowDispatchOptionalInputs { get; set; } = Array.Empty<string>();
    public string[] OnWorkflowDispatchRequiredInputs { get; set; } = Array.Empty<string>();
    public string OnCronSchedule { get; set; } = "";
    public bool EnableGitHubToken { get; set; }

    public Type BuildType { get; set; } = typeof(NukeBuild);

    public GitHubActionsSubmodules Submodules
    {
        set => _submodules = value;
        get => throw new NotSupportedException();
    }

    public uint FetchDepth
    {
        set => _fetchDepth = value;
        get => throw new NotSupportedException();
    }


    public override CustomFileWriter CreateWriter(StreamWriter streamWriter)
    {
        return new CustomFileWriter(streamWriter, indentationFactor: 2, commentPrefix: "#");
    }

    public override ConfigurationEntity GetConfiguration(NukeBuild build, IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        var configuration = new GitHubActionsConfiguration
        {
            Name = _name,
            ShortTriggers = On,
            DetailedTriggers = GetTriggers().ToArray(),
            Jobs = GetJobs(build).ToArray()
        };

        Assert.True(configuration.ShortTriggers.Length == 0 || configuration.DetailedTriggers.Length == 0,
            $"Workflows can only define either shorthand '{nameof(On)}' or '{nameof(On)}*' triggers");
        Assert.True(configuration.ShortTriggers.Length > 0 || configuration.DetailedTriggers.Length > 0,
            $"Workflows must define either shorthand '{nameof(On)}' or '{nameof(On)}*' triggers");

        return configuration;
    }

    private IEnumerable<GitHubActionsJob> GetJobs(NukeBuild build)
    {
        var jobs = build.GetType().GetCustomAttributes<WorkflowJobAttribute>().ToArray();
        if (jobs.Length == 1 && string.IsNullOrEmpty(jobs[0].Name))
            jobs[0].Name = _name;
        else if (jobs.Length > 1 && jobs.Any(j => string.IsNullOrEmpty(j.Name)))
            throw new ArgumentNullException("The Job Name can not be null or empty");

        foreach (var jobDef in jobs)
        {
            if (!JobNames.Contains(jobDef.Name))
                continue;

            yield return new GitHubWorkflowJob
            {
                Image = jobDef.Image,
                Name = jobDef.Name,
                Needs = jobDef.Needs,
                Steps = GetSteps(jobDef, build.ExecutableTargets()).ToArray()
            };
        }
    }

    private IEnumerable<GitHubActionsStep> GetSteps(WorkflowJobAttribute job, IEnumerable<ExecutableTarget> targets)
    {
        yield return new GitHubActionsCheckoutStep
        {
            Submodules = _submodules,
            FetchDepth = _fetchDepth
        };

        if (job.DownloadArtifacts.Any())
        {
            foreach(var artifact in job.DownloadArtifacts)
            {
                yield return new GitHubActionsDownloadArtifactStep
                {
                    ArtifactName = artifact
                };
            }
        }

        yield return new GitHubActionsUseDotNetVersionStep
        {
            Sdks = job.DotNetSdks
        };

        if (job.CacheKeyFiles.Any())
        {
            yield return new GitHubActionsCacheStep
            {
                IncludePatterns = job.CacheIncludePatterns,
                ExcludePatterns = job.CacheExcludePatterns,
                KeyFiles = job.CacheKeyFiles
            };
        }

        var cmdPath = BuildCmdPath;
        if (!job.Image.ToString().StartsWith("Windows"))
            cmdPath = cmdPath.Replace(".cmd", ".sh");

        yield return new GitHubActionsRunStep
        {
            Command = $"./{cmdPath} {job.InvokedTargets.JoinSpace()}",
            Imports = GetImports(job).ToDictionary(x => x.Key, x => x.Value)
        };

        if (job.PublishArtifacts)
        {
            var artifacts = targets
                .Where(x => job.InvokedTargets.Any(name => name == x.Name))
                .SelectMany(x => x.ArtifactProducts)
                .Select(x => (AbsolutePath)x)
                // TODO: https://github.com/actions/upload-artifact/issues/11
                .Select(x => x.DescendantsAndSelf(y => y.Parent).FirstOrDefault(y => !y.ToString().ContainsOrdinalIgnoreCase("*")))
                .Distinct().ToList();

            if (artifacts.Count == 1)
            {
                var name = string.IsNullOrEmpty(job.ArtifactName)
                    ? artifacts[0]!.ToString().TrimStart(artifacts[0]!.Parent.ToString()).TrimStart('/', '\\')
                    : job.ArtifactName;
                yield return new GitHubActionsArtifactStep
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

                    yield return new GitHubActionsArtifactStep
                    {
                        Name = artifact.ToString().TrimStart(artifact.Parent.ToString()).TrimStart('/', '\\'),
                        Path = NukeBuild.RootDirectory.GetUnixRelativePathTo(artifact)
                    };
                }
            }
        }
    }

    protected virtual IEnumerable<(string Key, string Value)> GetImports(WorkflowJobAttribute job)
    {
        foreach (var input in OnWorkflowDispatchOptionalInputs.Concat(OnWorkflowDispatchRequiredInputs))
            yield return (input, $"${{{{ github.event.inputs.{input} }}}}");

        static string GetSecretValue(string value) => $"${{{{ secrets.{value} }}}}";

        foreach (WorkflowSecret secret in job.ImportSecrets)
            yield return (secret.Name, GetSecretValue(secret.Secret));

        if (EnableGitHubToken)
            yield return ("GITHUB_TOKEN", GetSecretValue("GITHUB_TOKEN"));
    }

    protected virtual IEnumerable<GitHubActionsDetailedTrigger> GetTriggers()
    {
        if (OnPushBranches.Length > 0 ||
            OnPushBranchesIgnore.Length > 0 ||
            OnPushTags.Length > 0 ||
            OnPushTagsIgnore.Length > 0 ||
            OnPushIncludePaths.Length > 0 ||
            OnPushExcludePaths.Length > 0)
        {
            Assert.True(
                OnPushBranches.Length == 0 && OnPushTags.Length == 0 || OnPushBranchesIgnore.Length == 0 && OnPushTagsIgnore.Length == 0,
                $"Cannot use {nameof(OnPushBranches)}/{nameof(OnPushTags)} and {nameof(OnPushBranchesIgnore)}/{nameof(OnPushTagsIgnore)} in combination");

            yield return new GitHubActionsVcsTrigger
            {
                Kind = GitHubActionsTrigger.Push,
                Branches = OnPushBranches,
                BranchesIgnore = OnPushBranchesIgnore,
                Tags = OnPushTags,
                TagsIgnore = OnPushTagsIgnore,
                IncludePaths = OnPushIncludePaths,
                ExcludePaths = OnPushExcludePaths
            };
        }

        if (OnPullRequestBranches.Length > 0 ||
            OnPullRequestTags.Length > 0 ||
            OnPullRequestIncludePaths.Length > 0 ||
            OnPullRequestExcludePaths.Length > 0)
        {
            yield return new GitHubActionsVcsTrigger
            {
                Kind = GitHubActionsTrigger.PullRequest,
                Branches = OnPullRequestBranches,
                BranchesIgnore = new string[0],
                Tags = OnPullRequestTags,
                TagsIgnore = new string[0],
                IncludePaths = OnPullRequestIncludePaths,
                ExcludePaths = OnPullRequestExcludePaths
            };
        }

        if (OnWorkflowDispatchOptionalInputs.Length > 0 ||
            OnWorkflowDispatchRequiredInputs.Length > 0)
        {
            yield return new GitHubActionsWorkflowDispatchTrigger
            {
                OptionalInputs = OnWorkflowDispatchOptionalInputs,
                RequiredInputs = OnWorkflowDispatchRequiredInputs
            };
        }

        if (!string.IsNullOrEmpty(OnCronSchedule))
        {
            yield return new GitHubActionsScheduledTrigger { Cron = OnCronSchedule };
        }
    }
}
