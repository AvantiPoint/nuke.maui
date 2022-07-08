using System;
using System.Collections.Generic;
using AvantiPoint.Nuke.Maui.CI;
using AvantiPoint.Nuke.Maui.Windows;

public class SignedWindowsBuild : WindowsJob
{
    public override IEnumerable<string> CacheIncludePatterns => Array.Empty<string>();

    public override SecretImportCollection ImportSecrets => new()
    {
        { nameof(IWinUICodeSign.AzureKeyVault), BuildSecrets.AzureKeyVault },
        { nameof(IWinUICodeSign.AzureKeyVaultCertificate), BuildSecrets.AzureKeyVaultCertificate },
        { nameof(IWinUICodeSign.AzureKeyVaultClientId), BuildSecrets.AzureKeyVaultClientId },
        { nameof(IWinUICodeSign.AzureKeyVaultClientSecret), BuildSecrets.AzureKeyVaultClientSecret },
        { nameof(IWinUICodeSign.AzureKeyVaultTenantId), BuildSecrets.AzureKeyVaultTenantId },
    };
}
