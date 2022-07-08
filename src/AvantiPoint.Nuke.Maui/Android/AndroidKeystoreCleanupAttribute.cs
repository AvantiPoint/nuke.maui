using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Android;

public class AndroidKeystoreCleanupAttribute : BuildExtensionAttributeBase, IOnBuildFinished
{
    public void OnBuildFinished(NukeBuild build)
    {
        if(build is IHazAndroidKeystore android && build.FinishedTargets.Any(x => x.Name == nameof(IHazAndroidKeystore.RestoreKeystore)) && android.KeystorePath.FileExists())
        {
            File.Delete(android.KeystorePath);
            Log.Debug("Deleted temporary Android Keystore.");
        }
    }
}
