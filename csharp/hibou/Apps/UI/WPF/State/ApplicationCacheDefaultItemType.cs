
namespace Keane.CH.Framework.Apps.UI.WPF.State
{
    /// <summary>
    /// Enumeration over the default application cache items.
    /// </summary>
    internal enum ApplicationCacheDefaultItemType
    {
        /// <summary>
        /// The number of authenticaton attempts after which a user is locked out.
        /// </summary>
        AuthenticationLockout,

        /// <summary>
        /// The application meta-data being stored within the application session state.
        /// </summary>
        ApplicationMetaData,

        /// <summary>
        /// The application version.
        /// </summary>
        ApplicationVersion,

        /// <summary>
        /// The application release date.
        /// </summary>
        ApplicationReleaseDate,

        /// <summary>
        /// The configuration root folder path.
        /// </summary>
        ConfigRootFolderPath,

        /// <summary>
        /// The default culture.
        /// </summary>
        DefaultUserCulture,

        /// <summary>
        /// The default culture id.
        /// </summary>
        DefaultUserCultureId,

        /// <summary>
        /// The default UI culture.
        /// </summary>
        DefaultUserUICulture,

        /// <summary>
        /// The default theme.
        /// </summary>
        DefaultUserTheme,

        /// <summary>
        /// The default user role type Id.
        /// </summary>
        DefaultUserRoleId,

        /// <summary>
        /// The default user meta data.
        /// </summary>
        DefaultUserMetaData,

        /// <summary>
        /// The default client email address.
        /// </summary>
        DefaultClientEmailAddress,

        /// <summary>
        /// The client meta-data being stored within the application session state.
        /// </summary>
        ClientMetaData,

        /// <summary>
        /// The client name being stored within the application session state.
        /// </summary>
        ClientName,

        /// <summary>
        /// The admin password minimum length.
        /// </summary>
        PasswordAdminMinLength,

        /// <summary>
        /// The admin password strength.
        /// </summary>
        PasswordAdminStrength,

        /// <summary>
        /// The password byte length.
        /// </summary>
        PasswordByteLength,

        /// <summary>
        /// The password history count.
        /// </summary>
        PasswordHistoryCount,

        /// <summary>
        /// The standard password minimum length.
        /// </summary>
        PasswordMinLength,

        /// <summary>
        /// The standard password strength.
        /// </summary>
        PasswordStrength,
    }
}
