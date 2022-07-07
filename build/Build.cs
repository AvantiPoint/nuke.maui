using System;
using AvantiPoint.Nuke.Maui;
using AvantiPoint.Nuke.Maui.CI.AzurePipelines;
using AvantiPoint.Nuke.Maui.CI.GitHubActions;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Tools.NerdbankGitVersioning;

[GitHubWorkflow(typeof(CI))]
[GitHubWorkflow(typeof(PR))]
[AzurePipelines(typeof(CI))]
class Build : MauiBuild, ICompileLibrary, IPublishInternal, ICodeSignNuget
{
    public static int Main () => Execute<Build>();

    public GitHubActions GitHubActions => GitHubActions.Instance;

    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;

    public override string ApplicationDisplayVersion => NerdbankVersioning.SimpleVersion;
    public override long ApplicationVersion => IsLocalBuild ?
        (DateTimeOffset.Now.ToUnixTimeSeconds() - new DateTimeOffset(2022, 7, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeSeconds()) / 60 :
        GitHubActions.RunNumber;
}
