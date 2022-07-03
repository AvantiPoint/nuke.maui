# Nuke MAUI

The AvantiPoint Nuke Maui library is an extension library for [Nuke Build](https://www.nuke.build/) for developers writing DotNet Maui applications. Out of the box it's meant to simplify the process of generating a fully functional CI build for your target platforms.

| Platform | Status |
| -------- | ------ |
| Android | Supported |
| iOS | In Progress |
| macOS | In Progress |
| Windows | Planned |
| Tizen | Planned |

## Application Versioning

Different developers like to handle application versioning a bit different. As a result we take an approach that makes it dead simple to version your app but we do not actually take it to the next level where we make assumptions around how you want to handle versioning. You can easily apply any sort of GitVersioning supported by Nuke, or add a completely custom versioning system.

## Getting Started

To get started you will need to setup the Nuke Build CLI tool on your system and initialize a new Nuke Build project in your repo. This will update your solution file to include the build project and add a few helpful scripts and other resources needed by Nuke Build. Next update the Build class to inherit from `MauiBuild`. This will automatically add all of the supported targets to build the platforms listed above. The `MauiBuild` class is an abstract class and you will need to implement the ApplicationDisplayVersion and ApplicationVersion which will map to the MSBuild properties that MAUI uses.

By default if these properties are null or empty we will not specify them as command line arguments and it will be assumed that you are managing these values outside of the Nuke Build.

```bash
nuke :setup
```

```cs
public class Build : MauiBuild
{
    // This has no default targets and will display available targets when you run `nuke`
    public static int Main () => Execute<Build>();

    public GitHubActions GitHubActions => GitHubActions.Instance;

    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;

    public override string ApplicationDisplayVersion => NerdbankVersioning.NuGetPackageVersion;
    public override long ApplicationVersion => GitHubActions.RunId;
}
```

### Running the Build

The Build can easily be run by running the `nuke` command along with the desired build target.

```bash
nuke CompileAndroid
```

## Additional Considerations

Currently the MAUI templates do not include any additional parameters which are typically required for building, particularly on iOS. Be sure that the RuntimeIdentifier property is set to `ios-arm64` when building otherwise the build will fail.

The P12 Certificate used to sign iOS and macCatalyst apps along with the P8 Auth Key for connecting to AppStoreConnect, as well as the Android Keystore must be provided as Base64 encoded strings. The P12 & Android Keystore files will be decoded restored on the local filesystem in the Nuke temp directory. The P12 for iOS & macCatalyst will be added to a temporary Key Chain for use during the build.

As installing workloads requires sudo access which can be a bit of a pain when running this locally, the Install Maui Workload target will first determine if the MAUI workload is installed. If it is already installed it will return eliminating any issues with requiring sudo access. This shouldn't affect your CI builds as you will already have the necessary permissions.

### Apple Provisioning Profiles

The Nuke Targets include a target that will reach out to the Apple AppStore Connect API to retrieve a specified Provisioning Profile. This is particularly useful for CI Builds as it ensures that as long as your provisioning profile is active you will always have the latest valid profile. This can really save time when you need to regenerate the provisioning profile for new team members, add new devices, or renew expiring profiles.

## Creating Workflows

The AvantiPoint.Nuke.Maui library includes some custom attributes that can be used to create custom GitHub Workflows with multiple jobs per workflow. This can be done by defining WorkflowJobs and GitHubWorkflows. The Workflow can define as many Job Names as are required.

```cs
[GitHubWorkflow("maui-build",
    FetchDepth = 0,
    AutoGenerate = true,
    OnPushBranches = new[] { MasterBranch },
    JobNames = new[] { "android-build", "ios-build" } )]
[WorkflowJob(
    Name = "android-build",
    //ArtifactName = "android",
    Image = GitHubActionsImage.WindowsLatest,
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
    Image = GitHubActionsImage.MacOsLatest,
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
public class Build : MauiBuild
{
    public static int Main () => Execute<Build>();

    const string MasterBranch = "master";

    public GitHubActions GitHubActions => GitHubActions.Instance;

    [NerdbankGitVersioning]
    readonly NerdbankGitVersioning NerdbankVersioning;

    public override string ApplicationDisplayVersion => NerdbankVersioning.NuGetPackageVersion;
    public override long ApplicationVersion => GitHubActions.RunId;
}
```

## Running Locally

To run locally choose you will need to ensure that your environment has been configured with the secrets required to sign your app. Start by running `nuke :secrets` to add the values of the secrets you will need for the iOS or Android Build. Next pick the target you want to run and run `nuke` with the target name.

```bash
nuke CompileAndroid

nuke CompileIos
```
