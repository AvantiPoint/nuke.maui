using System.Linq;
using AvantiPoint.Nuke.Maui;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

public interface ICompileLibrary : IHazArtifacts, IHazConfiguration, IHazProject
{
    Target CompileLib => _ => _
        .DependsOn<IDotNetRestore>()
        .Produces(ArtifactsDirectory)
        .Executes(() =>
        {
            Project.NotNull("Could not locate a project");
            Assert.True("AvantiPoint.Nuke.Maui" == Project.Name, "The selected project is not the Nuke.Maui Project");

            DotNetBuild(settings => settings
                .SetProjectFile(Project)
                .SetConfiguration(Configuration)
                .SetDeterministic(!IsLocalBuild));
        });
}
