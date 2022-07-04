using JetBrains.Annotations;

namespace AvantiPoint.Nuke.Maui.CI;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class WorkflowJobAttribute : Attribute
{
    public string Name { get; set; } = "";

    public HostedAgent Image { get; set; } = HostedAgent.Windows;

    public string[] Needs { get; set; } = Array.Empty<string>();

    public string[] InvokedTargets { get; set; } = Array.Empty<string>();

    public string[] ImportSecrets { get; set; } = Array.Empty<string>();

    public string[] CacheIncludePatterns { get; set; } = { "~/.nuget/packages" };
    public string[] CacheExcludePatterns { get; set; } = Array.Empty<string>();
    public string[] CacheKeyFiles { get; set; } = { "**/global.json", "**/*.csproj" };

    public bool PublishArtifacts { get; set; } = true;

    public string ArtifactName { get; set; } = "";

    public string[] DownloadArtifacts { get; set; } = Array.Empty<string>();

    public string[] DotNetSdks { get; set; } = Array.Empty<string>();
}
