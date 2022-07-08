using Nuke.Common.Tooling;

namespace AvantiPoint.Nuke.Maui.CI;

public enum HostedAgent
{
    [EnumValue("windows-latest")]
    Windows,

    [EnumValue("macos-12")]
    Mac,

    [EnumValue("ubuntu-latest")]
    Linux,
}
