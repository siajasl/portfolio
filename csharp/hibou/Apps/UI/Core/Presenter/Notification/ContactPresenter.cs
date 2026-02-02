using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.Presenter;
using Keane.CH.Framework.Apps.UI.Core.Presenter.Security;
using Keane.CH.Framework.Apps.UI.Core.View.Security;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Notification.Contracts;
using Keane.CH.Framework.Services.Contracts.Notification.Message;
using Keane.CH.Framework.Apps.UI.Core.View.Notification;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Notification
{
    /// <summary>
    /// Manages the contact process.
    /// </summary>
    public class ContactPresenter :
        PresenterBase, 
        IContactPresenter
    {
        #region Collaborators

        /// <summary>
        /// Gets or sets the associated SeedRegisterService.
        /// </summary>
        public INotificationService NotificationService
        { get; set; }

        #endregion Collaborators

        #region IContactPresenter Members

        /// <summary>
        /// Sends an email to the administrator from an website user.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IContactPresenter.SendEmail(
            IContactView view, 
            GuiContext viewContext)
        {
            // Invoke service operation.
            SendContactRequest request = new SendContactRequest() 
            { 
                Context = base.GetRequestContext(viewContext),
                EmailAddress = view.EmailAddress,
                EmailBody = view.EmailBody,
                EmailSubject = view.EmailSubject,
                FirstName = view.FirstName,
                PostalAddressLine1 = view.PostalAddressLine1,
                PostalAddressTown = view.PostalAddressTown,
                PostalAddressZip = view.PostalAddressZip,
                Surname = view.Surname
            };
            OperationResponse response = 
                this.NotificationService.SendContactNotification(request);

            // Process service repsonse.
            if (response.Succeeded)
                view.OnEmailDelivery();
            else
                view.OnEmailDeliveryFailure();
        }

        #endregion
    }
}
