# Nuke Maui

The Nuke Maui library is an extension library that sits on top of Nuke Build. It contains a number of Components and Targets for use in building .NET MAUI applications. In addition to this the Nuke Maui library contains some additional CI Abstractions for defining Multi-Stage / Multi-Job CI Pipelines on Azure Pipelines or GitHub Actions. This functionality is not otherwise available in Nuke Build, and allows you to split your builds out so that jobs can be run in parallel across multiple hosted agents significantly reducing the amount of time you spend waiting for builds to complete.

## Getting Started

To Get started with Nuke Maui, you will need to first ensure that you have the Nuke Build CLI Tool installed and have set up a Nuke Build project in your repository. If you are unsure how to do this be sure to check out the official [Nuke Build documentation](https://nuke.build/docs/introduction). Once you have your Nuke Build project setup, you will need to install the `AvantiPoint.Nuke.Maui` NuGet package and update your Build class to inherit from `MauiBuild`. This will add all of the necessary targets for building Android, iOS, MacCatalyst, and Windows applications.

```cs
class Build : MauiBuild
{
    // your code here
}
```

The `MauiBuild` class is an abstract class which will require you to implement two properties:

- ApplicationDisplayVersion
- ApplicationVersion

These two properties map to the MAUI versioning properties. If you do NOT want Nuke Maui to automatically set these properties during the build you can simply provide a default implementation like:

```cs
class Build : MauiBuild
{
    public override string ApplicationDisplayVersion => string.Empty;
    public override long ApplicationVersion => 0;
}
```

There are a number of ways you can choose to version your app. For inspiration you can check out the implementation in the sample app in this project. This uses Nerdbank.GitVersioning to set the Display Version. When running locally it uses the DateTime to produce a numeric value that increments the ApplicationVersion, while in CI it uses the GitHubActions Build Number.

## CI YAML Generation

YAML is a pain to write for many developers, including the ones who do it on a fairly frequent basis. Nuke Build provides a framework for Generating these YAML files for you automatically. The Nuke Maui library builds on this framework to provide a way that you can easily abstract your build logic in C# and generate the YAML for either Azure Pipelines or GitHub Actions from the same C# Definition! For an example of this please see the Sample App in this repository.

### Setting up your build

To set up a build you simply need to add a C# class with whatever name you want. In this sample we'll use the name `CI` to represent our CI build. You absolutely must provide an implementation of the Stages property. You can optionally provide an override of several additional properties such as the Triggers you would like for your build which can be as simple as the branch name as shown below. You may additionally provide an array of branches to run the push trigger, or you can provide an implementation of the trigger if you need fine grained control such as Paths to include or exclude.

```cs
public class CI : CIBuild
{
    public override PushTrigger OnPush => "master";

    public override IEnumerable<ICIStage> Stages => new[]
    {
        new CIStage
        {
            Name = "Build",
            Jobs = new ICIJob[]
            {
                new AndroidJob(),
                new iOSJob(),
                new MacCatalystJob(),
                new WindowsJob()
            }
        }
    };
}
```

After you you have created your build class you can reference it using either the `GitHubWorkflow` or `AzurePipelines` attributes on your Build class like:

```cs
[GitHubWorkflow(typeof(CI))]
public class Build : MauiBuild
{
    // your code here
}
```

Now that you have a defined build with a CI Platform attribute on your Build simply run the `nuke` command and it will generate the YAML for you.

> **NOTE**
> The Jobs shown in the sample above are provided out of the box with Nuke Maui and provide a great default implementation for the properties you will need. You can optionally provide your own implementation inheriting from any of the provided jobs to add any additional secrets you may need to import or further customize the build.
