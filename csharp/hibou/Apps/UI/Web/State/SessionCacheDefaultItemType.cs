
namespace Keane.CH.Framework.Apps.UI.Web.State
{
    /// <summary>
    /// Enumeration over the default session cache items.
    /// </summary>
    internal enum SessionCacheDefaultItemType
    {
        /// <summary>
        /// The number of times that the user has unsuccessfully attempted to authenticate.
        /// </summary>
        AuthenticationFailureCount,

        /// <summary>
        /// The theme currently in use (see multi-themed web-sites).
        /// </summary>
        UserTheme,

        /// <summary>
        /// The id of the culture currently in use (see multi-language web-sites).
        /// </summary>
        UserCultureId,

        /// <summary>
        /// The culture currently in use (see multi-language web-sites).
        /// </summary>
        UserCulture,

        /// <summary>
        /// The ui culture currently in use (see multi-language web-sites).
        /// </summary>        
        UserUICulture,

        /// <summary>
        /// The user meta data.
        /// </summary>
        UserMetaData,

        /// <summary>
        /// The id of the user.
        /// </summary>
        UserId,

        /// <summary>
        /// The role type id of the user.
        /// </summary>
        UserRoleTypeIds,

        /// <summary>
        /// The name of the user.
        /// </summary>
        UserName,
        
        /// <summary>
        /// The roles of the user.
        /// </summary>
        UserRoles,

        /// <summary>
        /// The user's display name.
        /// </summary>
        UserDisplayName,
    }
}