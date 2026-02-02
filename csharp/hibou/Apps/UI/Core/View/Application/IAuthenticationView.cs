using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Application
{
    /// <summary>
    /// Encapsulates a view designed to retreive a user's credneitals for authentication purposes.
    /// </summary>
    public interface IAuthenticationView
    {
        /// <summary>
        /// Gets the entered user name.
        /// </summary>        
        string UserName { get; }

        /// <summary>
        /// Gets or sets the number of authentication failures within the session.
        /// </summary>
        int AuthenticationFailureCount { get; set; }

        /// <summary>
        /// Callback invoked when credentials are authenticated.
        /// </summary>
        /// <param name="userName">The user's system wide unique name.</param>
        /// <param name="userRoles">The array of user roles.</param>
        /// <param name="userDisplayName">The name of the user to be displayed within the application.</param>
        /// <param name="culture">The user's culture code.</param>
        /// <param name="uiCultureCode">The user's ui culture code.</param>
        /// <param name="userId">The id of the user.</param>
        /// <param name="userRoleTypeIds">The array of user role type id's.</param>
        /// <param name="userCantonId">The id of the user's canton.</param>
        /// <param name="userCantonCode">The code of the user's canton.</param>
        /// <param name="cultureId">The id of the user's culture.</param>
        void OnAuthenticationSuccess(
            string userName,
            string[] userRoles,
            string userDisplayName,
            string cultureCode,
            string uiCultureCode,
            int userId,
            int[] userRoleTypeIds,
            int userCantonId,
            string userCantonCode,
            int cultureId);

        /// <summary>
        /// Callback invoked when credentials authentication fails.
        /// </summary>
        void OnAuthenticationFailure();

        /// <summary>
        /// Callback invoked when the number of failed authentication attempts causes a lockout.
        /// </summary>
        void OnAuthenticationLockout();
    }
}