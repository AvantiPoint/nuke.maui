using System;
using System.Linq;
using AvantiPoint.Nuke.Maui;
using AvantiPoint.Nuke.Maui.Tools;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using Nuke.Components;
using static AvantiPoint.Nuke.Maui.Tools.NuGetKeyVaultSignToolTasks;

public interface ICodeSignNuget : IHazArtifacts, IHazAzureKeyVaultCertificate
{
    Target CodeSign => _ => _
        .DependentFor<IPublishInternal>()
        .OnlyWhenStatic(() => !IsLocalBuild && !GitHubActions.Instance.IsPullRequest)
        .Requires(() => AzureKeyVault)
        .Requires(() => AzureKeyVaultCertificate)
        .Requires(() => AzureKeyVaultClientId)
        .Requires(() => AzureKeyVaultClientSecret)
        .Requires(() => AzureKeyVaultTenantId)
        .Executes(() =>
        {
            if (!Uri.TryCreate(AzureKeyVault, UriKind.Absolute, out var uri))
                Assert.Fail("The specified Code Sign Key Vault was not a valid uri");

            var files = ArtifactsDirectory.GlobFiles("**/*.nupkg", "**/*.snupkg");
            Assert.True(files.Any(), "No NuGet Packages could be found in the artifacts directory to sign");
            files.ForEach(x => NuGetKeyVaultSignTool(_ => _
                .SetPackageFilter(x)
                .SetClientId(AzureKeyVaultClientId)
                .SetClientSecret(AzureKeyVaultClientSecret)
                .SetTenantId(AzureKeyVaultTenantId)
                .SetAzureKeyVaultUrl(uri)
                .SetCertificateName(AzureKeyVaultCertificate)));
        });
}
