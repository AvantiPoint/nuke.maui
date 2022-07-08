using JetBrains.Annotations;
using Nuke.Common;
using Serilog;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IHazMauiWorkload : INukeBuild
{
    Target InstallWorkload => _ => _
        .TryBefore<IDotNetRestore>()
        .Executes(() =>
        {
            if(!IsLocalBuild)
                DotNet("nuget locals all --clear");

            var output = DotNet("workload list");
            if (output.Any(x => x.Text.StartsWith("maui")))
            {
                Log.Information("MAUI Workload is already installed.");
                return;
            }

            var sources = string.Empty;
            if(EnvironmentInfo.IsWin)
            {
                sources = WindowsWorkloadHelpers.UpdateManifest();
                DotNet($"workload update {sources}");
                sources = $"--skip-manifest-update {sources} --source https://api.nuget.org/v3/index.json";
            }

            DotNet($"workload install maui {sources}");
            DotNet($"workload install android ios maccatalyst tvos macos maui wasm-tools maui-maccatalyst {sources}");
        });
}
