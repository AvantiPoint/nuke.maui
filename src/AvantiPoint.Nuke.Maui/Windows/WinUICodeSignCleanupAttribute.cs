using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Windows;

public class WinUICodeSignCleanupAttribute : BuildExtensionAttributeBase, IOnBuildFinished
{
    public void OnBuildFinished(NukeBuild build)
    {
        if(build is IWinUICodeSign &&
            build.SucceededTargets.Any(x => x.Name == nameof(IWinUICodeSign.CodeSignMsix)) &&
            WinUIAppSigning.CertificatePath.FileExists())
        {
            Log.Debug("Removing temporary Windows Signing Certificate");
            File.Delete(WinUIAppSigning.CertificatePath);
            Assert.False(WinUIAppSigning.CertificatePath.FileExists());
        }
    }
}
