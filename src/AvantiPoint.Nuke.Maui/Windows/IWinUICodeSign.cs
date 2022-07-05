using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Components;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Windows;

public interface IWinUICodeSign : IHazArtifacts, IHazAzureKeyVaultCertificate
{
    [Parameter("Base 64 Encoded PFX for code signing the Windows MSIX"), Secret]
    string PfxB64 => TryGetValue(() => PfxB64);

    [Parameter("The password for the Windows signing Certificate"), Secret]
    string PfxPassword => TryGetValue(() => PfxPassword);

    [Parameter("The Signing Algorithm, i.e. sha1, sha256, sha384, sha512. Defaults to sha256")]
    CodeSigningDigestAlgorithm DigestAlgorithm => TryGetValue(() => DigestAlgorithm) ?? CodeSigningDigestAlgorithm.SHA256;

    Target CodeSignMsix => _ => _
        .OnlyWhenDynamic(() => EnvironmentInfo.Platform.ToString() == nameof(PlatformFamily.Windows) &&
            ((!string.IsNullOrEmpty(PfxB64) && !string.IsNullOrEmpty(PfxPassword)) ||
            (!string.IsNullOrEmpty(AzureKeyVault) &&
            !string.IsNullOrEmpty(AzureKeyVaultCertificate) &&
            !string.IsNullOrEmpty(AzureKeyVaultClientId) &&
            !string.IsNullOrEmpty(AzureKeyVaultClientSecret) &&
            !string.IsNullOrEmpty(AzureKeyVaultTenantId))))
        .Unlisted()
        .Executes(() =>
        {
            var msixFiles = ArtifactsDirectory.GlobFiles("**/*.msix");
            Assert.NotEmpty(msixFiles, "No MSIX files could be located.");

            if (!this.LocalCodeSign(msixFiles.Cast<string>()) && !this.AzureKeyVaultSign(msixFiles.Cast<string>()))
            {
                Log.Warning("The MSIX was not signed.");
            }
        });
}
