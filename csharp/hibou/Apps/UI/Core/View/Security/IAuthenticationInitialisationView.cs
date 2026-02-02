using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Security
{
    /// <summary>
    /// Encapsulates a view designed to retreive a new user's credentials for authentication purposes.
    /// </summary>
    public interface IAuthenticationInitialisationView :
        IAuthenticationLoginView
    {
        /// <summary>
        /// Gets the entered new password.
        /// </summary>        
        string NewPassword { get; }
    }
}