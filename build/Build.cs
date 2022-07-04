using System;
using AvantiPoint.Nuke.Maui;
using AvantiPoint.Nuke.Maui.Android;
using AvantiPoint.Nuke.Maui.Apple;
using AvantiPoint.Nuke.Maui.CI;
using AvantiPoint.Nuke.Maui.Windows;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Tools.NerdbankGitVersioning;

[GitHubWorkflow("ci",
    FetchDepth = 0,
    AutoGenerate = true,
    OnPushBranches = new[] { MasterBranch },
    JobNames = new[] { LibraryBuild, AndroidBuild, IOSBuild, WinUIBuild, PublishInternal } )]
[GitHubWorkflow("pr",
    OnPullRequestBranches = new[] { MasterBranch },
    FetchDepth = 0,
    AutoGenerate = true,
    JobNames = new[] { LibraryBuild, AndroidBuild, IOSBuild, WinUIBuild } )]
[WorkflowJob(
    Name = AndroidBuild,
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
    Name = IOSBuild,
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
    Name = WinUIBuild,
    Image = HostedAgent.Windows,
    InvokedTargets = new[] { nameof(IHazWinUIBuild.CompileWindows) })]
[WorkflowJob(
    Name = LibraryBuild,
    ArtifactName = "nuget",
    InvokedTargets = new[] { nameof(ICompileLibrary.CompileLib), "--solution AvantiPoint.Nuke.Maui.sln", "--project-name AvantiPoint.Nuke.Maui" } )]
[WorkflowJob(
    Name = PublishInternal,
    Needs = new[] { LibraryBuild, AndroidBuild, IOSBuild, WinUIBuild },
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
    public const string WinUIBuild = "windows-build";
    public const string AndroidBuild = "android-build";
    public const string IOSBuild = "ios-build";
    public const string MacCatalystBuild = "maccatalyst-build";
    public const string LibraryBuild = "compile-lib";
    public const string PublishInternal = "publish-internal";

    public static int Main () => Execute<Build>();

    const string MasterBranch = "master";

    public GitHubActions GitHubActions => GitHubActions.Instance;

    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;

    public override string ApplicationDisplayVersion => NerdbankVersioning.NuGetPackageVersion;
    public override long ApplicationVersion => IsLocalBuild ? (DateTimeOffset.Now.ToUnixTimeSeconds() - new DateTimeOffset(2022, 7, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeSeconds()) / 60 : GitHubActions.RunNumber;
}
