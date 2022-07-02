// Generated from https://raw.githubusercontent.com/AvantiPoint/nuke.maui/master/src/AvantiPoint.Nuke.Maui/Apple/Xcrun.json

using JetBrains.Annotations;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Tooling;
using Nuke.Common.Tools;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace AvantiPoint.Nuke.Maui.Apple;

/// <summary>
///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class XcrunTasks
{
    /// <summary>
    ///   Path to the Xcrun executable.
    /// </summary>
    public static string XcrunPath =>
        ToolPathResolver.TryGetEnvironmentExecutable("XCRUN_EXE") ??
        ToolPathResolver.GetPathExecutable("xcrun");
    public static Action<OutputType, string> XcrunLogger { get; set; } = ProcessTasks.DefaultLogger;
    /// <summary>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    public static IReadOnlyCollection<Output> Xcrun(string arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Func<string, string> outputFilter = null)
    {
        using var process = ProcessTasks.StartProcess(XcrunPath, arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, XcrunLogger, outputFilter);
        process.AssertZeroExitCode();
        return process.Output;
    }
    /// <summary>
    ///   <p>The <c>xcrun altool</c> is used to upload your IPA</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>--apiIssuer</c> via <see cref="XcrunSettings.IssuerId"/></li>
    ///     <li><c>--apiKey</c> via <see cref="XcrunSettings.ApiKey"/></li>
    ///     <li><c>--apple-id</c> via <see cref="XcrunSettings.AppleId"/></li>
    ///     <li><c>--asc-provider</c> via <see cref="XcrunSettings.AscProvider"/></li>
    ///     <li><c>--bundle-short-version-string</c> via <see cref="XcrunSettings.BundleShortVersion"/></li>
    ///     <li><c>--bundle-version</c> via <see cref="XcrunSettings.BundleVersion"/></li>
    ///     <li><c>--type</c> via <see cref="XcrunSettings.PlatformType"/></li>
    ///     <li><c>--upload-app -f</c> via <see cref="XcrunSettings.UploadApp"/></li>
    ///     <li><c>--upload-package</c> via <see cref="XcrunSettings.UploadPackage"/></li>
    ///     <li><c>--validate-app -f</c> via <see cref="XcrunSettings.ValidateApp"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> Xcrun(XcrunSettings toolSettings = null)
    {
        toolSettings = toolSettings ?? new XcrunSettings();
        using var process = ProcessTasks.StartProcess(toolSettings);
        process.AssertZeroExitCode();
        return process.Output;
    }
    /// <summary>
    ///   <p>The <c>xcrun altool</c> is used to upload your IPA</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>--apiIssuer</c> via <see cref="XcrunSettings.IssuerId"/></li>
    ///     <li><c>--apiKey</c> via <see cref="XcrunSettings.ApiKey"/></li>
    ///     <li><c>--apple-id</c> via <see cref="XcrunSettings.AppleId"/></li>
    ///     <li><c>--asc-provider</c> via <see cref="XcrunSettings.AscProvider"/></li>
    ///     <li><c>--bundle-short-version-string</c> via <see cref="XcrunSettings.BundleShortVersion"/></li>
    ///     <li><c>--bundle-version</c> via <see cref="XcrunSettings.BundleVersion"/></li>
    ///     <li><c>--type</c> via <see cref="XcrunSettings.PlatformType"/></li>
    ///     <li><c>--upload-app -f</c> via <see cref="XcrunSettings.UploadApp"/></li>
    ///     <li><c>--upload-package</c> via <see cref="XcrunSettings.UploadPackage"/></li>
    ///     <li><c>--validate-app -f</c> via <see cref="XcrunSettings.ValidateApp"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> Xcrun(Configure<XcrunSettings> configurator)
    {
        return Xcrun(configurator(new XcrunSettings()));
    }
    /// <summary>
    ///   <p>The <c>xcrun altool</c> is used to upload your IPA</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>--apiIssuer</c> via <see cref="XcrunSettings.IssuerId"/></li>
    ///     <li><c>--apiKey</c> via <see cref="XcrunSettings.ApiKey"/></li>
    ///     <li><c>--apple-id</c> via <see cref="XcrunSettings.AppleId"/></li>
    ///     <li><c>--asc-provider</c> via <see cref="XcrunSettings.AscProvider"/></li>
    ///     <li><c>--bundle-short-version-string</c> via <see cref="XcrunSettings.BundleShortVersion"/></li>
    ///     <li><c>--bundle-version</c> via <see cref="XcrunSettings.BundleVersion"/></li>
    ///     <li><c>--type</c> via <see cref="XcrunSettings.PlatformType"/></li>
    ///     <li><c>--upload-app -f</c> via <see cref="XcrunSettings.UploadApp"/></li>
    ///     <li><c>--upload-package</c> via <see cref="XcrunSettings.UploadPackage"/></li>
    ///     <li><c>--validate-app -f</c> via <see cref="XcrunSettings.ValidateApp"/></li>
    ///   </ul>
    /// </remarks>
    public static IEnumerable<(XcrunSettings Settings, IReadOnlyCollection<Output> Output)> Xcrun(CombinatorialConfigure<XcrunSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
    {
        return configurator.Invoke(Xcrun, XcrunLogger, degreeOfParallelism, completeOnFailure);
    }
}
#region XcrunSettings
/// <summary>
///   Used within <see cref="XcrunTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Serializable]
public partial class XcrunSettings : ToolSettings
{
    /// <summary>
    ///   Path to the Xcrun executable.
    /// </summary>
    public override string ProcessToolPath => base.ProcessToolPath ?? XcrunTasks.XcrunPath;
    public override Action<OutputType, string> ProcessCustomLogger => XcrunTasks.XcrunLogger;
    /// <summary>
    ///   Specifies the API Key generated from the Apple Auth Key
    /// </summary>
    public virtual string ApiKey { get; internal set; }
    /// <summary>
    ///   The Issuer Id from App Store Connect
    /// </summary>
    public virtual string IssuerId { get; internal set; }
    /// <summary>
    ///   Uploads the package at the specified path.
    /// </summary>
    public virtual string UploadPackage { get; internal set; }
    public virtual string ValidateApp { get; internal set; }
    public virtual string UploadApp { get; internal set; }
    public virtual string PlatformType { get; internal set; } = "ios";
    /// <summary>
    ///   Specifies the AppleID of the app package.
    /// </summary>
    public virtual string AppleId { get; internal set; }
    /// <summary>
    ///   Specifies the bundle version of the app package.
    /// </summary>
    public virtual string BundleVersion { get; internal set; }
    /// <summary>
    ///   Specifies the bundle short version string of the app package.
    /// </summary>
    public virtual string BundleShortVersion { get; internal set; }
    /// <summary>
    ///   Required with --notarize-app and --notarization-history when a user account is associated with multiple providers and using username/password authentication. You can use the --list-providers command to retrieve the providers associated with your account. You may instead use --team-id or --asc-public-id.
    /// </summary>
    public virtual string AscProvider { get; internal set; }
    protected override Arguments ConfigureProcessArguments(Arguments arguments)
    {
        arguments
          .Add("altool")
          .Add("--apiKey {value}", ApiKey)
          .Add("--apiIssuer {value}", IssuerId)
          .Add("--upload-package {value}", UploadPackage)
          .Add("--validate-app -f {value}", ValidateApp)
          .Add("--upload-app -f {value}", UploadApp)
          .Add("--type {value}", PlatformType)
          .Add("--apple-id {value}", AppleId)
          .Add("--bundle-version {value}", BundleVersion)
          .Add("--bundle-short-version-string {value}", BundleShortVersion)
          .Add("--asc-provider {value}", AscProvider);
        return base.ConfigureProcessArguments(arguments);
    }
}
#endregion
#region XcrunSettingsExtensions
/// <summary>
///   Used within <see cref="XcrunTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class XcrunSettingsExtensions
{
    #region ApiKey
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.ApiKey"/></em></p>
    ///   <p>Specifies the API Key generated from the Apple Auth Key</p>
    /// </summary>
    [Pure]
    public static T SetApiKey<T>(this T toolSettings, string apiKey) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiKey = apiKey;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.ApiKey"/></em></p>
    ///   <p>Specifies the API Key generated from the Apple Auth Key</p>
    /// </summary>
    [Pure]
    public static T ResetApiKey<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ApiKey = null;
        return toolSettings;
    }
    #endregion
    #region IssuerId
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.IssuerId"/></em></p>
    ///   <p>The Issuer Id from App Store Connect</p>
    /// </summary>
    [Pure]
    public static T SetIssuerId<T>(this T toolSettings, string issuerId) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.IssuerId = issuerId;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.IssuerId"/></em></p>
    ///   <p>The Issuer Id from App Store Connect</p>
    /// </summary>
    [Pure]
    public static T ResetIssuerId<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.IssuerId = null;
        return toolSettings;
    }
    #endregion
    #region UploadPackage
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.UploadPackage"/></em></p>
    ///   <p>Uploads the package at the specified path.</p>
    /// </summary>
    [Pure]
    public static T SetUploadPackage<T>(this T toolSettings, string uploadPackage) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.UploadPackage = uploadPackage;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.UploadPackage"/></em></p>
    ///   <p>Uploads the package at the specified path.</p>
    /// </summary>
    [Pure]
    public static T ResetUploadPackage<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.UploadPackage = null;
        return toolSettings;
    }
    #endregion
    #region ValidateApp
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.ValidateApp"/></em></p>
    /// </summary>
    [Pure]
    public static T SetValidateApp<T>(this T toolSettings, string validateApp) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ValidateApp = validateApp;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.ValidateApp"/></em></p>
    /// </summary>
    [Pure]
    public static T ResetValidateApp<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.ValidateApp = null;
        return toolSettings;
    }
    #endregion
    #region UploadApp
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.UploadApp"/></em></p>
    /// </summary>
    [Pure]
    public static T SetUploadApp<T>(this T toolSettings, string uploadApp) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.UploadApp = uploadApp;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.UploadApp"/></em></p>
    /// </summary>
    [Pure]
    public static T ResetUploadApp<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.UploadApp = null;
        return toolSettings;
    }
    #endregion
    #region PlatformType
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.PlatformType"/></em></p>
    /// </summary>
    [Pure]
    public static T SetPlatformType<T>(this T toolSettings, string platformType) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.PlatformType = platformType;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.PlatformType"/></em></p>
    /// </summary>
    [Pure]
    public static T ResetPlatformType<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.PlatformType = null;
        return toolSettings;
    }
    #endregion
    #region AppleId
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.AppleId"/></em></p>
    ///   <p>Specifies the AppleID of the app package.</p>
    /// </summary>
    [Pure]
    public static T SetAppleId<T>(this T toolSettings, string appleId) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppleId = appleId;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.AppleId"/></em></p>
    ///   <p>Specifies the AppleID of the app package.</p>
    /// </summary>
    [Pure]
    public static T ResetAppleId<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AppleId = null;
        return toolSettings;
    }
    #endregion
    #region BundleVersion
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.BundleVersion"/></em></p>
    ///   <p>Specifies the bundle version of the app package.</p>
    /// </summary>
    [Pure]
    public static T SetBundleVersion<T>(this T toolSettings, string bundleVersion) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.BundleVersion = bundleVersion;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.BundleVersion"/></em></p>
    ///   <p>Specifies the bundle version of the app package.</p>
    /// </summary>
    [Pure]
    public static T ResetBundleVersion<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.BundleVersion = null;
        return toolSettings;
    }
    #endregion
    #region BundleShortVersion
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.BundleShortVersion"/></em></p>
    ///   <p>Specifies the bundle short version string of the app package.</p>
    /// </summary>
    [Pure]
    public static T SetBundleShortVersion<T>(this T toolSettings, string bundleShortVersion) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.BundleShortVersion = bundleShortVersion;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.BundleShortVersion"/></em></p>
    ///   <p>Specifies the bundle short version string of the app package.</p>
    /// </summary>
    [Pure]
    public static T ResetBundleShortVersion<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.BundleShortVersion = null;
        return toolSettings;
    }
    #endregion
    #region AscProvider
    /// <summary>
    ///   <p><em>Sets <see cref="XcrunSettings.AscProvider"/></em></p>
    ///   <p>Required with --notarize-app and --notarization-history when a user account is associated with multiple providers and using username/password authentication. You can use the --list-providers command to retrieve the providers associated with your account. You may instead use --team-id or --asc-public-id.</p>
    /// </summary>
    [Pure]
    public static T SetAscProvider<T>(this T toolSettings, string ascProvider) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AscProvider = ascProvider;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="XcrunSettings.AscProvider"/></em></p>
    ///   <p>Required with --notarize-app and --notarization-history when a user account is associated with multiple providers and using username/password authentication. You can use the --list-providers command to retrieve the providers associated with your account. You may instead use --team-id or --asc-public-id.</p>
    /// </summary>
    [Pure]
    public static T ResetAscProvider<T>(this T toolSettings) where T : XcrunSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AscProvider = null;
        return toolSettings;
    }
    #endregion
}
#endregion
