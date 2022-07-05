using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Utilities;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

public class GitHubActionsUploadArtifactV3 : GitHubActionsStep
{
    public string Name { get; set; } = "drop";

    public string Path { get; set; } = "artifacts";

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine("- name: Upload Artifact");
        using (writer.Indent())
        {
            writer.WriteLine("uses: actions/upload-artifact@v3");
            writer.WriteLine("with:");
            using (writer.Indent())
            {
                writer.WriteLine($"name: {Name}");
                writer.WriteLine($"path: {Path}");
            }
        }
    }
}
