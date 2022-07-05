using JetBrains.Annotations;
using Nuke.Common;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IHazAzureKeyVaultCertificate : INukeBuild
{
    [Parameter("The Azure KeyVault Uri"), Secret]
    string AzureKeyVault => TryGetValue(() => AzureKeyVault);

    [Parameter("The name of the Code Sign certificate in the Azure Key Vault"), Secret]
    string AzureKeyVaultCertificate => TryGetValue(() => AzureKeyVaultCertificate);

    [Parameter("The Azure AD Client Id to connect to the Azure Key Vault"), Secret]
    string AzureKeyVaultClientId => TryGetValue(() => AzureKeyVaultClientId);

    [Parameter("The Azure AD Client Secret to connect to the Azure Key Vault"), Secret]
    string AzureKeyVaultClientSecret => TryGetValue(() => AzureKeyVaultClientSecret);

    [Parameter("The Azure AD Tenant Id"), Secret]
    string AzureKeyVaultTenantId => TryGetValue(() => AzureKeyVaultTenantId);
}
