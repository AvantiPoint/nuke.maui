using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.Tooling;

namespace AvantiPoint.Nuke.Maui.Apple;

[PublicAPI]
public interface IHazAppleSecurity : INukeBuild
{
    [PathExecutable("security")]
    Tool Security => TryGetValue(() => Security);
}
