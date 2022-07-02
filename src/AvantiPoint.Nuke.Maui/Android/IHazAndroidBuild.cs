using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui.Android;

[PublicAPI]
public interface IHazAndroidBuild :
    IHazArtifacts,
    IHazConfiguration,
    IDotNetRestore,
    IHazMauiWorkload,
    IHazAndroidKeystore,
    IHazSolution,
    IHazMauiAppVersion,
    IHazTimeout
{
    Target CompileAndroid => _ => _
        .DependsOn<IHazAndroidKeystore>()
        .DependsOn<IHazMauiWorkload>()
        .DependsOn<IDotNetRestore>()
        .Produces(ArtifactsDirectory)
        .Executes(() =>
        {
            var targetFramework = Solution.GetTargetFramework("android");
            targetFramework.NotNullOrEmpty("Could not locate a valid Android Target Framework");

            Log.Information($"Display Version: {ApplicationDisplayVersion}");
            Log.Information($"Build Version: {ApplicationVersion}");

            DotNetPublish(settings =>
                settings.SetConfiguration(Configuration)
                    .SetFramework(targetFramework)
                    .AddProperty(BuildProps.Android.AndroidSigningKeyPass, AndroidKeystorePassword)
                    .AddProperty(BuildProps.Android.AndroidSigningStorePass, AndroidKeystorePassword)
                    .AddProperty(BuildProps.Android.AndroidSigningKeyAlias, AndroidKeystoreName)
                    .AddProperty(BuildProps.Android.AndroidSigningKeyStore, KeystorePath)
                    .AddProperty(BuildProps.Maui.ApplicationDisplayVersion, ApplicationDisplayVersion)
                    .AddProperty(BuildProps.Maui.ApplicationVersion, ApplicationVersion)
                    .SetProcessExecutionTimeout(CompileTimeout.Milliseconds)
                    .SetOutput(ArtifactsDirectory));

            Assert.True(Directory.EnumerateFiles(ArtifactsDirectory, "*-Signed.apk").Any(), "No Signed APK was found in the output directory");
            Assert.True(Directory.EnumerateFiles(ArtifactsDirectory, "*-Signed.aab").Any(), "No Signed AAB was found in the output directory");
        });
}
