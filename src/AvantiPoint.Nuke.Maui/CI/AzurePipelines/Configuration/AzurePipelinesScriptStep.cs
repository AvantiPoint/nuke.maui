using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

internal class AzurePipelinesScriptStep : AzurePipelinesStep
{
    public string Run { get; set; } = default!;
    public Dictionary<string, string> Imports { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- script: |");
        using (writer.Indent())
        {
            writer.WriteLine($"  {Run}");
            writer.WriteLine($"displayName: 'Run {Run}'");
            if (Imports.Count > 0)
            {
                writer.WriteLine("env:");
                using (writer.Indent())
                {
                    Imports.ForEach(x => writer.WriteLine($"{x.Key}: {x.Value}"));
                }
            }
        }
    }
}
