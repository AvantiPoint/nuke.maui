using System.ComponentModel;
using Nuke.Common.Tooling;

namespace AvantiPoint.Nuke.Maui.Apple;

[TypeConverter(typeof(TypeConverter<MtouchLink>))]
public class MtouchLink : Enumeration
{
    public static MtouchLink None = new() { Value = nameof(None) };

    public static MtouchLink SdkOnly = new() { Value = nameof(SdkOnly) };

    public static MtouchLink Full = new() { Value = nameof(Full) };

    public static implicit operator string(MtouchLink link) =>
        link.Value;
}
