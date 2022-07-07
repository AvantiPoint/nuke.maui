using AvantiPoint.Nuke.Maui.CI;
using AvantiPoint.Nuke.Maui.Windows;

public class SignedWindowsBuild : WindowsJob
{
    public override SecretImportCollection ImportSecrets => new()
    {
        { nameof(IWinUICodeSign.AzureKeyVault), BuildSecrets.AzureKeyVault },
        { nameof(IWinUICodeSign.AzureKeyVaultCertificate), BuildSecrets.AzureKeyVaultCertificate },
        { nameof(IWinUICodeSign.AzureKeyVaultClientId), BuildSecrets.AzureKeyVaultClientId },
        { nameof(IWinUICodeSign.AzureKeyVaultClientSecret), BuildSecrets.AzureKeyVaultClientSecret },
        { nameof(IWinUICodeSign.AzureKeyVaultTenantId), BuildSecrets.AzureKeyVaultTenantId },
    };
}
