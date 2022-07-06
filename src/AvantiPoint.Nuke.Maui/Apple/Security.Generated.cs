// Generated from https://raw.githubusercontent.com/AvantiPoint/nuke.maui/master/src/AvantiPoint.Nuke.Maui/Apple/Security.json

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
public static partial class SecurityTasks
{
    /// <summary>
    ///   Path to the Security executable.
    /// </summary>
    public static string SecurityPath =>
        ToolPathResolver.TryGetEnvironmentExecutable("SECURITY_EXE") ??
        ToolPathResolver.GetPathExecutable("security");
    public static Action<OutputType, string> SecurityLogger { get; set; } = ProcessTasks.DefaultLogger;
    /// <summary>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    public static IReadOnlyCollection<Output> Security(string arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Func<string, string> outputFilter = null)
    {
        using var process = ProcessTasks.StartProcess(SecurityPath, arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, SecurityLogger, outputFilter);
        process.AssertZeroExitCode();
        return process.Output;
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to create a new keychain.</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;keychain&gt;</c> via <see cref="SecurityCreateKeychainSettings.Keychain"/></li>
    ///     <li><c>-p</c> via <see cref="SecurityCreateKeychainSettings.Password"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> SecurityCreateKeychain(SecurityCreateKeychainSettings toolSettings = null)
    {
        toolSettings = toolSettings ?? new SecurityCreateKeychainSettings();
        using var process = ProcessTasks.StartProcess(toolSettings);
        process.AssertZeroExitCode();
        return process.Output;
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to create a new keychain.</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;keychain&gt;</c> via <see cref="SecurityCreateKeychainSettings.Keychain"/></li>
    ///     <li><c>-p</c> via <see cref="SecurityCreateKeychainSettings.Password"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> SecurityCreateKeychain(Configure<SecurityCreateKeychainSettings> configurator)
    {
        return SecurityCreateKeychain(configurator(new SecurityCreateKeychainSettings()));
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to create a new keychain.</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;keychain&gt;</c> via <see cref="SecurityCreateKeychainSettings.Keychain"/></li>
    ///     <li><c>-p</c> via <see cref="SecurityCreateKeychainSettings.Password"/></li>
    ///   </ul>
    /// </remarks>
    public static IEnumerable<(SecurityCreateKeychainSettings Settings, IReadOnlyCollection<Output> Output)> SecurityCreateKeychain(CombinatorialConfigure<SecurityCreateKeychainSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
    {
        return configurator.Invoke(SecurityCreateKeychain, SecurityLogger, degreeOfParallelism, completeOnFailure);
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to unlock the keychain</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;keychain&gt;</c> via <see cref="SecurityUnlockKeychainSettings.Keychain"/></li>
    ///     <li><c>-p</c> via <see cref="SecurityUnlockKeychainSettings.Password"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> SecurityUnlockKeychain(SecurityUnlockKeychainSettings toolSettings = null)
    {
        toolSettings = toolSettings ?? new SecurityUnlockKeychainSettings();
        using var process = ProcessTasks.StartProcess(toolSettings);
        process.AssertZeroExitCode();
        return process.Output;
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to unlock the keychain</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;keychain&gt;</c> via <see cref="SecurityUnlockKeychainSettings.Keychain"/></li>
    ///     <li><c>-p</c> via <see cref="SecurityUnlockKeychainSettings.Password"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> SecurityUnlockKeychain(Configure<SecurityUnlockKeychainSettings> configurator)
    {
        return SecurityUnlockKeychain(configurator(new SecurityUnlockKeychainSettings()));
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to unlock the keychain</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;keychain&gt;</c> via <see cref="SecurityUnlockKeychainSettings.Keychain"/></li>
    ///     <li><c>-p</c> via <see cref="SecurityUnlockKeychainSettings.Password"/></li>
    ///   </ul>
    /// </remarks>
    public static IEnumerable<(SecurityUnlockKeychainSettings Settings, IReadOnlyCollection<Output> Output)> SecurityUnlockKeychain(CombinatorialConfigure<SecurityUnlockKeychainSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
    {
        return configurator.Invoke(SecurityUnlockKeychain, SecurityLogger, degreeOfParallelism, completeOnFailure);
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to import a p12 certificate</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;certificatePath&gt;</c> via <see cref="SecurityImportSettings.CertificatePath"/></li>
    ///     <li><c>-A</c> via <see cref="SecurityImportSettings.AllowAny"/></li>
    ///     <li><c>-f</c> via <see cref="SecurityImportSettings.Format"/></li>
    ///     <li><c>-k</c> via <see cref="SecurityImportSettings.KeychainPath"/></li>
    ///     <li><c>-P</c> via <see cref="SecurityImportSettings.Password"/></li>
    ///     <li><c>-t</c> via <see cref="SecurityImportSettings.Type"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> SecurityImport(SecurityImportSettings toolSettings = null)
    {
        toolSettings = toolSettings ?? new SecurityImportSettings();
        using var process = ProcessTasks.StartProcess(toolSettings);
        process.AssertZeroExitCode();
        return process.Output;
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to import a p12 certificate</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;certificatePath&gt;</c> via <see cref="SecurityImportSettings.CertificatePath"/></li>
    ///     <li><c>-A</c> via <see cref="SecurityImportSettings.AllowAny"/></li>
    ///     <li><c>-f</c> via <see cref="SecurityImportSettings.Format"/></li>
    ///     <li><c>-k</c> via <see cref="SecurityImportSettings.KeychainPath"/></li>
    ///     <li><c>-P</c> via <see cref="SecurityImportSettings.Password"/></li>
    ///     <li><c>-t</c> via <see cref="SecurityImportSettings.Type"/></li>
    ///   </ul>
    /// </remarks>
    public static IReadOnlyCollection<Output> SecurityImport(Configure<SecurityImportSettings> configurator)
    {
        return SecurityImport(configurator(new SecurityImportSettings()));
    }
    /// <summary>
    ///   <p>Uses the <c>security</c> tool to import a p12 certificate</p>
    ///   <p>For more details, visit the <a href="https://developer.apple.com">official website</a>.</p>
    /// </summary>
    /// <remarks>
    ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
    ///   <ul>
    ///     <li><c>&lt;certificatePath&gt;</c> via <see cref="SecurityImportSettings.CertificatePath"/></li>
    ///     <li><c>-A</c> via <see cref="SecurityImportSettings.AllowAny"/></li>
    ///     <li><c>-f</c> via <see cref="SecurityImportSettings.Format"/></li>
    ///     <li><c>-k</c> via <see cref="SecurityImportSettings.KeychainPath"/></li>
    ///     <li><c>-P</c> via <see cref="SecurityImportSettings.Password"/></li>
    ///     <li><c>-t</c> via <see cref="SecurityImportSettings.Type"/></li>
    ///   </ul>
    /// </remarks>
    public static IEnumerable<(SecurityImportSettings Settings, IReadOnlyCollection<Output> Output)> SecurityImport(CombinatorialConfigure<SecurityImportSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
    {
        return configurator.Invoke(SecurityImport, SecurityLogger, degreeOfParallelism, completeOnFailure);
    }
}
#region SecurityCreateKeychainSettings
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Serializable]
public partial class SecurityCreateKeychainSettings : ToolSettings
{
    /// <summary>
    ///   Path to the Security executable.
    /// </summary>
    public override string ProcessToolPath => base.ProcessToolPath ?? SecurityTasks.SecurityPath;
    public override Action<OutputType, string> ProcessCustomLogger => SecurityTasks.SecurityLogger;
    /// <summary>
    ///   Use "password" as the password for the keychains being created
    /// </summary>
    public virtual string Password { get; internal set; }
    /// <summary>
    ///   Keychain Path
    /// </summary>
    public virtual string Keychain { get; internal set; }
    protected override Arguments ConfigureProcessArguments(Arguments arguments)
    {
        arguments
          .Add("create-keychain")
          .Add("-p {value}", Password, secret: true)
          .Add("{value}", Keychain);
        return base.ConfigureProcessArguments(arguments);
    }
}
#endregion
#region SecurityUnlockKeychainSettings
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Serializable]
public partial class SecurityUnlockKeychainSettings : ToolSettings
{
    /// <summary>
    ///   Path to the Security executable.
    /// </summary>
    public override string ProcessToolPath => base.ProcessToolPath ?? SecurityTasks.SecurityPath;
    public override Action<OutputType, string> ProcessCustomLogger => SecurityTasks.SecurityLogger;
    /// <summary>
    ///   Use "password" as the password to unlock the keychain
    /// </summary>
    public virtual string Password { get; internal set; }
    /// <summary>
    ///   Keychain Path
    /// </summary>
    public virtual string Keychain { get; internal set; }
    protected override Arguments ConfigureProcessArguments(Arguments arguments)
    {
        arguments
          .Add("unlock-keychain")
          .Add("-p {value}", Password, secret: true)
          .Add("{value}", Keychain);
        return base.ConfigureProcessArguments(arguments);
    }
}
#endregion
#region SecurityImportSettings
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
[Serializable]
public partial class SecurityImportSettings : ToolSettings
{
    /// <summary>
    ///   Path to the Security executable.
    /// </summary>
    public override string ProcessToolPath => base.ProcessToolPath ?? SecurityTasks.SecurityPath;
    public override Action<OutputType, string> ProcessCustomLogger => SecurityTasks.SecurityLogger;
    /// <summary>
    ///   The path to the certificate to import
    /// </summary>
    public virtual string CertificatePath { get; internal set; }
    /// <summary>
    ///   The p12 certificate password
    /// </summary>
    public virtual string Password { get; internal set; }
    /// <summary>
    ///   Allow any application to access the imported key without warning (insecure, not recommended!)
    /// </summary>
    public virtual bool? AllowAny { get; internal set; }
    /// <summary>
    ///   Type = pub|priv|session|cert|agg
    /// </summary>
    public virtual AppleCertificateType Type { get; internal set; }
    /// <summary>
    ///   Format = openssl|openssh1|openssh2|bsafe|raw|pkcs7|pkcs8|pkcs12|netscape|pemseq
    /// </summary>
    public virtual AppleCertificateFormat Format { get; internal set; }
    /// <summary>
    ///   The path to the keychain to import the certificate into
    /// </summary>
    public virtual string KeychainPath { get; internal set; }
    protected override Arguments ConfigureProcessArguments(Arguments arguments)
    {
        arguments
          .Add("import")
          .Add("{value}", CertificatePath)
          .Add("-P {value}", Password, secret: true)
          .Add("-A", AllowAny)
          .Add("-t {value}", Type)
          .Add("-f {value}", Format)
          .Add("-k {value}", KeychainPath);
        return base.ConfigureProcessArguments(arguments);
    }
}
#endregion
#region SecurityCreateKeychainSettingsExtensions
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class SecurityCreateKeychainSettingsExtensions
{
    #region Password
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityCreateKeychainSettings.Password"/></em></p>
    ///   <p>Use "password" as the password for the keychains being created</p>
    /// </summary>
    [Pure]
    public static T SetPassword<T>(this T toolSettings, [Secret] string password) where T : SecurityCreateKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Password = password;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityCreateKeychainSettings.Password"/></em></p>
    ///   <p>Use "password" as the password for the keychains being created</p>
    /// </summary>
    [Pure]
    public static T ResetPassword<T>(this T toolSettings) where T : SecurityCreateKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Password = null;
        return toolSettings;
    }
    #endregion
    #region Keychain
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityCreateKeychainSettings.Keychain"/></em></p>
    ///   <p>Keychain Path</p>
    /// </summary>
    [Pure]
    public static T SetKeychain<T>(this T toolSettings, string keychain) where T : SecurityCreateKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Keychain = keychain;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityCreateKeychainSettings.Keychain"/></em></p>
    ///   <p>Keychain Path</p>
    /// </summary>
    [Pure]
    public static T ResetKeychain<T>(this T toolSettings) where T : SecurityCreateKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Keychain = null;
        return toolSettings;
    }
    #endregion
}
#endregion
#region SecurityUnlockKeychainSettingsExtensions
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class SecurityUnlockKeychainSettingsExtensions
{
    #region Password
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityUnlockKeychainSettings.Password"/></em></p>
    ///   <p>Use "password" as the password to unlock the keychain</p>
    /// </summary>
    [Pure]
    public static T SetPassword<T>(this T toolSettings, [Secret] string password) where T : SecurityUnlockKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Password = password;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityUnlockKeychainSettings.Password"/></em></p>
    ///   <p>Use "password" as the password to unlock the keychain</p>
    /// </summary>
    [Pure]
    public static T ResetPassword<T>(this T toolSettings) where T : SecurityUnlockKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Password = null;
        return toolSettings;
    }
    #endregion
    #region Keychain
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityUnlockKeychainSettings.Keychain"/></em></p>
    ///   <p>Keychain Path</p>
    /// </summary>
    [Pure]
    public static T SetKeychain<T>(this T toolSettings, string keychain) where T : SecurityUnlockKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Keychain = keychain;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityUnlockKeychainSettings.Keychain"/></em></p>
    ///   <p>Keychain Path</p>
    /// </summary>
    [Pure]
    public static T ResetKeychain<T>(this T toolSettings) where T : SecurityUnlockKeychainSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Keychain = null;
        return toolSettings;
    }
    #endregion
}
#endregion
#region SecurityImportSettingsExtensions
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[ExcludeFromCodeCoverage]
public static partial class SecurityImportSettingsExtensions
{
    #region CertificatePath
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityImportSettings.CertificatePath"/></em></p>
    ///   <p>The path to the certificate to import</p>
    /// </summary>
    [Pure]
    public static T SetCertificatePath<T>(this T toolSettings, string certificatePath) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.CertificatePath = certificatePath;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityImportSettings.CertificatePath"/></em></p>
    ///   <p>The path to the certificate to import</p>
    /// </summary>
    [Pure]
    public static T ResetCertificatePath<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.CertificatePath = null;
        return toolSettings;
    }
    #endregion
    #region Password
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityImportSettings.Password"/></em></p>
    ///   <p>The p12 certificate password</p>
    /// </summary>
    [Pure]
    public static T SetPassword<T>(this T toolSettings, [Secret] string password) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Password = password;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityImportSettings.Password"/></em></p>
    ///   <p>The p12 certificate password</p>
    /// </summary>
    [Pure]
    public static T ResetPassword<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Password = null;
        return toolSettings;
    }
    #endregion
    #region AllowAny
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityImportSettings.AllowAny"/></em></p>
    ///   <p>Allow any application to access the imported key without warning (insecure, not recommended!)</p>
    /// </summary>
    [Pure]
    public static T SetAllowAny<T>(this T toolSettings, bool? allowAny) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AllowAny = allowAny;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityImportSettings.AllowAny"/></em></p>
    ///   <p>Allow any application to access the imported key without warning (insecure, not recommended!)</p>
    /// </summary>
    [Pure]
    public static T ResetAllowAny<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AllowAny = null;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Enables <see cref="SecurityImportSettings.AllowAny"/></em></p>
    ///   <p>Allow any application to access the imported key without warning (insecure, not recommended!)</p>
    /// </summary>
    [Pure]
    public static T EnableAllowAny<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AllowAny = true;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Disables <see cref="SecurityImportSettings.AllowAny"/></em></p>
    ///   <p>Allow any application to access the imported key without warning (insecure, not recommended!)</p>
    /// </summary>
    [Pure]
    public static T DisableAllowAny<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AllowAny = false;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Toggles <see cref="SecurityImportSettings.AllowAny"/></em></p>
    ///   <p>Allow any application to access the imported key without warning (insecure, not recommended!)</p>
    /// </summary>
    [Pure]
    public static T ToggleAllowAny<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.AllowAny = !toolSettings.AllowAny;
        return toolSettings;
    }
    #endregion
    #region Type
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityImportSettings.Type"/></em></p>
    ///   <p>Type = pub|priv|session|cert|agg</p>
    /// </summary>
    [Pure]
    public static T SetType<T>(this T toolSettings, AppleCertificateType type) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Type = type;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityImportSettings.Type"/></em></p>
    ///   <p>Type = pub|priv|session|cert|agg</p>
    /// </summary>
    [Pure]
    public static T ResetType<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Type = null;
        return toolSettings;
    }
    #endregion
    #region Format
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityImportSettings.Format"/></em></p>
    ///   <p>Format = openssl|openssh1|openssh2|bsafe|raw|pkcs7|pkcs8|pkcs12|netscape|pemseq</p>
    /// </summary>
    [Pure]
    public static T SetFormat<T>(this T toolSettings, AppleCertificateFormat format) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Format = format;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityImportSettings.Format"/></em></p>
    ///   <p>Format = openssl|openssh1|openssh2|bsafe|raw|pkcs7|pkcs8|pkcs12|netscape|pemseq</p>
    /// </summary>
    [Pure]
    public static T ResetFormat<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.Format = null;
        return toolSettings;
    }
    #endregion
    #region KeychainPath
    /// <summary>
    ///   <p><em>Sets <see cref="SecurityImportSettings.KeychainPath"/></em></p>
    ///   <p>The path to the keychain to import the certificate into</p>
    /// </summary>
    [Pure]
    public static T SetKeychainPath<T>(this T toolSettings, string keychainPath) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.KeychainPath = keychainPath;
        return toolSettings;
    }
    /// <summary>
    ///   <p><em>Resets <see cref="SecurityImportSettings.KeychainPath"/></em></p>
    ///   <p>The path to the keychain to import the certificate into</p>
    /// </summary>
    [Pure]
    public static T ResetKeychainPath<T>(this T toolSettings) where T : SecurityImportSettings
    {
        toolSettings = toolSettings.NewInstance();
        toolSettings.KeychainPath = null;
        return toolSettings;
    }
    #endregion
}
#endregion
#region AppleCertificateType
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<AppleCertificateType>))]
public partial class AppleCertificateType : Enumeration
{
    public static AppleCertificateType pub = (AppleCertificateType) "pub";
    public static AppleCertificateType priv = (AppleCertificateType) "priv";
    public static AppleCertificateType session = (AppleCertificateType) "session";
    public static AppleCertificateType cert = (AppleCertificateType) "cert";
    public static AppleCertificateType agg = (AppleCertificateType) "agg";
    public static implicit operator AppleCertificateType(string value)
    {
        return new AppleCertificateType { Value = value };
    }
}
#endregion
#region AppleCertificateFormat
/// <summary>
///   Used within <see cref="SecurityTasks"/>.
/// </summary>
[PublicAPI]
[Serializable]
[ExcludeFromCodeCoverage]
[TypeConverter(typeof(TypeConverter<AppleCertificateFormat>))]
public partial class AppleCertificateFormat : Enumeration
{
    public static AppleCertificateFormat openssl = (AppleCertificateFormat) "openssl";
    public static AppleCertificateFormat openssh = (AppleCertificateFormat) "openssh";
    public static AppleCertificateFormat openssh2 = (AppleCertificateFormat) "openssh2";
    public static AppleCertificateFormat bsafe = (AppleCertificateFormat) "bsafe";
    public static AppleCertificateFormat raw = (AppleCertificateFormat) "raw";
    public static AppleCertificateFormat pkcs7 = (AppleCertificateFormat) "pkcs7";
    public static AppleCertificateFormat pkcs8 = (AppleCertificateFormat) "pkcs8";
    public static AppleCertificateFormat pkcs12 = (AppleCertificateFormat) "pkcs12";
    public static AppleCertificateFormat netscape = (AppleCertificateFormat) "netscape";
    public static AppleCertificateFormat pemseq = (AppleCertificateFormat) "pemseq";
    public static implicit operator AppleCertificateFormat(string value)
    {
        return new AppleCertificateFormat { Value = value };
    }
}
#endregion
