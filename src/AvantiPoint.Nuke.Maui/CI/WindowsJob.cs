using AvantiPoint.Nuke.Maui.Windows;

namespace AvantiPoint.Nuke.Maui.CI;

public class WindowsJob : CIJobBase
{
    public static readonly WindowsJob Unsigned = new ();
    public static readonly WindowsJob LocallySigned = new (new()
    {
        nameof(IWinUICodeSign.PfxB64),
        nameof(IWinUICodeSign.PfxPassword)
    });
    public static readonly WindowsJob AzureSigned = new(new()
    {
        nameof(IWinUICodeSign.AzureKeyVault),
        nameof(IWinUICodeSign.AzureKeyVaultCertificate),
        nameof(IWinUICodeSign.AzureKeyVaultClientId),
        nameof(IWinUICodeSign.AzureKeyVaultClientSecret),
        nameof(IWinUICodeSign.AzureKeyVaultTenantId),
    });

    public WindowsJob()
        : this(new SecretImportCollection())
    {
    }

    public WindowsJob(SecretImportCollection importSecrets)
    {
        ImportSecrets = importSecrets;
    }

    public override string Name => "Windows Build";

    public override HostedAgent Image => HostedAgent.Windows;

    public override IEnumerable<string> InvokedTargets => new[]
    {
        nameof(IHazWinUIBuild.CompileWindows)
    };

    public override SecretImportCollection ImportSecrets { get; }
}
