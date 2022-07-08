using Newtonsoft.Json.Linq;
using Nuke.Common.IO;
using Nuke.Common.Utilities;
using Serilog;

namespace AvantiPoint.Nuke.Maui;

internal static class WindowsWorkloadHelpers
{
    public const string EngineeringFeed = "https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet{0}/nuget/v3/index.json";

    public static AbsolutePath DotNetDirectory => (AbsolutePath)Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) / "dotnet";
    public static AbsolutePath SdkManifests => DotNetDirectory / "sdk-manifests";
    public static AbsolutePath Workloads => DotNetDirectory / "metadata" / "workloads";

    public static string UpdateManifest()
    {
        foreach(AbsolutePath workloadSdk in Directory.GetDirectories(Workloads))
        {
            var msiFile = workloadSdk / "installertype" / "msi";
            if (msiFile.FileExists())
            {
                Log.Debug($"Deleting msi installer type: {msiFile}");
                File.Delete(msiFile);
            }
        }

        var sdks = SdkManifests.GlobDirectories("*");
        var sources = sdks.Select(x => x.Name[0].ToString())
            .Where(x => int.TryParse(x, out var version) && version >= 6)
            .Distinct()
            .Select(x => string.Format(EngineeringFeed, x))
            .OfType<string>()
            .ToList();

        foreach(var sdk in sdks)
        {
            var manifestPath = sdk / "microsoft.net.workload.mono.toolchain" / "WorkloadManifest.json";
            if (manifestPath.FileExists())
            {
                var manifest = JObject.Parse(File.ReadAllText(manifestPath));
                if (manifest.ContainsKey("packs"))
                    continue;

                var packs = manifest["packs"];
                if (packs is null)
                    continue;

                foreach(var pack in packs)
                {
                    if (pack is not JObject packObj || !packObj.ContainsKey("alias-to"))
                        continue;

                    var alias = packObj["alias-to"] as JObject;
                    if (alias is null || !alias.ContainsKey("win-x64"))
                        continue;

                    alias["win-arm64"] = alias["win-x64"];
                }

                var updated = manifest.ToString(Newtonsoft.Json.Formatting.Indented);
                Log.Debug($"Updating: {manifestPath}");
                File.WriteAllText(manifestPath, updated);
            }
        }

        return sources.Select(x => $"--source {x}").JoinSpace();
    }
}
