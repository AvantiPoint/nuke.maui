using System.Text.Json.Serialization;

namespace AvantiPoint.Nuke.Maui.Apple.AppStoreConnect;

public record Attributes(
    [property: JsonPropertyName("profileState")] ProfileState ProfileState,
    [property: JsonPropertyName("createdDate")] DateTime CreatedDate,
    [property: JsonPropertyName("profileType")] ProfileType ProfileType,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("profileContent")] string ProfileContent,
    [property: JsonPropertyName("uuid")] string Uuid,
    [property: JsonPropertyName("platform")] string Platform,
    [property: JsonPropertyName("expirationDate")] DateTime ExpirationDate
);

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProfileState
{
    ACTIVE,
    INVALID
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProfileType
{
    IOS_APP_DEVELOPMENT,
    IOS_APP_STORE,
    IOS_APP_ADHOC,
    IOS_APP_INHOUSE,
    MAC_APP_DEVELOPMENT,
    MAC_APP_STORE,
    MAC_APP_DIRECT,
    TVOS_APP_DEVELOPMENT,
    TVOS_APP_STORE,
    TVOS_APP_ADHOC,
    TVOS_APP_INHOUSE,
    MAC_CATALYST_APP_DEVELOPMENT,
    MAC_CATALYST_APP_STORE,
    MAC_CATALYST_APP_DIRECT
}

public record BundleId(
    [property: JsonPropertyName("links")] Links Links
);

public record Certificates(
    [property: JsonPropertyName("meta")] Meta Meta,
    [property: JsonPropertyName("links")] Links Links
);

public record ProfileResponse(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("attributes")] Attributes Attributes,
    [property: JsonPropertyName("relationships")] Relationships Relationships,
    [property: JsonPropertyName("links")] Links Links
);

public record Devices(
    [property: JsonPropertyName("meta")] Meta Meta,
    [property: JsonPropertyName("links")] Links Links
);

public record Links(
    [property: JsonPropertyName("self")] string Self,
    [property: JsonPropertyName("related")] string Related
);

public record Meta(
    [property: JsonPropertyName("paging")] Paging Paging
);

public record Paging(
    [property: JsonPropertyName("total")] int Total,
    [property: JsonPropertyName("limit")] object Limit
);

public record Relationships(
    [property: JsonPropertyName("bundleId")] BundleId BundleId,
    [property: JsonPropertyName("certificates")] Certificates Certificates,
    [property: JsonPropertyName("devices")] Devices Devices
);

public record GetProfileResponse(
    [property: JsonPropertyName("data")] IReadOnlyList<ProfileResponse> Data,
    [property: JsonPropertyName("links")] Links Links,
    [property: JsonPropertyName("meta")] Meta Meta
);
