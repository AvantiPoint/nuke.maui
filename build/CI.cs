using System.Collections.Generic;
using AvantiPoint.Nuke.Maui.CI;

public class CI : CIBuild
{
    public override PushTrigger OnPush => "master";

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
                new SignedWindowsBuild(),
                new CompileLibrary()
            }
        },
        new CIStage
        {
            Name = "Deploy",
            Jobs = new ICIJob[]
            {
                new PublishInternal()
            }
        }
    };
}
