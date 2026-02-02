using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Notification.Contracts;
using Keane.CH.Framework.Core.Utilities.Mail;
using Keane.CH.Framework.Services.Entity.Contracts;
using Keane.CH.Framework.Services.Entity.Contracts.Message;
using Keane.CH.Framework.Services.Security.Contracts.Message;
using Keane.CH.Framework.Services.Security.Implementation;
using Keane.CH.Framework.Services.Security.Implementation.DataAccess;
using Keane.CH.Framework.Services.Notification.Contracts.Data;
using Keane.CH.Framework.Services.Contracts.Notification.Message;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Notification.Implementation
{
    /// <summary>
    /// Encapsulates service operations for sending notifications.
    /// </summary>
    public class NotificationService : 
        ServiceImplementationBase, 
        INotificationService
    {
        #region Properties

        #region Collaborators

        /// <summary>
        /// Gets or sets an associated dao.
        /// </summary>
        public IUserDao UserDao
        { get; set; }

        /// <summary>
        /// Gets or sets the coollection of notification templates.
        /// </summary>
        public NotificationTemplateCollection NotificationTemplateCollection
        { get; set; }

        #endregion Collaborators

        /// <summary>
        /// Gets or sets the default client email address.
        /// </summary>
        public string DefaultClientEmailAddress
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the admin user role type.
        /// </summary>
        public int AdminUserRoleTypeId
        { get; set; }

        #endregion Properties

        #region INotificationService Members

        /// <summary>
        /// Sends a contact notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        OperationResponse 
            INotificationService.SendContactNotification(
                SendContactRequest request)
        {
            try
            {
                // Get admin email list.
                string adminEmailList = GetAdminEmailAddressList();

                // Get template.
                NotificationTemplate template =
                    NotificationTemplateCollection.GetTemplate(NotificationTemplateType.Contact);
                if (template == null)
                    throw new ApplicationException("Email template cannot be found.");

                // Send email.
                string from = this.DefaultClientEmailAddress;
                string to = adminEmailList;
                string subject = template.Subject;
                string body = 
                    String.Format(template.Body, 
                                  request.FirstName, request.Surname, 
                                  request.EmailAddress, request.EmailSubject, 
                                  request.PostalAddressLine1, request.PostalAddressZip, 
                                  request.PostalAddressTown, request.EmailBody);
                SendEmail(from, to, subject, body);

                // Return success.
                return OperationResponse.GetSuccess();
            }
            catch (Exception ex)
            {
                return base.HandleServiceException(ex);
            }
        }

        #endregion

        #region IStandardNotificationService Members

        #region Authentication related

        /// <summary>
        /// Sends a credentials initialisation notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>Operation success or failure.</returns>
        OperationResponse ISecurityNotificationService.SendCreateUserNotification(
            SendCreateUserNotificationRequest request)
        {
            try
            {
                // Get template.
                NotificationTemplate template =
                    NotificationTemplateCollection.GetTemplate(NotificationTemplateType.UserInitialisation);
                if (template == null)
                    throw new ApplicationException("Email template cannot be found.");

                // Send email.
                string from = this.DefaultClientEmailAddress;
                string to = request.EmailAddress;
                string subject = template.Subject;
                string body = String.Format(template.Body, request.InitialPassword);
                SendEmail(from, to, subject, body);

                // Return success.
                return OperationResponse.GetSuccess();
            }
            catch (Exception ex)
            {
                return base.HandleServiceException(ex);
            }
        }

        /// <summary>
        /// Sends a credentials forgotten notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>Operation success or failure.</returns>
        OperationResponse ISecurityNotificationService.SendForgottenCredentialsNotification(
            SendForgottenCredentialsNotificationRequest request)
        {
            try
            {
                // Get template.
                NotificationTemplate template =
                    NotificationTemplateCollection.GetTemplate(NotificationTemplateType.UserCredentialsForgotten);
                if (template == null)
                    throw new ApplicationException("Email template cannot be found.");

                // Send email.
                string from = this.DefaultClientEmailAddress;
                string to = request.EmailAddress;
                string subject = template.Subject;
                string body = String.Format(template.Body, request.TemporaryPassword);
                SendEmail(from, to, subject, body);

                // Return success.
                return OperationResponse.GetSuccess();
            }
            catch (Exception ex)
            {
                return base.HandleServiceException(ex);
            }
        }

        #endregion Authentication related

        #region Protected entity related

        /// <summary>
        /// Sends an entity deletion request notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>Operation success or failure.</returns>
        OperationResponse
            IProtectedEntityNotificationService.SendModificationNotification(
                SendModificationNotificationRequest request)
        {
            try
            {
                // Get admin email list.
                string adminEmailList = GetAdminEmailAddressList();

                // Get requestor.
                User requestor = (User)UserDao.Retrieve(typeof(User), request.Context.UserId);
                if (requestor == null)
                    throw new ApplicationException("The user cannot be found.");

                // Get template.
                NotificationTemplate template =
                    NotificationTemplateCollection.GetTemplate(NotificationTemplateType.ModificationRequest);
                if (template == null)
                    throw new ApplicationException("Email template cannot be found.");

                // Send email.
                string from = requestor.EmailAddress;
                string to = adminEmailList;
                string subject = template.Subject;
                string body = template.Body;
                SendEmail(from, to, subject, body);

                // Return success.
                return OperationResponse.GetSuccess();
            }
            catch (Exception ex)
            {
                return base.HandleServiceException(ex);
            }
        }

        /// <summary>
        /// Sends an entity deletion decision notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>Operation success or failure.</returns>
        OperationResponse
            IProtectedEntityNotificationService.SendAdjudicationNotification(
                SendAjudicationNotificationRequest request)
        {
            try
            {
                // Get adminstrator making the decision.
                User admin = (User)UserDao.Retrieve(typeof(User), request.Context.UserId);
                if (admin == null)
                    throw new ApplicationException("The user cannot be found.");

                // Get originator.
                User requestor = UserDao.GetByModificationId(request.ModificationId);
                if (requestor == null)
                    throw new ApplicationException("The modification requestor cannot be found.");

                // Get template.
                NotificationTemplateType templateType;
                if (request.DecisionType == AjudicationDecisionType.Accept)
                    templateType = NotificationTemplateType.ModificationDecisionAccept;
                else
                    templateType = NotificationTemplateType.ModificationDecisionReject;
                NotificationTemplate template =
                    NotificationTemplateCollection.GetTemplate(templateType);
                if (template == null)
                    throw new ApplicationException("Email template cannot be found.");

                // Send email.
                string from = admin.EmailAddress;
                string to = requestor.EmailAddress;
                string subject = template.Subject;
                string body = template.Body;
                SendEmail(from, to, subject, body);

                // Return success.
                return OperationResponse.GetSuccess();
            }
            catch (Exception ex)
            {
                return base.HandleServiceException(ex);
            }
        }

        #endregion Protected entity related

        #endregion

        #region Private methods

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="from">The email from address.</param>
        /// <param name="to">The email to address.</param>
        /// <param name="subject">The email subject.</param>
        /// <param name="body">The email body.</param>
        private void SendEmail(
            string from, string to, string subject, string body)
        {
            EmailDetails email = new EmailDetails()
            {
                From = from,
                To = to,
                Subject = subject,
                Body = body
            };
            SmtpUtility.SendEmail(email);
        }

        /// <summary>
        /// Gets the administrator email list.
        /// </summary>
        /// <returns>The list of administrators emails.</returns>
        private string @GetAdminEmailAddressList()
        {
            // Get admin email list.
            string result = string.Empty;
            EntityBaseCollection<User> administratorList =
                UserDao.GetByRoleType(this.AdminUserRoleTypeId);
            if (administratorList.Count == 0)
                throw new ApplicationException("The system does not have an administrator.");
            administratorList.ForEach(a => result += a.EmailAddress + @", ");
            result = result.Substring(0, result.Length - 2);
            return result;
        }

        #endregion Private methods
    }
}