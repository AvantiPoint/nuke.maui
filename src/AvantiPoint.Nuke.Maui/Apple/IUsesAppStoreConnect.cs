using System.Security.Cryptography;
using System.Text;
using AvantiPoint.Nuke.Maui.Apple.AppStoreConnect;
using JetBrains.Annotations;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Nuke.Common;
using Serilog;

namespace AvantiPoint.Nuke.Maui.Apple;

[PublicAPI]
public interface IUsesAppStoreConnect : INukeBuild
{
    [Parameter("Apple Issuer Id is required"), Secret]
    string AppleIssuerId => TryGetValue(() => AppleIssuerId);

    [Parameter("Apple Key Id is required"), Secret]
    string AppleKeyId => TryGetValue(() => AppleKeyId);

    [Parameter("Apple AuthKey P8 text value"), Secret]
    string AppleAuthKeyP8 => TryGetValue(() => AppleAuthKeyP8);

    Target GenerateJwt => _ => _
        .Description("This is a helper for local testing. You should avoid using this in CI as it will print the generated JWT to the logs.")
        .Executes(() =>
        {
            var token = GenerateToken();
            Log.Information("JWT: {token}", token);
        });

    async Task<GetProfileResponse> GetProvisioningProfiles()
    {
        var client = AppStoreConnectApi.GetClient(GenerateToken());
        using var response = await client.GetProfiles();
        Assert.True(response.IsSuccessStatusCode, $"Unable to successfully connect to the AppStore Connect API. ({response.StatusCode})");
        response.Content.NotNull("AppStore Connect API Response produced an empty response.");
        return response.Content!;
    }

    string GenerateToken(params string[] scopes)
    {
        var p8 = Encoding.Default.GetString(Convert.FromBase64String(AppleAuthKeyP8));
        var key = ECDsa.Create();
        key.NotNull("Unable to create ECDsa Key");
        key.ImportFromPem(p8.AsSpan());

        var now = DateTime.UtcNow;
        var claims = new Dictionary<string, object>();
        if (scopes.Any())
            claims.Add("scope", scopes);

        return new JsonWebTokenHandler().CreateToken(new SecurityTokenDescriptor
        {
            Issuer = AppleIssuerId,
            Audience = AppStoreConnectApi.Audience,
            NotBefore = now,
            Expires = now.AddMinutes(20),
            IssuedAt = now,
            Claims = claims,
            SigningCredentials = new SigningCredentials(new ECDsaSecurityKey(key) { KeyId = AppleKeyId }, "ES256")
        });
    }
}
