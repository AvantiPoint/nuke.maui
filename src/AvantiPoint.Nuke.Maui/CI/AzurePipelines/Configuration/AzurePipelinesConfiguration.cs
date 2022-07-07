using Nuke.Common.CI;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

internal class AzurePipelinesConfiguration : ConfigurationEntity
{
    public CIVariableCollection Variables { get; set; } = default!;

    public AzurePipelinesTrigger Trigger { get; set; } = default!;

    public IEnumerable<AzurePipelinesStage> Stages { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        if(Variables.Any())
        {
            writer.WriteLine("variables:");
            Variables.OfType<CIVariable>()
                .ForEach(x =>
                {
                    writer.WriteLine($"- name: {x.Key}");
                    writer.WriteLine($"  value: {x.Value}");
                });
            Variables.OfType<CIVariableGroup>()
                .ForEach(x => writer.WriteLine($"- group: {x.Name}"));
        }

        Trigger.Write(writer);

        writer.WriteLine("stages:");
        Stages.ForEach(x => x.Write(writer));
    }
}
