namespace AvantiPoint.Nuke.Maui.CI;

public class CIJob : ICIJob
{
    public string? ArtifactName { get; set; }
    public IEnumerable<string> CacheExcludePatterns { get; set; } = Array.Empty<string>();
    public IEnumerable<string> CacheIncludePatterns { get; set; } = new[] { ".nuke/temp", "~/.nuget/packages" };
    public IEnumerable<string> CacheKeyFiles { get; set; } = new[] { "**/global.json", "**/*.csproj" };
    public IEnumerable<string> DotNetSdks { get; set; } = Array.Empty<string>();
    public IEnumerable<string> DownloadArtifacts { get; set; } = Array.Empty<string>();
    public string? Environment { get; set; }
    public HostedAgent Image { get; set; } = HostedAgent.Windows;
    public SecretImportCollection ImportSecrets { get; set; } = new();
    public IEnumerable<string> InvokedTargets { get; set; } = Array.Empty<string>();
    public string Name { get; set; } = default!;
    public IEnumerable<string> Needs { get; set; } = Array.Empty<string>();
    public bool PublishArtifacts { get; set; } = true;
}
