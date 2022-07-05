using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

internal class GitHubActionsDownloadArtifactStep : GitHubActionsStep
{
    public string ArtifactName { get; set; } = "";

    public override void Write(CustomFileWriter writer)
    {
        if (string.IsNullOrEmpty(ArtifactName))
            return;

        writer.WriteLine($"- name: Download '{ArtifactName}'");
        using(writer.Indent())
        {
            writer.WriteLine($"uses: actions/download-artifact@v3");
            writer.WriteLine("with:");
            using (writer.Indent())
            {
                writer.WriteLine($"name: {ArtifactName}");
                writer.WriteLine($"path: artifacts");
            }
        }
    }
}
