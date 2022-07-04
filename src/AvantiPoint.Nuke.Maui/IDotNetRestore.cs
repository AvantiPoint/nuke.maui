using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IDotNetRestore : IHazProject
{
    Target Restore => _ => _
        .DependsOn<IDotNetClean>()
        .Executes(() => DotNetRestore(_ => _
            .SetProjectFile(Project)));
}
