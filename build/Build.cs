using System;
using AvantiPoint.Nuke.Maui;
using AvantiPoint.Nuke.Maui.Android;
using AvantiPoint.Nuke.Maui.Apple;
using AvantiPoint.Nuke.Maui.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Tools.NerdbankGitVersioning;

[GitHubWorkflow("ci",
    FetchDepth = 0,
    AutoGenerate = true,
    OnPushBranches = new[] { MasterBranch },
    JobNames = new[] { "android-build", "ios-build", "compile-lib", "publish-internal" } )]
[GitHubWorkflow("pr",
    OnPullRequestBranches = new[] { MasterBranch },
    FetchDepth = 0,
    AutoGenerate = true,
    JobNames = new[] { "android-build", "ios-build", "compile-lib" } )]
[WorkflowJob(
    Name = "android-build",
    //ArtifactName = "android",
    Image = HostedAgent.Windows,
    InvokedTargets = new[] { nameof(IHazAndroidBuild.CompileAndroid) },
    ImportSecrets = new[]
    {
        nameof(IHazAndroidKeystore.AndroidKeystoreName),
        nameof(IHazAndroidKeystore.AndroidKeystoreB64),
        nameof(IHazAndroidKeystore.AndroidKeystorePassword)
    })]

[WorkflowJob(
    Name = "ios-build",
    //ArtifactName = "ios",
    Image = HostedAgent.Mac,
    InvokedTargets = new[] { nameof(IHazIOSBuild.CompileIos) },
    ImportSecrets = new[]
    {
         nameof(IHazAppleCertificate.P12B64),
         nameof(IHazAppleCertificate.P12Password),
         nameof(IRestoreAppleProvisioningProfile.AppleIssuerId),
         nameof(IRestoreAppleProvisioningProfile.AppleKeyId),
         nameof(IRestoreAppleProvisioningProfile.AppleAuthKeyP8),
         nameof(IRestoreAppleProvisioningProfile.AppleProfileId)
    })]
[WorkflowJob(
    Name = "compile-lib",
    ArtifactName = "nuget",
    InvokedTargets = new[] { nameof(ICompileLibrary.CompileLib), "--solution AvantiPoint.Nuke.Maui.sln" } )]
[WorkflowJob(
    Name = "publish-internal",
    Needs = new[] { "compile-lib", "android-build", "ios-build" },
    DownloadArtifacts = new[] { "nuget" },
    ImportSecrets = new[]
    {
        nameof(IPublishInternal.InHouseNugetFeed),
        nameof(IPublishInternal.InHouseApiKey),
        $"{nameof(ICodeSignNuget.CodeSignCertificate)}=CODESIGNCERTIFICATE",
        $"{nameof(ICodeSignNuget.CodeSignClientId)}=CODESIGNCLIENTID",
        $"{nameof(ICodeSignNuget.CodeSignClientSecret)}=CODESIGNCLIENTSECRET",
        $"{nameof(ICodeSignNuget.CodeSignKeyVault)}=CODESIGNKEYVAULT",
        $"{nameof(ICodeSignNuget.CodeSignTenantId)}=CODESIGNTENANTID"
    },
    InvokedTargets = new[] { nameof(IPublishInternal.PublishNuGet) })]
class Build : MauiBuild, ICompileLibrary, IPublishInternal, ICodeSignNuget
{
    public static int Main () => Execute<Build>();

    const string MasterBranch = "master";

    public GitHubActions GitHubActions => GitHubActions.Instance;

    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;

    public override string ApplicationDisplayVersion => NerdbankVersioning.NuGetPackageVersion;
    public override long ApplicationVersion => IsLocalBuild ? (DateTimeOffset.Now.ToUnixTimeSeconds() - new DateTimeOffset(2022, 7, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeSeconds()) / 60 : GitHubActions.RunNumber;
}
