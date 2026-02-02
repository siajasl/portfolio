using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Security
{
    /// <summary>
    /// Encapsulates step two of the forgotten credentials view.
    /// </summary>
    public interface IAuthenticationForgottenStepTwoView :
        IAuthenticationForgottenStepOneView
    {
        /// <summary>
        /// Gets the entered password question answer.
        /// </summary>        
        string PasswordQuestionAnswer { get; }
    }
}