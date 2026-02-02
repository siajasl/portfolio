using System;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.WPF.State;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Encapsulates common settings.
    /// </summary>
    /// <remarks>
    /// These are either initialised at application start-up or by the user.
    /// </remarks>
    public class WPFWindowBaseSettings
    {
        #region Ctor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="window">The window currently being processed.</param>
        internal WPFWindowBaseSettings(WPFWindowBase window)
        {
            InitialiseMembers(window);
        }

        /// <summary>
        /// Standard member initialisation method.
        /// </summary>
        /// <param name="window">The window currently being processed.</param>
        protected void InitialiseMembers(WPFWindowBase window)
        {
            StateManager = new StateManager(window);
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the state manager.
        /// </summary>
        private StateManager StateManager
        { get; set; }

        /// <summary>
        /// Gets or sets the current theme.
        /// </summary>
        public string UserTheme
        {
            get
            {
                string cachedValue = StateManager.Session.CachedUserTheme;
                if (string.IsNullOrEmpty(cachedValue))
                    return StateManager.Application.CachedDefaultTheme;
                else
                    return cachedValue;
            }
            set
            {
                StateManager.Session.CachedUserTheme = value;
            }
        }

        /// <summary>
        /// Gets or sets the current culture.
        /// </summary>
        public string UserCulture
        {
            get
            {
                string cachedValue = StateManager.Session.CachedUserCulture;
                if (string.IsNullOrEmpty(cachedValue))
                    return StateManager.Application.CachedDefaultCulture;
                else
                    return cachedValue;
            }
            set
            {
                StateManager.Session.CachedUserCulture = value; 
            }
        }

        /// <summary>
        /// Gets or sets the current culture id.
        /// </summary>
        public int UserCultureId
        {
            get
            {
                int cachedValue = StateManager.Session.CachedUserCultureId;
                if (cachedValue == 0)
                    return StateManager.Application.CachedDefaultCultureId;
                else
                    return cachedValue;
            }
            set
            {
                StateManager.Session.CachedUserCultureId = value;
            }
        }

        /// <summary>
        /// Gets whether the user is locked out of the system.
        /// </summary>
        public bool UserIsLockedOut
        {
            get
            {
                return 
                    AuthenticationFailureCount >= StateManager.Application.CachedAuthenticationLockout;
            }
        }

        /// <summary>
        /// Gets or sets the count of user authentication failures.
        /// </summary>
        public int AuthenticationFailureCount
        {
            get
            {
                return StateManager.Session.AuthenticationFailureCount;
            }
            set
            {
                StateManager.Session.AuthenticationFailureCount = value;
            }
        }

        /// <summary>
        /// Gets or sets the current user id.
        /// </summary>
        public int UserId
        {
            get
            {
                return StateManager.Session.CachedUserId;
            }
            set
            {
                StateManager.Session.CachedUserId = value;
            }
        }

        /// <summary>
        /// Gets or sets the cached user meta data.
        /// </summary>
        public object UserMetaData
        {
            get
            {
                object cachedValue = StateManager.Session.CachedUserMetaData;
                if (cachedValue == null)
                    return StateManager.Application.CachedDefaultUserMetaData;
                else
                    return cachedValue;
            }
            set
            {
                StateManager.Session.CachedUserMetaData = value;
            }
        }

        /// <summary>
        /// Gets or sets the array of user role ids.
        /// </summary>
        public int[] UserRoleIds
        {
            get
            {
                int[] cachedValue = StateManager.Session.CachedUserRoleIds;
                if (cachedValue == null || cachedValue.Length == 0)
                    cachedValue = new int[] {StateManager.Application.CachedDefaultUserRoleId};
                return cachedValue;
            }
            set
            {
                StateManager.Session.CachedUserRoleIds = value;
            }
        }

        /// <summary>
        /// Gets or sets the current user name (unique across the system).
        /// </summary>
        public string UserName
        {
            get
            {
                return StateManager.Session.CachedUserName;
            }
            set
            {
                StateManager.Session.CachedUserName = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the current user display name.
        /// </summary>
        public string UserDisplayName
        {
            get
            {
                return StateManager.Session.CachedUserDisplayName;
            }
            set
            {
                StateManager.Session.CachedUserDisplayName = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the current user roles.
        /// </summary>
        public string[] UserRoleNames
        {
            get
            {
                return StateManager.Session.CachedUserRoleNames;
            }
            set
            {
                StateManager.Session.CachedUserRoleNames = value;
            }
        }

        /// <summary>
        /// Gets or sets the current ui culture.
        /// </summary>
        public string UserUiCulture
        {
            get
            {
                string cachedValue = StateManager.Session.CachedUserUICulture;
                if (String.IsNullOrEmpty(cachedValue))
                    return StateManager.Application.CachedDefaultUICulture;
                else
                    return cachedValue;
            }
            set
            {
                StateManager.Session.CachedUserUICulture = value;
            }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        public string ApplicationVersion
        {
            get
            {
                return StateManager.Application.CachedApplicationVersion;
            }
        }

        /// <summary>
        /// Gets the application release date.
        /// </summary>
        public string ApplicationReleaseDate
        {
            get
            {
                return StateManager.Application.CachedApplicationReleaseDate;
            }
        }

        /// <summary>
        /// Gets the concatantated application version & release date.
        /// </summary>
        public string ApplicationVersionAndReleaseDate
        {
            get
            {
                return ApplicationVersion + " - " + ApplicationReleaseDate;
            }
        }

        /// <summary>
        /// Gets or sets the cached application meta data.
        /// </summary>
        public object ApplicationMetaData
        {
            get
            {
                return StateManager.Application.CachedApplicationMetaData;
            }
            set
            {
                StateManager.Application.CachedApplicationMetaData = value;
            }
        }

        /// <summary>
        /// Gets the client name that should have been setup in the application initialisation routine.
        /// </summary>
        public string ClientName
        {
            get
            {
                return StateManager.Application.CachedClientName;
            }
        }

        /// <summary>
        /// Gets or sets the cached client meta data.
        /// </summary>
        public object ClientMetaData
        {
            get
            {
                return StateManager.Application.CachedClientMetaData;
            }
            set
            {
                StateManager.Application.CachedClientMetaData = value;
            }
        }

        #endregion Properties

        #region Methods

        public GuiContext CreateContext()
        {
            GuiContext result = new GuiContext();
            result.CultureId = UserCultureId;
            result.UserId = UserId;
            result.UserRoleTypeIds = UserRoleIds;
            return result;
        }

        #endregion Methods
    }
}
