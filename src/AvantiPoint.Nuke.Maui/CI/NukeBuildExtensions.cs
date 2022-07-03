using System.Reflection;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Execution;

namespace AvantiPoint.Nuke.Maui.CI;

internal static class NukeBuildExtensions
{
    public static IEnumerable<ExecutableTarget> ExecutableTargets(this NukeBuild build)
    {
        var type = typeof(NukeBuild);
        var prop = type.GetProperty("ExecutableTargets", BindingFlags.Instance | BindingFlags.NonPublic);
        var value = prop?.GetValue(build);
        if (value is IEnumerable<ExecutableTarget> targets)
            return targets;

        return Array.Empty<ExecutableTarget>();
    }
}
