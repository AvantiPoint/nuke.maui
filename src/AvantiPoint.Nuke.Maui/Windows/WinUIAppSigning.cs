using AvantiPoint.Nuke.Maui.Tools.DotNet;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.AzureSignTool;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.SignTool;
using Serilog;
using static Nuke.Common.Tools.AzureSignTool.AzureSignToolTasks;
using static Nuke.Common.Tools.SignTool.SignToolTasks;

namespace AvantiPoint.Nuke.Maui.Windows;

internal static class WinUIAppSigning
{
    public static AbsolutePath CertificatePath => NukeBuild.TemporaryDirectory / "WinSignCert.pfx";

    public static bool LocalCodeSign(this IWinUICodeSign codeSign, IEnumerable<string> files)
    {
        if (string.IsNullOrEmpty(codeSign.PfxB64) || string.IsNullOrEmpty(codeSign.PfxPassword))
        {
            Log.Debug("No Base64 Encoded Certificate & Password has been provided.");
            return false;
        }

        Log.Debug("Restoring PFX");
        var data = Convert.FromBase64String(codeSign.PfxB64);
        File.WriteAllBytes(CertificatePath, data);
        Assert.FileExists(CertificatePath, "Could not locate the restored certificate");
        Log.Debug("PFX Restored");

        SignTool(_ => _
            .EnableAutomaticSelection()
            .SetFileDigestAlgorithm(codeSign.DigestAlgorithm)
            .SetFile(CertificatePath)
            .SetPassword(codeSign.PfxPassword)
            .AddFiles(files));
        return true;
    }

    public static bool AzureKeyVaultSign(this IWinUICodeSign codeSign, IEnumerable<string> files)
    {
        if (string.IsNullOrEmpty(codeSign.AzureKeyVault) ||
            string.IsNullOrEmpty(codeSign.AzureKeyVaultCertificate) ||
            string.IsNullOrEmpty(codeSign.AzureKeyVaultClientId) ||
            string.IsNullOrEmpty(codeSign.AzureKeyVaultClientSecret) ||
            string.IsNullOrEmpty(codeSign.AzureKeyVaultTenantId))
        {
            Log.Debug("Build has not been configured for signing the MSIX with Azure Key Vault");
            return false;
        }

        Assert.True(codeSign.AzureKeyVault.StartsWith("https://"), "The Uri must start with the https protocol");
        Assert.True(Uri.TryCreate(codeSign.AzureKeyVault, UriKind.Absolute, out var uri), "The supplied Azure Key Vault is not a valid Uri.");

        DotNetToolHelper.EnsureInstalled("AzureSignTool");

        AzureSignTool(_ => _
                .SetKeyVaultUrl(codeSign.AzureKeyVault)
                .SetKeyVaultClientId(codeSign.AzureKeyVaultClientId)
                .SetKeyVaultClientSecret(codeSign.AzureKeyVaultClientSecret)
                .SetKeyVaultTenantId(codeSign.AzureKeyVaultTenantId)
                .SetKeyVaultCertificateName(codeSign.AzureKeyVaultCertificate)
                .SetFileDigest(codeSign.DigestAlgorithm)
                .SetTimestampRfc3161Url("http://timestamp.digicert.com")
                .SetTimestampDigest(codeSign.DigestAlgorithm)
                .When(codeSign.Verbosity == Verbosity.Verbose, _ => _
                    .EnableVerbose())
                .AddFiles(files));

        return true;
    }
}
