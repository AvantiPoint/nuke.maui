using Refit;

namespace AvantiPoint.Nuke.Maui.Apple;

public static class AppStoreConnectApi
{
    internal const string Audience = "appstoreconnect-v1";

    internal const string Api = "https://api.appstoreconnect.apple.com";

    public static IAppStoreConnectClient GetClient(string jwt) =>
        RestService.For<IAppStoreConnectClient>(Api, new RefitSettings
        {
            AuthorizationHeaderValueGetter = () => Task.FromResult(jwt),
        });
}
