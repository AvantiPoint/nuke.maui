using Nuke.Common.IO;
using Nuke.Common.Utilities.Collections;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Extensions;

public static class PathExtensions
{
    public static void CopyDirectory(this AbsolutePath source, AbsolutePath destination)
    {
        if (!destination.Exists())
        {
            Log.Debug("Creating directory: {source}", source);
            Directory.CreateDirectory(destination);
        }

        var dir = new DirectoryInfo(source);
        dir.EnumerateFiles()
            .ForEach(_ =>
            {
                Log.Debug("Copying file: {Name}", _.Name);
                _.CopyTo(destination / _.Name);
            });

        dir.EnumerateDirectories()
            .ForEach(_ => CopyDirectory((AbsolutePath)_.FullName, destination / _.Name));
    }
}
