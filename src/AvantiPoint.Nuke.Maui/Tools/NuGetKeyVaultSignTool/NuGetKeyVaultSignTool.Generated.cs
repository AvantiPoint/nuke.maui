// Generated from https://raw.githubusercontent.com/AvantiPoint/nuke.maui/master/src/AvantiPoint.Nuke.Maui/Tools/NuGetKeyVaultSignTool/NuGetKeyVaultSignTool.json

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

namespace AvantiPoint.Nuke.Maui.Tools.NuGetKeyVaultSignTool
{
    /// <summary>
    ///   <p>NuGetKeyVaultSignTool is a DotNet CLI Tool to help you easily sign NuGet packages</p>
    ///   <p>For more details, visit the <a href="https://github.com/novotnyllc/NuGetKeyVaultSignTool">official website</a>.</p>
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class NuGetKeyVaultSignToolTasks
    {
        /// <summary>
        ///   Path to the NuGetKeyVaultSignTool executable.
        /// </summary>
        public static string NuGetKeyVaultSignToolPath =>
            ToolPathResolver.TryGetEnvironmentExecutable("NUGETKEYVAULTSIGNTOOL_EXE") ??
            ToolPathResolver.GetPackageExecutable("NuGetKeyVaultSignTool", "NuGetKeyVaultSignTool.exe");
        public static Action<OutputType, string> NuGetKeyVaultSignToolLogger { get; set; } = ProcessTasks.DefaultLogger;
        /// <summary>
        ///   <p>NuGetKeyVaultSignTool is a DotNet CLI Tool to help you easily sign NuGet packages</p>
        ///   <p>For more details, visit the <a href="https://github.com/novotnyllc/NuGetKeyVaultSignTool">official website</a>.</p>
        /// </summary>
        public static IReadOnlyCollection<Output> NuGetKeyVaultSignTool(string arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Func<string, string> outputFilter = null)
        {
            using var process = ProcessTasks.StartProcess(NuGetKeyVaultSignToolPath, arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, NuGetKeyVaultSignToolLogger, outputFilter);
            process.AssertZeroExitCode();
            return process.Output;
        }
        /// <summary>
        ///   <p>NuGetKeyVaultSignTool is a DotNet CLI Tool to help you easily sign NuGet packages</p>
        ///   <p>For more details, visit the <a href="https://github.com/novotnyllc/NuGetKeyVaultSignTool">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;packageFilter&gt;</c> via <see cref="NuGetKeyVaultSignToolSettings.PackageFilter"/></li>
        ///     <li><c>--azure-key-vault-certificate</c> via <see cref="NuGetKeyVaultSignToolSettings.CertificateName"/></li>
        ///     <li><c>--azure-key-vault-client-id</c> via <see cref="NuGetKeyVaultSignToolSettings.ClientId"/></li>
        ///     <li><c>--azure-key-vault-client-secret</c> via <see cref="NuGetKeyVaultSignToolSettings.ClientSecret"/></li>
        ///     <li><c>--azure-key-vault-tenant-id</c> via <see cref="NuGetKeyVaultSignToolSettings.TenantId"/></li>
        ///     <li><c>--azure-key-vault-url</c> via <see cref="NuGetKeyVaultSignToolSettings.AzureKeyVaultUrl"/></li>
        ///     <li><c>--file-digest</c> via <see cref="NuGetKeyVaultSignToolSettings.FileDigest"/></li>
        ///     <li><c>--timestamp-digest</c> via <see cref="NuGetKeyVaultSignToolSettings.TimestampDigest"/></li>
        ///     <li><c>--timestamp-rfc3161</c> via <see cref="NuGetKeyVaultSignToolSettings.TimestampUrl"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> NuGetKeyVaultSignTool(NuGetKeyVaultSignToolSettings toolSettings = null)
        {
            toolSettings = toolSettings ?? new NuGetKeyVaultSignToolSettings();
            using var process = ProcessTasks.StartProcess(toolSettings);
            process.AssertZeroExitCode();
            return process.Output;
        }
        /// <summary>
        ///   <p>NuGetKeyVaultSignTool is a DotNet CLI Tool to help you easily sign NuGet packages</p>
        ///   <p>For more details, visit the <a href="https://github.com/novotnyllc/NuGetKeyVaultSignTool">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;packageFilter&gt;</c> via <see cref="NuGetKeyVaultSignToolSettings.PackageFilter"/></li>
        ///     <li><c>--azure-key-vault-certificate</c> via <see cref="NuGetKeyVaultSignToolSettings.CertificateName"/></li>
        ///     <li><c>--azure-key-vault-client-id</c> via <see cref="NuGetKeyVaultSignToolSettings.ClientId"/></li>
        ///     <li><c>--azure-key-vault-client-secret</c> via <see cref="NuGetKeyVaultSignToolSettings.ClientSecret"/></li>
        ///     <li><c>--azure-key-vault-tenant-id</c> via <see cref="NuGetKeyVaultSignToolSettings.TenantId"/></li>
        ///     <li><c>--azure-key-vault-url</c> via <see cref="NuGetKeyVaultSignToolSettings.AzureKeyVaultUrl"/></li>
        ///     <li><c>--file-digest</c> via <see cref="NuGetKeyVaultSignToolSettings.FileDigest"/></li>
        ///     <li><c>--timestamp-digest</c> via <see cref="NuGetKeyVaultSignToolSettings.TimestampDigest"/></li>
        ///     <li><c>--timestamp-rfc3161</c> via <see cref="NuGetKeyVaultSignToolSettings.TimestampUrl"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> NuGetKeyVaultSignTool(Configure<NuGetKeyVaultSignToolSettings> configurator)
        {
            return NuGetKeyVaultSignTool(configurator(new NuGetKeyVaultSignToolSettings()));
        }
        /// <summary>
        ///   <p>NuGetKeyVaultSignTool is a DotNet CLI Tool to help you easily sign NuGet packages</p>
        ///   <p>For more details, visit the <a href="https://github.com/novotnyllc/NuGetKeyVaultSignTool">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;packageFilter&gt;</c> via <see cref="NuGetKeyVaultSignToolSettings.PackageFilter"/></li>
        ///     <li><c>--azure-key-vault-certificate</c> via <see cref="NuGetKeyVaultSignToolSettings.CertificateName"/></li>
        ///     <li><c>--azure-key-vault-client-id</c> via <see cref="NuGetKeyVaultSignToolSettings.ClientId"/></li>
        ///     <li><c>--azure-key-vault-client-secret</c> via <see cref="NuGetKeyVaultSignToolSettings.ClientSecret"/></li>
        ///     <li><c>--azure-key-vault-tenant-id</c> via <see cref="NuGetKeyVaultSignToolSettings.TenantId"/></li>
        ///     <li><c>--azure-key-vault-url</c> via <see cref="NuGetKeyVaultSignToolSettings.AzureKeyVaultUrl"/></li>
        ///     <li><c>--file-digest</c> via <see cref="NuGetKeyVaultSignToolSettings.FileDigest"/></li>
        ///     <li><c>--timestamp-digest</c> via <see cref="NuGetKeyVaultSignToolSettings.TimestampDigest"/></li>
        ///     <li><c>--timestamp-rfc3161</c> via <see cref="NuGetKeyVaultSignToolSettings.TimestampUrl"/></li>
        ///   </ul>
        /// </remarks>
        public static IEnumerable<(NuGetKeyVaultSignToolSettings Settings, IReadOnlyCollection<Output> Output)> NuGetKeyVaultSignTool(CombinatorialConfigure<NuGetKeyVaultSignToolSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
        {
            return configurator.Invoke(NuGetKeyVaultSignTool, NuGetKeyVaultSignToolLogger, degreeOfParallelism, completeOnFailure);
        }
    }
    #region NuGetKeyVaultSignToolSettings
    /// <summary>
    ///   Used within <see cref="NuGetKeyVaultSignToolTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class NuGetKeyVaultSignToolSettings : ToolSettings
    {
        /// <summary>
        ///   Path to the NuGetKeyVaultSignTool executable.
        /// </summary>
        public override string ProcessToolPath => base.ProcessToolPath ?? NuGetKeyVaultSignToolTasks.NuGetKeyVaultSignToolPath;
        public override Action<OutputType, string> ProcessCustomLogger => NuGetKeyVaultSignToolTasks.NuGetKeyVaultSignToolLogger;
        /// <summary>
        ///   A filter like '**/*.nupkg' or '**/*.snupkg'
        /// </summary>
        public virtual string PackageFilter { get; internal set; }
        public virtual string FileDigest { get; internal set; } = "sha256";
        /// <summary>
        ///   The URL for the Timestamp server
        /// </summary>
        public virtual Uri TimestampUrl { get; internal set; } = new Uri("http://timestamp.digicert.com");
        public virtual string TimestampDigest { get; internal set; } = "sha256";
        public virtual Uri AzureKeyVaultUrl { get; internal set; }
        public virtual string ClientId { get; internal set; }
        public virtual string TenantId { get; internal set; }
        public virtual string ClientSecret { get; internal set; }
        public virtual string CertificateName { get; internal set; }
        protected override Arguments ConfigureProcessArguments(Arguments arguments)
        {
            arguments
              .Add("sign")
              .Add("{value}", PackageFilter)
              .Add("--file-digest {value}", FileDigest)
              .Add("--timestamp-rfc3161 {value}", TimestampUrl)
              .Add("--timestamp-digest {value}", TimestampDigest)
              .Add("--azure-key-vault-url {value}", AzureKeyVaultUrl, secret: true)
              .Add("--azure-key-vault-client-id {value}", ClientId, secret: true)
              .Add("--azure-key-vault-tenant-id {value}", TenantId, secret: true)
              .Add("--azure-key-vault-client-secret {value}", ClientSecret, secret: true)
              .Add("--azure-key-vault-certificate {value}", CertificateName, secret: true);
            return base.ConfigureProcessArguments(arguments);
        }
    }
    #endregion
    #region NuGetKeyVaultSignToolSettingsExtensions
    /// <summary>
    ///   Used within <see cref="NuGetKeyVaultSignToolTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class NuGetKeyVaultSignToolSettingsExtensions
    {
        #region PackageFilter
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.PackageFilter"/></em></p>
        ///   <p>A filter like '**/*.nupkg' or '**/*.snupkg'</p>
        /// </summary>
        [Pure]
        public static T SetPackageFilter<T>(this T toolSettings, string packageFilter) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.PackageFilter = packageFilter;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.PackageFilter"/></em></p>
        ///   <p>A filter like '**/*.nupkg' or '**/*.snupkg'</p>
        /// </summary>
        [Pure]
        public static T ResetPackageFilter<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.PackageFilter = null;
            return toolSettings;
        }
        #endregion
        #region FileDigest
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.FileDigest"/></em></p>
        /// </summary>
        [Pure]
        public static T SetFileDigest<T>(this T toolSettings, string fileDigest) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.FileDigest = fileDigest;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.FileDigest"/></em></p>
        /// </summary>
        [Pure]
        public static T ResetFileDigest<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.FileDigest = null;
            return toolSettings;
        }
        #endregion
        #region TimestampUrl
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.TimestampUrl"/></em></p>
        ///   <p>The URL for the Timestamp server</p>
        /// </summary>
        [Pure]
        public static T SetTimestampUrl<T>(this T toolSettings, Uri timestampUrl) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TimestampUrl = timestampUrl;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.TimestampUrl"/></em></p>
        ///   <p>The URL for the Timestamp server</p>
        /// </summary>
        [Pure]
        public static T ResetTimestampUrl<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TimestampUrl = null;
            return toolSettings;
        }
        #endregion
        #region TimestampDigest
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.TimestampDigest"/></em></p>
        /// </summary>
        [Pure]
        public static T SetTimestampDigest<T>(this T toolSettings, string timestampDigest) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TimestampDigest = timestampDigest;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.TimestampDigest"/></em></p>
        /// </summary>
        [Pure]
        public static T ResetTimestampDigest<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TimestampDigest = null;
            return toolSettings;
        }
        #endregion
        #region AzureKeyVaultUrl
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.AzureKeyVaultUrl"/></em></p>
        /// </summary>
        [Pure]
        public static T SetAzureKeyVaultUrl<T>(this T toolSettings, [Secret] Uri azureKeyVaultUrl) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.AzureKeyVaultUrl = azureKeyVaultUrl;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.AzureKeyVaultUrl"/></em></p>
        /// </summary>
        [Pure]
        public static T ResetAzureKeyVaultUrl<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.AzureKeyVaultUrl = null;
            return toolSettings;
        }
        #endregion
        #region ClientId
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.ClientId"/></em></p>
        /// </summary>
        [Pure]
        public static T SetClientId<T>(this T toolSettings, [Secret] string clientId) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ClientId = clientId;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.ClientId"/></em></p>
        /// </summary>
        [Pure]
        public static T ResetClientId<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ClientId = null;
            return toolSettings;
        }
        #endregion
        #region TenantId
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.TenantId"/></em></p>
        /// </summary>
        [Pure]
        public static T SetTenantId<T>(this T toolSettings, [Secret] string tenantId) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TenantId = tenantId;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.TenantId"/></em></p>
        /// </summary>
        [Pure]
        public static T ResetTenantId<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.TenantId = null;
            return toolSettings;
        }
        #endregion
        #region ClientSecret
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.ClientSecret"/></em></p>
        /// </summary>
        [Pure]
        public static T SetClientSecret<T>(this T toolSettings, [Secret] string clientSecret) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ClientSecret = clientSecret;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.ClientSecret"/></em></p>
        /// </summary>
        [Pure]
        public static T ResetClientSecret<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.ClientSecret = null;
            return toolSettings;
        }
        #endregion
        #region CertificateName
        /// <summary>
        ///   <p><em>Sets <see cref="NuGetKeyVaultSignToolSettings.CertificateName"/></em></p>
        /// </summary>
        [Pure]
        public static T SetCertificateName<T>(this T toolSettings, [Secret] string certificateName) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.CertificateName = certificateName;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="NuGetKeyVaultSignToolSettings.CertificateName"/></em></p>
        /// </summary>
        [Pure]
        public static T ResetCertificateName<T>(this T toolSettings) where T : NuGetKeyVaultSignToolSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.CertificateName = null;
            return toolSettings;
        }
        #endregion
    }
    #endregion
}
