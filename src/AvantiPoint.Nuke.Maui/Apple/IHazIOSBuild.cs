using System.Text.Json;
using AvantiPoint.Nuke.Maui.Apple.AppStoreConnect;
using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui.Apple;

[PublicAPI]
public interface IHazIOSBuild :
    IHazArtifacts,
    IHazProject,
    IHazConfiguration,
    IHazAppleCertificate,
    IRestoreAppleProvisioningProfile,
    IDotNetClean,
    IDotNetRestore,
    IHazMauiWorkload,
    IHazMauiAppVersion,
    IHazTimeout
{
    [Parameter("Sets the Linker for iOS builds. Valid options None, SdkOnly, Full")]
    MtouchLink Linker => TryGetValue(() => Linker);

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

            var mobileProvision = TemporaryDirectory / "apple.mobileprovision";
            Assert.True(mobileProvision.Exists(), "No Provisioning Profile cache response exists.");
            var json = File.ReadAllText(mobileProvision);
            json.NotNullOrEmpty("The Provisioning Profile response cache was empty.");
            var profile = JsonSerializer.Deserialize<ProfileResponse>(json);
            profile.NotNull("Unable to deserialize the Profile Response.");
            var codesignKey = profile!.Attributes.ProfileType switch
            {
                ProfileType.IOS_APP_ADHOC => "iPhone Distribution",
                ProfileType.IOS_APP_STORE => "iPhone Distribution",
                ProfileType.IOS_APP_INHOUSE => "iPhone Distribution",
                ProfileType.IOS_APP_DEVELOPMENT => "iPhone Developer",
                _ => string.Empty
            };
            codesignKey.NotNullOrEmpty("Invalid Profile Type - {ProfileType}", profile.Attributes.ProfileType.ToString());

            var outputDirectory = ArtifactsDirectory / "ios-build";
            DotNetPublish(settings =>
                settings.SetConfiguration(Configuration)
                    .SetProject(Project)
                    .SetFramework(targetFramework)
                    .AddProperty(BuildProps.iOS.ArchiveOnBuild, true)
                    .AddProperty(BuildProps.iOS.CodesignKey, codesignKey)
                    .AddProperty(BuildProps.iOS.CodesignKeychain, KeychainPath)
                    .AddProperty(BuildProps.iOS.CodesignProvision, profile!.Attributes.Name)
                    .When(!string.IsNullOrEmpty(ApplicationDisplayVersion), _ => _
                        .AddProperty(BuildProps.Maui.ApplicationDisplayVersion, ApplicationDisplayVersion))
                    .When(ApplicationVersion > 0, _ => _
                        .AddProperty(BuildProps.Maui.ApplicationVersion, ApplicationVersion))
                    .When(Linker != null, _ => _
                        .AddProperty(BuildProps.iOS.MtouchLink, Linker))
                    .SetProcessExecutionTimeout(CompileTimeout)
                    .SetContinuousIntegrationBuild(!IsLocalBuild)
                    .SetDeterministic(!IsLocalBuild)
                    .SetOutput(outputDirectory)
                    .When(Verbosity == Verbosity.Verbose, _ => _.SetVerbosity(DotNetVerbosity.Diagnostic))
                    .When(IsLocalBuild, _ => _
                        .SetProcessArgumentConfigurator(_ => _.Add($"/bl:{outputDirectory / "ios.binlog"}"))));
        });
}
