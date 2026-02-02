using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Application
{
    /// <summary>
    /// Encapsulates step one of the forgotten credentials view.
    /// </summary>
    public interface IAuthenticationForgottenStepOneView :
        IAuthenticationView
    {
        /// <summary>
        /// Gets the entered email address.
        /// </summary>        
        string EmailAddress { get; }

        /// <summary>
        /// Step one authentication success event handler.
        /// </summary>
        void ProceedToStepTwo(string passwordQuestion);
    }
}