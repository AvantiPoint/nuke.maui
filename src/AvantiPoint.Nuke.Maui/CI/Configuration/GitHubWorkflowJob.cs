using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

internal class GitHubWorkflowJob : GitHubActionsJob
{
    public string[] Needs { get; set; } = Array.Empty<string>();

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"{Name}:");

        using (writer.Indent())
        {
            writer.WriteLine($"name: {Name}");
            if (Needs.Any())
            {
                if (Needs.Length == 1)
                    writer.WriteLine($"needs: {Needs[0]}");
                else
                    writer.WriteLine($"needs: [{string.Join(',', Needs)}]");
            }
            writer.WriteLine($"runs-on: {Image.GetValue()}");
            writer.WriteLine("steps:");
            using (writer.Indent())
            {
                Steps.ForEach(x => x.Write(writer));
            }
        }
    }
}
