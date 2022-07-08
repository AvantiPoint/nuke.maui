using System.Text;
using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Components;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Android;

[PublicAPI]
public interface IHazAndroidKeystore : INukeBuild
{
    [Parameter("Android KeyStore must be Base64 Encoded"), Secret]
    string AndroidKeystoreB64 => TryGetValue(() => AndroidKeystoreB64);

    [Parameter("Android KeyStore name must be provided"), Secret]
    string AndroidKeystoreName => TryGetValue(() => AndroidKeystoreName);

    [Parameter("Android KeyStore must be provided"), Secret]
    string AndroidKeystorePassword => TryGetValue(() => AndroidKeystorePassword);

    AbsolutePath KeystorePath => TemporaryDirectory / (string.IsNullOrEmpty(AndroidKeystoreName) ? Guid.NewGuid().ToString() : $"{AndroidKeystoreName}.keystore");

    Target RestoreKeystore => _ => _
        .TryBefore<IDotNetRestore>()
        .TryBefore<IHazMauiWorkload>()
        .Unlisted()
        .Requires(() => AndroidKeystoreB64)
        .Requires(() => AndroidKeystoreName)
        .Requires(() => AndroidKeystorePassword)
        .Executes(() =>
        {
            if(KeystorePath.FileExists())
            {
                Log.Information("Keystore has already been restored");
                return;
            }

            Log.Debug("Restoring Android Keystore.");
            var contents = Encoding.Default.GetBytes(AndroidKeystoreB64!);
            File.WriteAllBytes(KeystorePath, contents);

            Assert.True(KeystorePath.FileExists(), "Something went wrong, the keystore could not be found at the expected location.");

            var keystoreName = KeystorePath.Name;
            Log.Debug("Android Keystore '{keystoreName}' has been restored.", keystoreName);
        });
}
