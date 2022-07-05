using Nuke.Common;
using Serilog;

namespace AvantiPoint.Nuke.Maui;

public interface IEncodeFile : INukeBuild
{
    [Parameter]
    string InputFilePath => TryGetValue(() => InputFilePath);

    Target EncodeFile => _ => _
        .Description("Run this locally to help encode your files to base 64 regardless of which platform you're on.")
        .OnlyWhenDynamic(() => !IsLocalBuild && !string.IsNullOrEmpty(InputFilePath) && File.Exists(InputFilePath))
        .Executes(() =>
        {
            var data = File.ReadAllBytes(InputFilePath);
            var fileName = Path.GetFileNameWithoutExtension(InputFilePath);
            var encoded = Convert.ToBase64String(data);
            File.WriteAllText(TemporaryDirectory / $"{fileName}.b64", encoded);
            Log.Debug(encoded);
        });
}
