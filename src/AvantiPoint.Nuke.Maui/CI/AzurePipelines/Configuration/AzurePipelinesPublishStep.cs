using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

internal class AzurePipelinesPublishStep : AzurePipelinesStep
{
    public string ArtifactName { get; set; } = default!;
    public string PathToPublish { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- task: PublishPipelineArtifact@1");
        using (writer.Indent())
        {
            writer.WriteLine("inputs:");
            using (writer.Indent())
            {
                writer.WriteLine($"artifactName: {ArtifactName}");
                writer.WriteLine($"targetPath: {PathToPublish.SingleQuote()}");
            }
        }
    }
}
