using System.Collections.Generic;
using AvantiPoint.Nuke.Maui.CI;

public class PR : CIBuild
{
    public override PullRequestTrigger OnPull => new()
    {
        Branches = new[] { "master" },
        ExcludePaths = new[] { ".gitignore", ".editorconfig", "docs/**", "**/*.md", ".github/**" }
    };

    public override IEnumerable<ICIStage> Stages => new[]
    {
        new CIStage
        {
            Name = "Build",
            Jobs = new ICIJob[]
            {
                new AndroidJob(),
                new iOSJob(),
                new MacCatalystJob(),
                //new SignedWindowsBuild(),
                new WindowsJob(),
            }
        }
    };
}
