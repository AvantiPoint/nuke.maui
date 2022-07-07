using Nuke.Common.CI;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

internal class AzurePipelinesJob : ConfigurationEntity
{
    public string Name { get; set; } = default!;
    public string? DisplayName { get; set; }
    public string? Environment { get; set; }
    public IEnumerable<AzurePipelinesStep> Steps { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"- job: {Name}");
        using (writer.Indent())
        {
            if (!string.IsNullOrEmpty(DisplayName))
                writer.WriteLine($"displayName: '{DisplayName}'");

            if (!string.IsNullOrEmpty(Environment))
                writer.WriteLine($"environment: {Environment}");

            writer.WriteLine("steps:");
            Steps.ForEach(x => x.Write(writer));
        }
    }
}
