using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using Nuke.Components;
using Serilog;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IDotNetClean : IHazArtifacts, IHazConfiguration, IHazProject
{
    Target CleanArtifacts => _ => _
        .Executes(() =>
        {
            if (ArtifactsDirectory.DirectoryExists())
            {
                Log.Information("Deleting the Artifacts Directory");
                Directory.Delete(ArtifactsDirectory, true);
            }

            Directory.EnumerateDirectories(Project.Path / "obj")
                .ForEach(x => Directory.Delete(x, true));

            var bin = Project.Path / "bin";
            if (bin.Exists())
                Directory.Delete(bin, true);
        });
}
