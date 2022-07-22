﻿using System.Text.Json;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
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
            var assetsJsonPath = RootDirectory / "build" / "obj" / "project.assets.json";
            Assert.FileExists(assetsJsonPath, "Could not find the project.assets.json");
            Log.Information($"Project Assets: {assetsJsonPath}");
            var json = File.ReadAllText(assetsJsonPath);
            Assert.NotNullOrWhiteSpace(json, "The contents of the project.assets.json are null or empty");
            var assets = JsonSerializer.Deserialize<ProjectAssets>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web));

            Assert.True(assets.Project.Frameworks.ContainsKey("net6.0"), "Expected to find a net6.0 target in the build project.assets.json");

            var framework = assets.Project.Frameworks["net6.0"];
            framework.DownloadDependencies
                .ForEach(x => Log.Information($"Download Dependency: {x.Name} - {x.Version}"));

            var artifacts = ArtifactsDirectory / "windows-build" / "AppPackages";

            var msixFiles = artifacts.GlobFiles("*/*.msix");
            Assert.NotEmpty(msixFiles, "No MSIX files could be located.");

            var filePaths = msixFiles.Select(x => (string)x);
            if (!this.LocalCodeSign(filePaths) && !this.AzureKeyVaultSign(filePaths))
            {
                Log.Warning("The MSIX was not signed.");
            }
        });
}
