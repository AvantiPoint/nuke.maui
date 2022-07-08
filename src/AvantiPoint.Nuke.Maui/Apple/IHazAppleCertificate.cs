using AvantiPoint.Nuke.Maui.Extensions;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
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

    AbsolutePath P12CertifiatePath => TemporaryDirectory / "apple.p12";

    AbsolutePath KeychainPath => TemporaryDirectory / "signing_temp.keychain";

    Target RestoreIOSCertificate => _ => _
        .OnlyOnMacHost()
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
                if(!KeychainPath.Exists())
                {
                    SecurityCreateKeychain(settings => settings
                        .SetPassword(P12Password)
                        .SetKeychain(KeychainPath));
                    Security($"set-keychain-settings -lut 21600 {KeychainPath}");
                }
                else
                {
                    Log.Information("Temporary Keychain already exists. Attempting to unlock keychain");
                }

                // Unlock Keychain
                SecurityUnlockKeychain(_ => _
                    .SetPassword(P12Password)
                    .SetKeychain(KeychainPath));
                // Import Pkcs12
                SecurityImport(_ => _
                    .SetCertificatePath(P12CertifiatePath)
                    .SetPassword(P12Password)
                    .EnableAllowAny()
                    .SetType(AppleCertificateType.cert)
                    .SetFormat(AppleCertificateFormat.pkcs12)
                    .SetKeychainPath(KeychainPath)
                    .SetProcessArgumentConfigurator(_ => _
                        .Add("-T /usr/bin/codesign")
                        .Add("-T /usr/bin/security")));
                // SetPartitionList
                SecuritySetPartitionList(_ => _
                    .SetAllowedList("apple-tool:,apple:")
                    .SetPassword(P12Password)
                    .SetKeychain(KeychainPath));
                // Update Keychain list
                Security($"list-keychain -d user -s {KeychainPath} login.keychain");
            }
            catch
            {
                Log.Error("Error Encountered by Security Tool");
                Assert.Fail("Unable to import p12 into the keychain");
            }
        });
}
