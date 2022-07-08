using AvantiPoint.Nuke.Maui.Tools.Security;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Serilog;
using static AvantiPoint.Nuke.Maui.Tools.Security.SecurityTasks;

namespace AvantiPoint.Nuke.Maui.Apple;

public class AppleCertificateCleanupAttribute : BuildExtensionAttributeBase, IOnBuildFinished
{
    public void OnBuildFinished(NukeBuild build)
    {
        if(build is IHazAppleCertificate appCert && build.FinishedTargets.Any(x => x.Name == nameof(IHazAppleCertificate.RestoreIOSCertificate)) && appCert.KeychainPath.Exists())
        {
            try
            {
                SecurityDelete(_ => _.SetKeychain(appCert.KeychainPath));
            }
            finally
            {
                Log.Information("Removed Temporary Keychain");
            }
        }
    }
}
