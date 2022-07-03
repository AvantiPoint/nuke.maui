using AvantiPoint.Nuke.Maui.Apple.AppStoreConnect;
using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Components;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Apple;

[PublicAPI]
public interface IRestoreAppleProvisioningProfile : IUsesAppStoreConnect
{
    [Parameter("Apple Profile Id is required"), Secret]
    string AppleProfileId => TryGetValue(() => AppleProfileId);

    AbsolutePath ProfileDirectory => (AbsolutePath)Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) / "Library" / "MobileDevice" / "Provisioning Profiles";

    Target DownloadProvisioningProfile => _ => _
        .OnlyOnMacHost()
        .TryBefore<IDotNetRestore>()
        .BeforeMauiWorkload()
        .Unlisted()
        .Requires(() => AppleIssuerId)
        .Requires(() => AppleKeyId)
        .Requires(() => AppleProfileId)
        .Requires(() => AppleAuthKeyP8)
        .Executes(() =>
        {
            bool ActiveProfile(ProfileResponse profile) =>
                profile.Id == AppleProfileId && profile.Attributes.ProfileState == ProfileState.ACTIVE;

            var profileResponse = GetProvisioningProfiles();
            Assert.NotEmpty(profileResponse.Data, "No Provisioning Profiles found.");
            Assert.True(profileResponse.Data.Any(x => x.Id == AppleProfileId),
                $"No profile found with the id: {AppleProfileId}");
            Assert.True(profileResponse.Data.Any(ActiveProfile), "The specified Profile is currently Invalid.");

            var profiles = profileResponse.Data
                .Where(ActiveProfile)
                .ToArray();

            foreach (var profile in profiles)
            {
                // "$HOME/Library/MobileDevice/Provisioning Profiles/${UUID}.mobileprovision"
                if (!ProfileDirectory.Exists())
                {
                    Log.Information("Creating Provisioning Profiles Directory: '{ProfileDirectory}'", ProfileDirectory);
                    Directory.CreateDirectory(ProfileDirectory);
                }

                var filePath = Path.Combine(
                    ProfileDirectory,
                    $"{profile.Attributes.Uuid}.mobileprovision");
                var data = Convert.FromBase64String(profile.Attributes.ProfileContent);
                File.WriteAllBytes(filePath, data);
                Log.Information(messageTemplate: "Downloaded Provisioning Profile: {0}",
                    propertyValue: profile.Attributes.Name);
                Log.Information(File.ReadAllText(filePath));
            }
        });
}
