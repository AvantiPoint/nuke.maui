using Nuke.Common.CI;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

internal class AzurePipelinesStage : ConfigurationEntity
{
    public string Name { get; set; } = default!;
    public string? DisplayName { get; set; }
    public string? Environment { get; set; }
    public IEnumerable<AzurePipelinesJob> Jobs { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"- stage: {Name}");
        using (writer.Indent())
        {
            if (!string.IsNullOrEmpty(DisplayName))
                writer.WriteLine($"displayName: '{DisplayName}'");

            if (!string.IsNullOrEmpty(Environment))
                writer.WriteLine($"environment: {Environment}");

            writer.WriteLine("jobs:");
            Jobs.ForEach(x => x.Write(writer));
        }
    }
}
