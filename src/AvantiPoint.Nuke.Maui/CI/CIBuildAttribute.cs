using JetBrains.Annotations;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Serilog;

namespace AvantiPoint.Nuke.Maui.CI;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public abstract class CIBuildAttribute : ConfigurationAttributeBase
{
    protected readonly CIBuild Build;
    protected readonly string _name;

    public CIBuildAttribute(Type type)
    {
        var instance = Activator.CreateInstance(type);
        if (instance is CIBuild build)
            Build = build;
        else
            throw new InvalidCastException();

        _name = type.Name.SplitCamelHumpsWithKnownWords().JoinUnderscore().ToLowerInvariant();
    }

    public override string IdPostfix => _name;

    public override IEnumerable<string> RelevantTargetNames => Array.Empty<string>();
    public override IEnumerable<string> IrrelevantTargetNames => Array.Empty<string>();

    public override CustomFileWriter CreateWriter(StreamWriter streamWriter) =>
        new (streamWriter, indentationFactor: 2, commentPrefix: "#");

    public override ConfigurationEntity GetConfiguration(NukeBuild build, IReadOnlyCollection<ExecutableTarget> relevantTargets)
    {
        Assert.True(Build.Stages.Any(), "The Build must contain at least one Stage");
        Build.Stages.ForEach(x => Assert.True(x.Jobs.Any(), "The Stage '{Name}' must contain at least one job", x.Name));
        var executableTargets = build.ExecutableTargets();
        var targetNames = Build.Stages.SelectMany(x => x.Jobs)
            .SelectMany(x => x.InvokedTargets)
            .Where(x => !x.StartsWith("--"))
            .Distinct();

        try
        {
            targetNames.ForEach(x =>
            {
                x.NotNullOrEmpty("The job name cannot be null or empty.");
                Assert.True(executableTargets.Select(_ => _.Name).Contains(x),
                    $"The Target '{x}' does not exist");
            });

            var targets = executableTargets.Where(x => targetNames.Contains(x.Name));
            return BuildConfiguration(build, targets);
        }
        catch (Exception ex)
        {
            if (System.Diagnostics.Debugger.IsAttached)
                System.Diagnostics.Debugger.Break();
            else
                System.Diagnostics.Debugger.Launch();
            Log.Error(ex.ToString());
         
            throw;
        }
    }

    protected abstract ConfigurationEntity BuildConfiguration(NukeBuild build, IEnumerable<ExecutableTarget> relevantTargets);
}
