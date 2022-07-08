using Nuke.Common;
using Nuke.Common.IO;
using Serilog;

namespace AvantiPoint.Nuke.Maui;

public interface IEncodeFile : INukeBuild
{
    [Parameter]
    string InputFilePath => TryGetValue(() => InputFilePath);

    Target EncodeFile => _ => _
        .Description("Run this locally to help encode your files to base 64 regardless of which platform you're on.")
        .OnlyWhenStatic(() => IsLocalBuild)
        .Requires(() => InputFilePath)
        .Executes(() =>
        {
            Assert.True(File.Exists(InputFilePath), $"The file '{InputFilePath}' does not exist at the given path.");
            Log.Debug("Encoding file: {InputFilePath}", InputFilePath);
            var data = File.ReadAllBytes(InputFilePath);
            var fileName = Path.GetFileNameWithoutExtension(InputFilePath);
            var encoded = Convert.ToBase64String(data);
            var filePath = TemporaryDirectory / $"{fileName}.b64";
            var relativeOutputPath = EnvironmentInfo.WorkingDirectory.GetRelativePathTo(filePath);
            File.WriteAllText(filePath, encoded);
            Log.Debug("File has been encoded to: {relativeOutputPath}", relativeOutputPath);
        });
}
