using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Application
{
    /// <summary>
    /// Encapsulates a view designed to allow a user to change their password.
    /// </summary>
    public interface IAuthenticationChangeView :
        IAuthenticationLoginView
    {
        /// <summary>
        /// Gets the entered new password.
        /// </summary>        
        string NewPassword { get; }
    }
}