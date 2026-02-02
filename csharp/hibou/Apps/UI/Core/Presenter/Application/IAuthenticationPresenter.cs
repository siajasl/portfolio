using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View.Application;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Application
{
    /// <summary>
    /// Presenter encapsulating security operations.
    /// </summary>
    public interface IAuthenticationPresenter
    {
        /// <summary>
        /// Authenticates the passed change user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void AuthenticateChange(
            IAuthenticationChangeView view,
            GuiContext viewContext);

        /// <summary>
        /// Authenticates the passed login user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void AuthenticateLogin(
            IAuthenticationLoginView view,
            GuiContext viewContext);

        /// <summary>
        /// Authenticates the passed initial user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void AuthenticateInititalisation(
            IAuthenticationInitialisationView view,
            GuiContext viewContext);

        /// <summary>
        /// Authenticates the step one forgotten passed user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void AuthenticateForgottenStepOne(
            IAuthenticationForgottenStepOneView view,
            GuiContext viewContext);

        /// <summary>
        /// Authenticates the step two forgotten passed user credentials.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void AuthenticateForgottenStepTwo(
            IAuthenticationForgottenStepTwoView view,
            GuiContext viewContext);
    }
}