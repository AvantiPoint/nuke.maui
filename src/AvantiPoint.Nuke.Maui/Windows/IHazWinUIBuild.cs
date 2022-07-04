using AvantiPoint.Nuke.Maui.Extensions;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Components;
using Nuke.Common.Tools.DotNet;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui.Windows;

public interface IHazWinUIBuild :
    IHazArtifacts,
    IHazConfiguration,
    IHazProject,
    IDotNetRestore,
    IHazMauiWorkload,
    IHazMauiAppVersion,
    IHazTimeout
{
    Target CompileWindows => _ => _
        .OnlyOnWindowsHost()
        .DependsOn<IHazMauiWorkload>()
        .DependsOn<IDotNetRestore>()
        .Produces(ArtifactsDirectory / "windows-build" / "*.msix")
        .Executes(() =>
        {
            var targetFramework = Project.GetTargetFramework("windows");
            targetFramework.NotNullOrEmpty("Could not locate a valid Windows Target Framework");

            if(!string.IsNullOrEmpty(ApplicationDisplayVersion))
                Log.Information("Display Version: {ApplicationDisplayVersion}", ApplicationDisplayVersion);

            if(ApplicationVersion > 0)
                Log.Information("Build Version: {ApplicationVersion}", ApplicationVersion);

            var outputDir = ArtifactsDirectory / "windows-build";
            DotNetPublish(settings =>
                settings.SetConfiguration(Configuration)
                    .SetProject(Project)
                    .SetFramework(targetFramework)
                    .AddProperty("GenerateAppxPackageOnBuild", true)
                    .AddProperty("AppxPackageSigningEnabled", false)
                    .When(!string.IsNullOrEmpty(ApplicationDisplayVersion), _ => _
                        .AddProperty(BuildProps.Maui.ApplicationDisplayVersion, ApplicationDisplayVersion))
                    .When(ApplicationVersion > 0, _ => _
                        .AddProperty(BuildProps.Maui.ApplicationVersion, ApplicationVersion))
                    .SetProcessExecutionTimeout(CompileTimeout)
                    .SetOutput(outputDir));

            Assert.NotEmpty(outputDir.GlobFiles("*.msix"), "Could not locate an MSIX file in the publish directory");
        });
}
