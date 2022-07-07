using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

internal class AzurePipelinesDownloadStep : AzurePipelinesStep
{
    public string ArtifactName { get; set; } = default!;
    public string DownloadPath { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- task: DownloadPipelineArtifact@2");
        using (writer.Indent())
        {
            // writer.WriteLine("displayName: Download Artifacts");
            writer.WriteLine("inputs:");
            using (writer.Indent())
            {
                writer.WriteLine($"artifact: {ArtifactName}");
                writer.WriteLine($"path: {DownloadPath.SingleQuote()}");
            }
        }
    }
}
