using System.Collections.Generic;
using AvantiPoint.Nuke.Maui.CI;

public class CompileLibrary : CIJobBase
{
    public override string ArtifactName => "nuget";
    public override IEnumerable<string> InvokedTargets => new[]
    {
        nameof(ICompileLibrary.CompileLib),
        "--solution AvantiPoint.Nuke.Maui.sln",
        "--project-name AvantiPoint.Nuke.Maui"
    };
}
