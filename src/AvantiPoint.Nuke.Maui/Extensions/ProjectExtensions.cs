using Nuke.Common.ProjectModel;

namespace AvantiPoint.Nuke.Maui.Extensions;

internal static class ProjectExtensions
{
    public static string? GetTargetFramework(this Solution solution, string platform)
    {
        var projects = Path.GetExtension(solution.Path) == ".csproj" ?
            solution.Projects :
            solution.AllProjects;
        return projects
                .Select(p => p.GetTargetFramework(platform))
                .FirstOrDefault(tfm => !string.IsNullOrEmpty(tfm));
    }

    public static string? GetTargetFramework(this Project project, string platform) =>
        project.GetTargetFrameworks()
            .Distinct()
            .FirstOrDefault(tfm => tfm.EndsWith($"-{platform}"));
}
