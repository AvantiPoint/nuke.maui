using Nuke.Common.ProjectModel;

namespace AvantiPoint.Nuke.Maui.Extensions;

internal static class ProjectExtensions
{
    public static string? GetTargetFramework(this Solution solution, string platform) =>
        solution.AllProjects
                .SelectMany(p => p.GetTargetFrameworks())
                .Distinct()
                .FirstOrDefault(tfm => tfm.Contains($"-{platform}"));
}
