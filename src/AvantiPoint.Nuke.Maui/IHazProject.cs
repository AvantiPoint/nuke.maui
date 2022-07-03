using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Components;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IHazProject : IHazSolution
{
    [Parameter("The name of the MAUI Single Project to build.")]
    string ProjectName => TryGetValue(() => ProjectName);

    Project Project => Solution.AllProjects.First(x => x.Name == ProjectName);
}
