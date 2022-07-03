using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Apple;

[PublicAPI]
public interface IHazIOSBuild :
    IHazArtifacts,
    IHazProject,
    IHazConfiguration,
    IHazAppleCertificate,
    IRestoreAppleProvisioningProfile,
    IDotNetRestore,
    IHazMauiWorkload,
    IHazMauiAppVersion,
    IHazTimeout
{
    [Parameter]
    MtouchLink Linker => TryGetValue(() => Linker) ?? MtouchLink.None;

    Target CompileIos => _ => _
        .OnlyOnMacHost()
        .DependsOn(RestoreIOSCertificate, DownloadProvisioningProfile, InstallWorkload, Restore)
        .Produces(ArtifactsDirectory / "ios-build" / "*.ipa")
        .Executes(() =>
        {
            var targetFramework = Project.GetTargetFramework("ios");
            targetFramework.NotNullOrEmpty("Could not locate a valid iOS Target Framework");

            if(!string.IsNullOrEmpty(ApplicationDisplayVersion))
                Log.Information("Display Version: {ApplicationDisplayVersion}", ApplicationDisplayVersion);

            if(ApplicationVersion > 0)
                Log.Information("Build Version: {ApplicationVersion}", ApplicationVersion);

            DotNetTasks.DotNetPublish(settings =>
                settings.SetConfiguration(Configuration)
                    .SetProject(Project)
                    .SetFramework(targetFramework)
                    .AddProperty(BuildProps.iOS.ArchiveOnBuild, true)
                    .When(!string.IsNullOrEmpty(ApplicationDisplayVersion), _ => _
                        .AddProperty(BuildProps.Maui.ApplicationDisplayVersion, ApplicationDisplayVersion))
                    .When(ApplicationVersion > 0, _ => _
                        .AddProperty(BuildProps.Maui.ApplicationVersion, ApplicationVersion))
                    .AddProperty(BuildProps.iOS.MtouchLink, Linker)
                    .SetProcessExecutionTimeout(CompileTimeout)
                    .SetOutput(ArtifactsDirectory / "ios-build"));
        });
}
