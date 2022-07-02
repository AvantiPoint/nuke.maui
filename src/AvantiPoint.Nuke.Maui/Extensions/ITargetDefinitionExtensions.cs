using Nuke.Common;

namespace AvantiPoint.Nuke.Maui.Extensions;

internal static class ITargetDefinitionExtensions
{
    public static ITargetDefinition OnlyOnMacHost(this ITargetDefinition target) =>
        target.OnlyWhenStatic(() => EnvironmentInfo.Platform == PlatformFamily.OSX);

    public static ITargetDefinition BeforeMauiWorkload(this ITargetDefinition target) =>
        target.TryBefore<IHazMauiWorkload>();
}
