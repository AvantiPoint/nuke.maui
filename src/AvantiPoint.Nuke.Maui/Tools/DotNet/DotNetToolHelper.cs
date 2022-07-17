using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui.Tools.DotNet;

public static class DotNetToolHelper
{
    public static void EnsureInstalled(string packageName)
    {
        var output = DotNetTasks.DotNet("tool list --global");
        if (output.Any(x => x.Type == OutputType.Std && x.Text.Contains(packageName, StringComparison.InvariantCultureIgnoreCase)))
            return;

        DotNetToolInstall(_ => _
            .SetGlobal(true)
            .SetPackageName(packageName));
    }
}
