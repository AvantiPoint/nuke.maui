using AvantiPoint.Nuke.Maui.Apple;
using Nuke.Common;
using Nuke.Common.Tools.DotNet;
using Nuke.Components;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

public interface IPublishInternal : IHazArtifacts
{
    [Parameter, Secret]
    string InHouseNugetFeed => TryGetValue(() => InHouseNugetFeed);

    [Parameter, Secret]
    string InHouseApiKey => TryGetValue(() => InHouseApiKey);

    Target PublishNuGet => _ => _
        .Requires(() => InHouseNugetFeed)
        .Requires(() => InHouseApiKey)
        .Executes(() =>
        {
            DotNetNuGetPush(_ => _
                .SetSource(InHouseNugetFeed)
                .SetApiKey(InHouseApiKey)
                .SetSkipDuplicate(true)
                .SetTargetPath(ArtifactsDirectory / "*.nupkg"));
        });
}
