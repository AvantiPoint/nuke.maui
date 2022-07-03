using System.Linq;
using AvantiPoint.Nuke.Maui;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

public interface ICompileLibrary : IHazArtifacts, IHazConfiguration, IHazSolution
{
    Target CompileLib => _ => _
        .DependsOn<IDotNetRestore>()
        .DependsOn<IHazMauiWorkload>()
        .Produces(ArtifactsDirectory)
        .Executes(() =>
        {
            var project = Solution.AllProjects.FirstOrDefault(x => x.Name.EndsWith("Nuke.Maui"));
            project.NotNull("Could not locate the Nuke Maui Project");

            DotNetBuild(settings => settings
                .SetProjectFile(project)
                .SetConfiguration(Configuration)
                .SetDeterministic(!IsLocalBuild));
        });
}
