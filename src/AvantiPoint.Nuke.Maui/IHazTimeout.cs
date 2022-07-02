using JetBrains.Annotations;

namespace AvantiPoint.Nuke.Maui;

[PublicAPI]
public interface IHazTimeout
{
    TimeSpan CompileTimeout => TimeSpan.FromMinutes(15);
}
