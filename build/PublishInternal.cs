using System.Collections.Generic;
using AvantiPoint.Nuke.Maui;
using AvantiPoint.Nuke.Maui.CI;

public class PublishInternal : CIJobBase
{
    public override IEnumerable<string> DownloadArtifacts => new[] { "nuget" };

    public override SecretImportCollection ImportSecrets => new()
    {
        nameof(IPublishInternal.InHouseNugetFeed),
        nameof(IPublishInternal.InHouseApiKey),
        { nameof(IHazAzureKeyVaultCertificate.AzureKeyVault), BuildSecrets.AzureKeyVault },
        { nameof(IHazAzureKeyVaultCertificate.AzureKeyVaultCertificate), BuildSecrets.AzureKeyVaultCertificate },
        { nameof(IHazAzureKeyVaultCertificate.AzureKeyVaultClientId), BuildSecrets.AzureKeyVaultClientId },
        { nameof(IHazAzureKeyVaultCertificate.AzureKeyVaultClientSecret), BuildSecrets.AzureKeyVaultClientSecret },
        { nameof(IHazAzureKeyVaultCertificate.AzureKeyVaultTenantId), BuildSecrets.AzureKeyVaultTenantId },
    };

    public override IEnumerable<string> InvokedTargets => new[] { nameof(IPublishInternal.PublishNuGet) };
}
