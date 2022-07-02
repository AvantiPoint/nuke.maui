using System.Reflection;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Utilities.Collections;
using AvantiPoint.Nuke.Maui;
using AvantiPoint.Nuke.Maui.Android;
using AvantiPoint.Nuke.Maui.Apple;
using Nuke.Common.Tools.NerdbankGitVersioning;

[GitHubActions("android-build",
    GitHubActionsImage.WindowsLatest,
    FetchDepth = 0,
    AutoGenerate = true,
    OnPushBranches = new[] { MasterBranch },
    InvokedTargets = new[] { nameof(IHazAndroidBuild.CompileAndroid) },
    ImportSecrets = new[] { nameof(IHazAndroidKeystore.AndroidKeystoreName), nameof(IHazAndroidKeystore.AndroidKeystoreB64), nameof(IHazAndroidKeystore.AndroidKeystorePassword) }
    )]
[GitHubActions("ios-build",
    GitHubActionsImage.MacOsLatest,
    FetchDepth = 0,
    AutoGenerate = true,
    OnPushBranches = new[] { MasterBranch },
    InvokedTargets = new[] { nameof(IHazIOSBuild.CompileIos) },
    ImportSecrets = new[]
    {
         nameof(IHazAppleCertificate.P12B64),
         nameof(IHazAppleCertificate.P12Password),
         nameof(IRestoreAppleProvisioningProfile.AppleIssuerId),
         nameof(IRestoreAppleProvisioningProfile.AppleKeyId),
         nameof(IRestoreAppleProvisioningProfile.AppleAuthKeyP8),
         nameof(IRestoreAppleProvisioningProfile.AppleProfileId)
    }
)]
class Build : MauiBuild
{
    public static int Main () => Execute<Build>();

    const string MasterBranch = "master";

    public GitHubActions GitHubActions => GitHubActions.Instance;

    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;

    public override string ApplicationDisplayVersion => NerdbankVersioning.NuGetPackageVersion;
    public override long ApplicationVersion => GitHubActions.RunId;
}
