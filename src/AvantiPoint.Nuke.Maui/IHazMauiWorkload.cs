using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Components;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IHazMauiWorkload : INukeBuild
{
    Target InstallWorkload => _ => _
        .TryBefore<IRestore>()
        .Executes(() =>
        {
            var output = DotNet("workload list");
            if (output.Any(x => x.Text.StartsWith("maui")))
            {
                Log.Information("MAUI Workload is already installed.");
                return;
            }

            DotNet("workload install maui");
        });
}
