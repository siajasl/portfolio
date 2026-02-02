using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View.Application;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Application
{
    /// <summary>
    /// Presenter encapsulating contact  operations.
    /// </summary>
    public interface IContactPresenter
    {
        /// <summary>
        /// Sends an email to the administrator from an application user.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void SendEmail(
            IContactView view,
            GuiContext viewContext);
    }
}