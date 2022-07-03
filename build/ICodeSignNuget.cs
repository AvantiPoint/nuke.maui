using System;
using System.Linq;
using AvantiPoint.Nuke.Maui.Tools;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using Nuke.Components;
using static AvantiPoint.Nuke.Maui.Tools.NuGetKeyVaultSignToolTasks;

public interface ICodeSignNuget : IHazArtifacts
{
    [Parameter("The Azure KeyVault Uri"), Secret]
    string CodeSignKeyVault => TryGetValue(() => CodeSignKeyVault);

    [Parameter("The name of the Code Sign certificate in the Azure Key Vault"), Secret]
    string CodeSignCertificate => TryGetValue(() => CodeSignCertificate);

    [Parameter("The Azure AD Client Id to connect to the Azure Key Vault"), Secret]
    string CodeSignClientId => TryGetValue(() => CodeSignClientId);

    [Parameter("The Azure AD Tenant Id"), Secret]
    string CodeSignTenantId => TryGetValue(() => CodeSignTenantId);

    [Parameter("The Azure AD Client Secret to connect to the Azure Key Vault"), Secret]
    string CodeSignClientSecret => TryGetValue(() => CodeSignClientSecret);

    Target CodeSign => _ => _
        .DependentFor<IPublishInternal>()
        .OnlyWhenStatic(() => !IsLocalBuild && !GitHubActions.Instance.IsPullRequest)
        .Requires(() => CodeSignKeyVault)
        .Requires(() => CodeSignCertificate)
        .Requires(() => CodeSignClientId)
        .Requires(() => CodeSignClientSecret)
        .Requires(() => CodeSignTenantId)
        .Executes(() =>
        {
            if (!Uri.TryCreate(CodeSignKeyVault, UriKind.Absolute, out var uri))
                Assert.Fail("The specified Code Sign Key Vault was not a valid uri");

            var files = ArtifactsDirectory.GlobFiles("**/*.nupkg", "**/*.snupkg");
            Assert.True(files.Any(), "No NuGet Packages could be found in the artifacts directory to sign");
            files.ForEach(x => NuGetKeyVaultSignTool(_ => _
                .SetPackageFilter(x)
                .SetClientId(CodeSignClientId)
                .SetClientSecret(CodeSignClientSecret)
                .SetTenantId(CodeSignTenantId)
                .SetAzureKeyVaultUrl(uri)
                .SetCertificateName(CodeSignCertificate)));
        });
}
