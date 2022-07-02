# Nuke MAUI

The AvantiPoint Nuke Maui library is an extension library for [Nuke Build](https://www.nuke.build/) for developers writing DotNet Maui applications. Out of the box it's meant to simplify the process of generating a fully functional CI build for your target platforms. 

| Platform | Status |
| -------- | ------ |
| Android | Supported |
| iOS | In Progress |
| macOS | Planned |
| Windows | Planned |
| Tizen | Planned |

## Application Versioning

Different developers like to handle application versioning a bit different. As a result we take an approach that makes it dead simple to version your app but we do not actually take it to the next level where we make assumptions around how you want to handle versioning. You can easily apply any sort of GitVersioning supported by Nuke, or add a completely custom versioning system.

## Getting Started

To get started you will need to setup the Nuke Build CLI tool on your system and initialize a new Nuke Build project in your repo. This will update your solution file to include the build project and add a few helpful scripts and other resources needed by Nuke Build. Next update the Build class to inherit from `MauiBuild`. This will automatically add all of the supported targets to build the platforms listed above. The `MauiBuild` class is an abstract class and you will need to implement the ApplicationDisplayVersion and ApplicationVersion which will map to the MSBuild properties that MAUI uses.

By default if these properties are null or empty we will not specify them as command line arguments and it will be assumed that you are managing these values outside of the Nuke Build.

```cs
public class Build : MauiBuild
{
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