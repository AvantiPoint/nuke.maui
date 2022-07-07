using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI;

public abstract class CIJobBase : ICIJob
{
    public virtual string Name => GetType().Name;

    public virtual string? Environment { get; }

    public virtual HostedAgent Image => HostedAgent.Windows;

    public virtual IEnumerable<string> Needs => Array.Empty<string>();

    public virtual IEnumerable<string> InvokedTargets => Array.Empty<string>();

    public virtual SecretImportCollection ImportSecrets => new();

    public virtual IEnumerable<string> CacheIncludePatterns => new[] { "~/.nuget/packages" };
    public virtual IEnumerable<string> CacheExcludePatterns => Array.Empty<string>();
    public virtual IEnumerable<string> CacheKeyFiles => new[] { "**/global.json", "**/*.csproj" };

    public virtual bool PublishArtifacts => true;

    public virtual string? ArtifactName => null;

    public virtual IEnumerable<string> DownloadArtifacts => Array.Empty<string>();

    public virtual IEnumerable<string> DotNetSdks => Array.Empty<string>();
}
