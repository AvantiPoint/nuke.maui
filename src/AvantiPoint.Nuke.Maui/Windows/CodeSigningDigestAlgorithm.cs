using JetBrains.Annotations;
using System.ComponentModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.AzureSignTool;
using Nuke.Common.Tools.SignTool;
using Nuke.Components;

namespace AvantiPoint.Nuke.Maui.Windows;

[PublicAPI]
[TypeConverter(typeof(TypeConverter<Configuration>))]
public class CodeSigningDigestAlgorithm : Enumeration
{
    public static CodeSigningDigestAlgorithm SHA1 = new() { Value = "sha1" };
    public static CodeSigningDigestAlgorithm SHA256 = new() { Value = "sha256" };
    public static CodeSigningDigestAlgorithm SHA384 = new() { Value = "sha384" };
    public static CodeSigningDigestAlgorithm SHA512 = new() { Value = "sha512" };

    public static implicit operator SignToolDigestAlgorithm(CodeSigningDigestAlgorithm? algorithm) =>
        algorithm?.Value switch
        {
            "sha1" => SignToolDigestAlgorithm.SHA1,
            "sha384" => throw new NotSupportedException("The Windows Sign Tool does not support SHA384"),
            "sha512" => throw new NotSupportedException("The Windows Sign Tool does not support SHA512"),
            _ => SignToolDigestAlgorithm.SHA256
        };

    public static implicit operator AzureSignToolDigestAlgorithm(CodeSigningDigestAlgorithm? algorithm) =>
        algorithm?.Value switch
        {
            "sha1" => AzureSignToolDigestAlgorithm.sha1,
            "sha384" => AzureSignToolDigestAlgorithm.sha384,
            "sha512" => AzureSignToolDigestAlgorithm.sha512,
            _ => AzureSignToolDigestAlgorithm.sha256
        };
}
