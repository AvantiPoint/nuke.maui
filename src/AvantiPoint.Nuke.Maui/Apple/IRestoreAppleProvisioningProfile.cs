using System.Text.Json;
using AvantiPoint.Nuke.Maui.Apple.AppStoreConnect;
using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
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
        .Executes(async () =>
        {
            bool ActiveProfile(ProfileResponse profile) =>
                profile.Id == AppleProfileId && profile.Attributes.ProfileState == ProfileState.ACTIVE;

            var profileResponse = await GetProvisioningProfiles();
            Assert.NotEmpty(profileResponse.Data, "No Provisioning Profiles found.");
            Assert.True(profileResponse.Data.Any(x => x.Id == AppleProfileId),
                $"No profile found with the id: {AppleProfileId}");
            Assert.True(profileResponse.Data.Any(ActiveProfile), "The specified Profile is currently Invalid.");

            var profile = profileResponse.Data
                .Where(ActiveProfile)
                .FirstOrDefault();

            profile.NotNull("Could not locate the Provisioning Profile");
            Log.Debug("Found an active provisioning profile.");

            // "$HOME/Library/MobileDevice/Provisioning Profiles/${UUID}.mobileprovision"
            if (!ProfileDirectory.Exists())
            {
                Log.Debug("Creating Provisioning Profiles Directory: '{ProfileDirectory}'", ProfileDirectory);
                Directory.CreateDirectory(ProfileDirectory);
            }

            Log.Debug("Caching Provisioning Profile data.");
            await File.WriteAllTextAsync(TemporaryDirectory / "apple.mobileprovision", JsonSerializer.Serialize(profile));

            var filePath = ProfileDirectory / $"{profile!.Attributes.Uuid}.mobileprovision";
            var data = Convert.FromBase64String(profile.Attributes.ProfileContent);
            File.WriteAllBytes(filePath, data);
            Log.Information(messageTemplate: "Downloaded Provisioning Profile: {0}",
                propertyValue: profile.Attributes.Name);
            Log.Verbose(File.ReadAllText(filePath));
        });
}
