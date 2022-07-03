using JetBrains.Annotations;
using Nuke.Common.Tooling;

namespace AvantiPoint.Nuke.Maui.Extensions;

public static class ToolSettingsExtensions
{
    [Pure]
    public static T SetProcessExecutionTimeout<T>(this T toolSettings, TimeSpan timespan)
                where T : ToolSettings =>
        toolSettings.SetProcessExecutionTimeout((int)timespan.TotalMilliseconds);
}
