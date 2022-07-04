using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Components;
using Serilog;
using static AvantiPoint.Nuke.Maui.Apple.SecurityTasks;

namespace AvantiPoint.Nuke.Maui.Apple;

[PublicAPI]
public interface IHazAppleCertificate : IHazGitRepository, INukeBuild
{
    [Parameter("P12 Certificate must be Base64 Encoded"), Secret]
    string P12B64 => TryGetValue(() => P12B64);

    [Parameter("P12 Certificate must be provided"), Secret]
    string P12Password => TryGetValue(() => P12Password);

    AbsolutePath P12CertifiatePath => (AbsolutePath)Path.Combine(TemporaryDirectory, "apple.p12");

    Target RestoreIOSCertificate => _ => _
        .OnlyOnMacHost(() => !IsLocalBuild)
        .TryBefore<IDotNetRestore>()
        .BeforeMauiWorkload()
        .Unlisted()
        .Requires(() => P12B64)
        .Requires(() => P12Password)
        .Executes(() =>
        {
            var data = Convert.FromBase64String(P12B64);
            File.WriteAllBytes(P12CertifiatePath, data);

            try
            {

                var keychainPath = (AbsolutePath)Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) / "Library"
                    / "Keychains" / EnvironmentInfo.WorkingDirectory.Name / GitRepository.Commit / "app-signing.keychain-db";
                if(keychainPath.Exists())
                {
                    SecurityCreateKeychain(settings => settings
                        .SetPassword(P12Password)
                        .SetKeychain(keychainPath));
                    Security($"set-keychain-settings -lut 21600 {keychainPath}");
                    SecurityUnlockKeychain(_ => _
                        .SetPassword(P12Password)
                        .SetKeychain(keychainPath));
                }
                else
                {
                    Log.Information("Temporary Keychain already exists. Attempting to unlock keychain");
                    Security($"set-keychain-settings -lut 21600 {keychainPath}");
                }

                SecurityImport(_ => _
                    .SetCertificatePath(P12CertifiatePath)
                    .SetKeychainPath(keychainPath)
                    .SetPassword(P12Password));

                Security($"list-keychain -d user -s {keychainPath}");
            }
            catch
            {
                Log.Error("Error Encountered by Security Tool");
                Assert.Fail("Unable to import p12 into the keychain");
            }
        });
}
