using AvantiPoint.Nuke.Maui.Extensions;
using Nuke.Common;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Apple;

internal interface IHazMacCatalystBuild :
    IHazConfiguration,
    IDotNetRestore,
    IHazAppleCertificate,
    IRestoreAppleProvisioningProfile,
    IHazMauiWorkload,
    IHazMauiAppVersion,
    IHazArtifacts,
    IHazTimeout
{
    Target CompileMacCatalyst => _ => _
        .TryDependsOn<IDotNetRestore>()
        .TryDependsOn<IHazMauiWorkload>()
        .TryDependsOn<IHazAppleCertificate>()
        .TryDependsOn<IRestoreAppleProvisioningProfile>()
        .Produces(ArtifactsDirectory / "maccatalyst-build")
        .Executes(() =>
        {
            var targetFramework = Project.GetTargetFramework("maccatlyst");
            targetFramework.NotNullOrEmpty("Could not locate a valid iOS Target Framework");

            if (!string.IsNullOrEmpty(ApplicationDisplayVersion))
                Log.Information($"Display Version: {ApplicationDisplayVersion}");

            if (ApplicationVersion > 0)
                Log.Information($"Build Version: {ApplicationVersion}");

            DotNetTasks.DotNetPublish(settings =>
                settings.SetConfiguration(Configuration)
                    .SetProject(Project)
                    .SetFramework(targetFramework)
                    .AddProperty(BuildProps.MacCatalyst.CreatePackage, true)
                    .When(!string.IsNullOrEmpty(ApplicationDisplayVersion), _ => _
                        .AddProperty(BuildProps.Maui.ApplicationDisplayVersion, ApplicationDisplayVersion))
                    .When(ApplicationVersion > 0, _ => _
                        .AddProperty(BuildProps.Maui.ApplicationVersion, ApplicationVersion))
                    //.AddProperty(BuildProps.iOS.MtouchLink, Linker)
                    .SetProcessExecutionTimeout(CompileTimeout)
                    .SetOutput(ArtifactsDirectory));
        });
}
