using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

internal class GitHubActionsCacheStepV3 : GitHubActionsCacheStep
{
    public string JobName { get; set; } = "";

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"- name: Cache {IncludePatterns.JoinCommaSpace()}");
        using (writer.Indent())
        {
            writer.WriteLine("uses: actions/cache@v2");
            writer.WriteLine("with:");
            using (writer.Indent())
            {
                writer.WriteLine("path: |");
                using (writer.Indent())
                {
                    IncludePatterns.ForEach(x => writer.WriteLine($"{x}"));
                    ExcludePatterns.ForEach(x => writer.WriteLine($"!{x}"));
                }
                writer.WriteLine($"key: {JobName}-${{{{ runner.os }}}}-${{{{ hashFiles({KeyFiles.Select(x => x.SingleQuote()).JoinCommaSpace()}) }}}}");
            }
        }
    }
}
