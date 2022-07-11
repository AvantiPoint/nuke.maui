using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Nuke.Common.Tooling;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

internal class GitHubActionsWorkflowConfiguration : ConfigurationEntity
{
    public string Name { get; set; } = default!;

    public GitHubActionsTrigger[] ShortTriggers { get; set; } = default!;
    public GitHubActionsDetailedTrigger[] DetailedTriggers { get; set; } = default!;
    public GitHubActionsJob[] Jobs { get; set; } = default!;
    public IEnumerable<CIVariable> Variables { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"name: {Name}");
        writer.WriteLine();

        if (ShortTriggers.Length > 0)
            writer.WriteLine($"on: [{ShortTriggers.Select(x => x.GetValue().ToLowerInvariant()).JoinCommaSpace()}]");
        else
        {
            writer.WriteLine("on:");
            using (writer.Indent())
            {
                DetailedTriggers.ForEach(x => x.Write(writer));
            }
        }

        writer.WriteLine();

        writer.WriteLine("env:");
        using (writer.Indent())
        {
            writer.WriteLine($"{DotNetEnvironment.SystemConsoleAllowAnsiColorRedirection}: true");
            writer.WriteLine($"{DotNetEnvironment.SkipFirstTimeExperience}: true");
            writer.WriteLine($"{DotNetEnvironment.NoLogo}: true");
            writer.WriteLine($"{DotNetEnvironment.CliTelemetryOutput}: true");
            if (Variables.Any())
                Variables.ForEach(x => writer.WriteLine($"{x.Key}: {x.Value}"));
        }

        writer.WriteLine();

        writer.WriteLine("jobs:");
        using (writer.Indent())
        {
            Jobs.ForEach(x => x.Write(writer));
        }
    }
}
