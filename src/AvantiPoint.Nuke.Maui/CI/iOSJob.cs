using AvantiPoint.Nuke.Maui.Apple;

namespace AvantiPoint.Nuke.Maui.CI;

public class iOSJob : CIJobBase
{
    public override string Name => "iOS Build";

    public override HostedAgent Image => HostedAgent.Mac;

    public override IEnumerable<string> InvokedTargets => new[] { nameof(IHazIOSBuild.CompileIos) };

    public override SecretImportCollection ImportSecrets => new()
    {
        nameof(IHazAppleCertificate.P12B64),
        nameof(IHazAppleCertificate.P12Password),
        nameof(IRestoreAppleProvisioningProfile.AppleIssuerId),
        nameof(IRestoreAppleProvisioningProfile.AppleKeyId),
        nameof(IRestoreAppleProvisioningProfile.AppleAuthKeyP8),
        { nameof(IRestoreAppleProvisioningProfile.AppleProfileId), "IOS_PROVISIONING_PROFILE" }
    };

    public override IEnumerable<string> DotNetSdks => new[] { "6.0.x" };
}
