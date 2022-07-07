using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

// https://docs.microsoft.com/en-us/azure/devops/pipelines/release/caching
internal class AzurePipelinesCacheStep : AzurePipelinesStep
{
    public HostedAgent Agent { get; set; }
    public IEnumerable<string> KeyFiles { get; set; } = Array.Empty<string>();
    public string Path { get; set; } = string.Empty;

    private string AdjustedPath =>
        Agent == HostedAgent.Mac
            ? Path.Replace("~", "$(HOME)")
            : Path.Replace("~", "$(USERPROFILE)");

    private string Identifier => Path
        .Replace(".", "/")
        .Replace("~", "/")
        .Replace("/", "-")
        .Trim('-');

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- task: Cache@2");
        using (writer.Indent())
        {
            writer.WriteLine($"displayName: Cache ({Identifier})");
            writer.WriteLine("inputs:");
            using (writer.Indent())
            {
                writer.WriteLine($"key: $(Agent.OS) | {Identifier} | {KeyFiles.JoinCommaSpace()}");
                writer.WriteLine($"restoreKeys: $(Agent.OS) | {Identifier}");
                writer.WriteLine($"path: {AdjustedPath}");
            }
        }
    }
}
