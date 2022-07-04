namespace AvantiPoint.Nuke.Maui;

internal static class BuildProps
{
    public static class Maui
    {
        public const string ApplicationDisplayVersion = nameof(ApplicationDisplayVersion);

        public const string ApplicationVersion = nameof(ApplicationVersion);
    }

    public static class Android
    {
        public const string AndroidSigningKeyPass = nameof(AndroidSigningKeyPass);

        public const string AndroidSigningStorePass = nameof(AndroidSigningStorePass);

        public const string AndroidSigningKeyAlias = nameof(AndroidSigningKeyAlias);

        public const string AndroidSigningKeyStore = nameof(AndroidSigningKeyStore);
    }

    public static class iOS
    {
        public const string ArchiveOnBuild = nameof(ArchiveOnBuild);

        public const string CodesignKey = nameof(CodesignKey);

        public const string CodesignProvision = nameof(CodesignProvision);

        public const string MtouchLink = nameof(MtouchLink);
    }

    public static class MacCatalyst
    {
        public const string CreatePackage = nameof(CreatePackage);
    }
}
