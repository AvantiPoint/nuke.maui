using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Nuke.Components;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui.Android;

[PublicAPI]
public interface IHazAndroidBuild :
    IHazArtifacts,
    IHazConfiguration,
    IHazProject,
    IDotNetClean,
    IDotNetRestore,
    IHazMauiWorkload,
    IHazAndroidKeystore,
    IHazMauiAppVersion,
    IHazTimeout
{
    Target CompileAndroid => _ => _
        .DependsOn<IHazAndroidKeystore>()
        .DependsOn<IHazMauiWorkload>()
        .DependsOn<IDotNetRestore>()
        .Produces(ArtifactsDirectory / "android-build" / "*-Signed.apk", ArtifactsDirectory / "android-build" / "*-Signed.aab")
        .Executes(() =>
        {
            var targetFramework = Project.GetTargetFramework("android");
            targetFramework.NotNullOrEmpty("Could not locate a valid Android Target Framework");

            if (!string.IsNullOrEmpty(ApplicationDisplayVersion))
                Log.Information($"Display Version: {ApplicationDisplayVersion}");

            if (ApplicationVersion > 0)
                Log.Information($"Build Version: {ApplicationVersion}");

            var outputDirectory = ArtifactsDirectory / "android-build";

            DotNetPublish(settings =>
                settings.SetConfiguration(Configuration)
                    .SetProject(Project)
                    .SetFramework(targetFramework)
                    .AddProperty(BuildProps.Android.AndroidSigningKeyPass, AndroidKeystorePassword)
                    .AddProperty(BuildProps.Android.AndroidSigningStorePass, AndroidKeystorePassword)
                    .AddProperty(BuildProps.Android.AndroidSigningKeyAlias, AndroidKeystoreName)
                    .AddProperty(BuildProps.Android.AndroidSigningKeyStore, KeystorePath)
                    .When(!string.IsNullOrEmpty(ApplicationDisplayVersion), _ => _
                        .AddProperty(BuildProps.Maui.ApplicationDisplayVersion, ApplicationDisplayVersion))
                    .When(ApplicationVersion > 0, _ => _
                        .AddProperty(BuildProps.Maui.ApplicationVersion, ApplicationVersion))
                    .SetProcessExecutionTimeout(CompileTimeout)
                    .SetContinuousIntegrationBuild(!IsLocalBuild)
                    .SetDeterministic(!IsLocalBuild)
                    .SetOutput(outputDirectory)
                    .When(IsLocalBuild, _ => _
                        .SetProcessArgumentConfigurator(_ => _.Add($"/bl:{outputDirectory / "android.binlog"}"))));

            Assert.NotEmpty(outputDirectory.GlobFiles("*-Signed.apk", "*-Signed.aab"), "No Signed APK or AAB files could be found");
            outputDirectory.GlobFiles("*")
                .Where(x => !x.Name.Contains("-Signed"))
                .ForEach(x =>
                {
                    if (!x.NameWithoutExtension.EndsWith("-Signed"))
                        File.Delete(x);
                    else
                        Log.Information("Android Artifact: {Name}", x.Name);
                });
        });
}
