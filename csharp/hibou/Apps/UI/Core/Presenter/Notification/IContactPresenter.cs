using Keane.CH.Framework.Apps.UI.Core.View.Notification;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Notification
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