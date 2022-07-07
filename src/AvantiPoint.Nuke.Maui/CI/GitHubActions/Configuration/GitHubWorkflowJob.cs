using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

internal class GitHubWorkflowJob : GitHubActionsJob
{
    public ICIJob Job { get; set; } = default!;
    public string[] Needs { get; set; } = Array.Empty<string>();

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"{Job.JobName()}:");

        using (writer.Indent())
        {
            writer.WriteLine($"name: {Job.DisplayName()}");
            if (Needs.Any())
            {
                if (Needs.Length == 1)
                    writer.WriteLine($"needs: {Needs[0]}");
                else
                    writer.WriteLine($"needs: [{Needs.JoinComma()}]");
            }
            writer.WriteLine($"runs-on: {Job.Image.GetValue()}");
            writer.WriteLine("steps:");
            using (writer.Indent())
            {
                Steps.ForEach(x => x.Write(writer));
            }
        }
    }
}
