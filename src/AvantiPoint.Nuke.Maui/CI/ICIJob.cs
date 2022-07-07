namespace AvantiPoint.Nuke.Maui.CI
{
    public interface ICIJob
    {
        string? ArtifactName { get; }
        IEnumerable<string> CacheExcludePatterns { get; }
        IEnumerable<string> CacheIncludePatterns { get; }
        IEnumerable<string> CacheKeyFiles { get; }
        IEnumerable<string> DotNetSdks { get; }
        IEnumerable<string> DownloadArtifacts { get; }
        string? Environment { get; }
        HostedAgent Image { get; }
        SecretImportCollection ImportSecrets { get; }
        IEnumerable<string> InvokedTargets { get; }
        string Name { get; }
        IEnumerable<string> Needs { get; }
        bool PublishArtifacts { get; }
    }
}