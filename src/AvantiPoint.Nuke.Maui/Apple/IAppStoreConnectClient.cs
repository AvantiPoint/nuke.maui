using AvantiPoint.Nuke.Maui.Apple.AppStoreConnect;
using Refit;

namespace AvantiPoint.Nuke.Maui;

public interface IAppStoreConnectClient
{
    [Get("/v1/profiles")]
    [Headers("Authorization: Bearer")]
    Task<ApiResponse<GetProfileResponse>> GetProfiles();
}
