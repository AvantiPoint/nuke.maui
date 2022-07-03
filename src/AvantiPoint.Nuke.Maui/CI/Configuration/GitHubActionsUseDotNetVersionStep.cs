using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

internal class GitHubActionsUseDotNetVersionStep : GitHubActionsStep
{
    public string[] Sdks { get; set; } = Array.Empty<string>();

    public override void Write(CustomFileWriter writer)
    {
        if (!Sdks.Any())
            return;

        writer.WriteLine($"- name: 'Setup .NET {Sdks.JoinComma()}'");
        using (writer.Indent())
        {
            writer.WriteLine("uses: actions/setup-dotnet@v2");
            writer.WriteLine("with:");
            using (writer.Indent())
            {
                if (Sdks.Length == 1)
                    writer.WriteLine($"dotnet-version: {Sdks[0]}");
                else
                {
                    writer.WriteLine("dotnet-version: |");
                    using (writer.Indent())
                    {
                        foreach (var sdk in Sdks)
                            writer.WriteLine(sdk);
                    }
                }
            }
        }
    }
}
