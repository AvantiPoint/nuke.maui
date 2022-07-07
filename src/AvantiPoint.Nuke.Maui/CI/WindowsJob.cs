using AvantiPoint.Nuke.Maui.Windows;

namespace AvantiPoint.Nuke.Maui.CI;

public class WindowsJob : CIJobBase
{
    public override string Name => "Windows Build";

    public override HostedAgent Image => HostedAgent.Windows;

    public override IEnumerable<string> InvokedTargets => new[]
    {
        nameof(IHazWinUIBuild.CompileWindows)
    };
}
