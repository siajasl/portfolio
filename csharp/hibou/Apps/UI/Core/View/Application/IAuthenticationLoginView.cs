using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Application
{
    /// <summary>
    /// Encapsulates a view designed to retreive a user's credneitals for authentication purposes.
    /// </summary>
    public interface IAuthenticationLoginView : 
        IAuthenticationView
    {
        /// <summary>
        /// Gets the entered password.
        /// </summary>        
        string Password { get; }
    }
}
