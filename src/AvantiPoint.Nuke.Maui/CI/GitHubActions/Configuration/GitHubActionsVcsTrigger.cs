using Nuke.Common.CI.GitHubActions;
using Nuke.Common.CI.GitHubActions.Configuration;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.Configuration;

internal class GitHubActionsVcsTrigger : GitHubActionsDetailedTrigger
{
    public GitHubActionsTrigger Kind { get; set; }
    public string[] Branches { get; set; } = default!;
    public string[] BranchesIgnore { get; set; } = default!;
    public string[] Tags { get; set; } = default!;
    public string[] TagsIgnore { get; set; } = default!;
    public string[] IncludePaths { get; set; } = default!;
    public string[] ExcludePaths { get; set; } = default!;

    public override void Write(CustomFileWriter writer)
    {
        writer.WriteLine($"{Kind.GetValue()}:");

        void WriteValue(string value) => writer.WriteLine($"- {value.SingleQuoteIfNeeded('*', '!')}");

        using (writer.Indent())
        {
            if (Branches.Length > 0)
            {
                writer.WriteLine("branches:");
                using (writer.Indent())
                {
                    Branches.ForEach(WriteValue);
                }
            }

            if (BranchesIgnore.Length > 0)
            {
                writer.WriteLine("branches-ignore:");
                using (writer.Indent())
                {
                    BranchesIgnore.ForEach(WriteValue);
                }
            }

            if (Tags.Length > 0)
            {
                writer.WriteLine("tags:");
                using (writer.Indent())
                {
                    Tags.ForEach(WriteValue);
                }
            }

            if (TagsIgnore.Length > 0)
            {
                writer.WriteLine("tags-ignore:");
                using (writer.Indent())
                {
                    TagsIgnore.ForEach(WriteValue);
                }
            }

            if (IncludePaths.Length > 0)
            {
                writer.WriteLine("paths:");
                using (writer.Indent())
                {
                    IncludePaths.ForEach(WriteValue);
                }
            }

            if(ExcludePaths.Length > 0)
            {
                writer.WriteLine("paths-ignore:");
                using (writer.Indent())
                {
                    ExcludePaths.ForEach(WriteValue);
                }
            }
        }
    }
}
