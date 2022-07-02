using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IDotNetRestore : IRestore, IHazSolution
{
    Target IRestore.Restore => _ => _
        .Executes(() => DotNetRestore(_ => _
            .SetProjectFile(Solution)));
}
