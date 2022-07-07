using Nuke.Common.Utilities;
using Nuke.Common.IO;
using Nuke.Common.Execution;
using Nuke.Common.Utilities.Collections;
using System.Xml.Linq;

namespace AvantiPoint.Nuke.Maui.CI;

internal static class ICIModelExtensions
{
    internal static string JobName(this ICIJob job)
    {
        var split = job.Name.Contains(' ') ? job.Name.Split(' ') : job.Name.SplitCamelHumpsWithKnownWords();
        return split.JoinDash().ToLowerInvariant();
    }

    internal static string DisplayName(this ICIJob job) =>
        job.Name.Contains(' ') ? job.Name : job.Name.SplitCamelHumpsWithKnownWords().JoinSpace();

    internal static string StageName(this ICIStage stage, int index)
    {
        if (string.IsNullOrEmpty(stage.Name))
            return $"stage{index}";

        var split = stage.Name.Contains(' ') ? stage.Name.Split(' ') : stage.Name.SplitCamelHumpsWithKnownWords();
        return split.JoinDash().ToLowerInvariant();
    }

    internal static string DisplayName(this ICIStage stage, int index)
    {
        if (string.IsNullOrEmpty(stage.Name))
            return $"Stage {index}";

        return stage.Name.Contains(' ') ? stage.Name : stage.Name.SplitCamelHumpsWithKnownWords().JoinSpace();
    }

    internal static IEnumerable<AbsolutePath> GetArtifacts(this ICIJob job, IEnumerable<ExecutableTarget> targets)
    {
        return targets.Where(x => job.InvokedTargets.Any(name => name == x.Name))
               .SelectMany(x => x.ArtifactProducts)
               .Select(x => (AbsolutePath)x)
               // TODO: https://github.com/actions/upload-artifact/issues/11
               .Select(x => x.DescendantsAndSelf(y => y.Parent).FirstOrDefault(y => !y.ToString().ContainsOrdinalIgnoreCase("*")))
               .Distinct()
               .Where(x => x is not null)
               .Cast<AbsolutePath>();
    }

    internal static string GetRunCommand(this ICIJob job, string cmdPath)
    {
        if (!job.Image.ToString().StartsWith("Windows"))
            cmdPath = cmdPath.Replace(".cmd", ".sh");

        return $"./{cmdPath} {job.InvokedTargets.JoinSpace()}";
    }
}
