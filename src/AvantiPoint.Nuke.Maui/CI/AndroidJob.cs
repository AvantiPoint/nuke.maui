using AvantiPoint.Nuke.Maui.Android;

namespace AvantiPoint.Nuke.Maui.CI;

public class AndroidJob : CIJobBase
{
    public override string Name => "Android Build";

    public override HostedAgent Image => HostedAgent.Windows;

    public override SecretImportCollection ImportSecrets => new()
    {
        nameof(IHazAndroidKeystore.AndroidKeystoreName),
        nameof(IHazAndroidKeystore.AndroidKeystoreB64),
        nameof(IHazAndroidKeystore.AndroidKeystorePassword)
    };

    public override IEnumerable<string> InvokedTargets => new[]
    {
        nameof(IHazAndroidBuild.CompileAndroid)
    };
}
