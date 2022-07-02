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
    IHazSolution,
    IHazConfiguration,
    IHazAppleCertificate,
    IRestoreAppleProvisioningProfile,
    IRestore,
    IHazMauiWorkload,
    IHazMauiAppVersion,
    IHazTimeout
{
    MtouchLink Linker => TryGetValue(() => Linker);

    Target CompileIos => _ => _
        .OnlyOnMacHost()
        .DependsOn(RestoreIOSCertificate, DownloadProvisioningProfile, InstallWorkload, Restore)
        .Produces(ArtifactsDirectory / "*.ipa")
        .Executes(() =>
        {
            var targetFramework = Solution.GetTargetFramework("ios");
            targetFramework.NotNullOrEmpty("Could not locate a valid iOS Target Framework");

            if(!string.IsNullOrEmpty(ApplicationDisplayVersion))
                Log.Information($"Display Version: {ApplicationDisplayVersion}");

            if(ApplicationVersion > 0)
                Log.Information($"Build Version: {ApplicationVersion}");

            DotNetTasks.DotNetPublish(settings =>
                settings.SetConfiguration(Configuration)
                    .SetProject(Solution)
                    .SetFramework(targetFramework)
                    .AddProperty(BuildProps.iOS.ArchiveOnBuild, true)
                    .When(!string.IsNullOrEmpty(ApplicationDisplayVersion), _ => _
                        .AddProperty(BuildProps.Maui.ApplicationDisplayVersion, ApplicationDisplayVersion))
                    .When(ApplicationVersion > 0, _ => _
                        .AddProperty(BuildProps.Maui.ApplicationVersion, ApplicationVersion))
                    .AddProperty(BuildProps.iOS.MtouchLink, Linker)
                    .SetProcessExecutionTimeout(CompileTimeout.Milliseconds)
                    .SetOutput(ArtifactsDirectory));
        });
}
