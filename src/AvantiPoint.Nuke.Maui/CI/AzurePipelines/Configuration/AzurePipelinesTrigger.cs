using Nuke.Common.CI;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace AvantiPoint.Nuke.Maui.CI.AzurePipelines.Configuration;

internal class AzurePipelinesTrigger : ConfigurationEntity
{
    public CIBuild Build { get; set; } = default!;
    

    public override void Write(CustomFileWriter writer)
    {
        if(Build.OnPush is not null)
        {
            writer.WriteLine("trigger:");
            using (writer.Indent())
            {
                Write(writer, Build.OnPush.Disabled, Build.OnPush.Batch, Build.OnPush.AutoCancel, Build.OnPush.Branches, Build.OnPush.BranchesIgnore, Build.OnPush.IncludePaths, Build.OnPush.ExcludePaths);
            }
        }
        if(Build.OnPull is not null)
        {
            writer.WriteLine("pr:");
            using (writer.Indent())
            {
                Write(writer, Build.OnPull.Disabled, Build.OnPull.Batch, Build.OnPull.AutoCancel, Build.OnPull.Branches, Array.Empty<string>(), Build.OnPull.IncludePaths, Build.OnPull.ExcludePaths);
            }
        }

        //if(!string.IsNullOrEmpty(Build.OnCronSchedule))
        //{
        //    writer.WriteLine("schedules:");
        //    writer.WriteLine($"- cron: {Build.OnCronSchedule}");
        //}
    }

    private static void Write(CustomFileWriter writer, bool disabled, bool? batch, bool? autocancel, IEnumerable<string> branches, IEnumerable<string> ignoreBranches, IEnumerable<string> paths, IEnumerable<string> pathsIgnore)
    {
        if(disabled)
        {
            writer.WriteLine("none");
            return;
        }

        if (batch.HasValue)
            writer.WriteLine($"batch: {batch.Value.ToString().ToLowerInvariant()}");

        if (autocancel.HasValue)
            writer.WriteLine($"autoCancel: {autocancel.Value.ToString().ToLowerInvariant()}");

        if(branches.Any() || ignoreBranches.Any())
        {
            writer.WriteLine("branches:");
            using (writer.Indent())
            {
                WriteInclusionsAndExclusions(writer, branches, ignoreBranches);
            }
        }

        if(paths.Any() || pathsIgnore.Any())
        {
            writer.WriteLine("paths:");
            using (writer.Indent())
            {
                WriteInclusionsAndExclusions(writer, paths, pathsIgnore);
            }
        }
    }

    private static void WriteInclusionsAndExclusions(
            CustomFileWriter writer,
            IEnumerable<string> inclusions,
            IEnumerable<string> exclusions)
    {
        if (inclusions.Any())
        {
            writer.WriteLine("include:");
            //using (writer.Indent())
            {
                inclusions.ForEach(x => writer.WriteLine($"- {(x == "*" ? $"'{x}'" : x)}"));
            }
        }

        if (exclusions.Any())
        {
            //writer.WriteLine("exclude:");
            using (writer.Indent())
            {
                exclusions.ForEach(x => writer.WriteLine($"- {x}"));
            }
        }
    }
}
