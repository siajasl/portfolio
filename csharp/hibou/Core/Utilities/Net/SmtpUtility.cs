using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Keane.CH.Framework.Services.Core.Operation;
using System.Collections.Specialized;
using System.Configuration;
using Keane.CH.Framework.Core.Resources.Configuration;
using System.Net;

namespace Keane.CH.Framework.Core.Utilities.Mail
{
    /// <summary>
    /// Encapsualtes smtp utility functions.
    /// </summary>
    public sealed class SmtpUtility
    {
        #region Ctor.

        private SmtpUtility() { }

        #endregion Ctor.

        #region Methods

        /// <summary>
        /// Returns a default smtp client configured via app.config.
        /// </summary>
        /// <returns>An smtp client.</returns>
        public static SmtpClient GetDefaultSmtpClient()
        {
            // Instantiate the smtp client.
            // This picks up settings defined in <system.net> config section.
            SmtpClient result = new SmtpClient();

            // Pick up custom config settings that are not defined in <system.net>
            NameValueCollection config =
                ConfigurationManager.GetSection(MailSettings.CustomMailSettings) as NameValueCollection;
            if (config != null)
            {
                // Enable ssl.
                if (!string.IsNullOrEmpty(config[MailSettings.EnableSsl]))
                {
                    bool enableSsl;
                    bool.TryParse(config[MailSettings.EnableSsl], out enableSsl);
                    result.EnableSsl = enableSsl;
                }
                // Time out.
                if (!string.IsNullOrEmpty(config[MailSettings.Timeout]))
                {
                    int timeout;
                    int.TryParse(config[MailSettings.Timeout], out timeout);
                    if (timeout > 0)
                        result.Timeout = timeout;
                }
            }

            // Return.
            return result;
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="smtpClient">The configured smtp client used to send the email.</param>
        /// <param name="email">The details of the email being sent.</param>
        public static void SendEmail(SmtpClient smtpClient, EmailDetails email)
        {
            // Defensive programming.
            if (smtpClient == null)
                throw new ArgumentNullException("smtpClient");
            if (email == null)
                throw new ArgumentNullException("email");

            // Prepare the mail message from the passed details.
            MailMessage message = new MailMessage();
            message.To.Add(email.To);
            message.Body = email.Body;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = email.Subject;
            message.SubjectEncoding = Encoding.UTF8;
            if (!string.IsNullOrEmpty(email.From))
                message.From = new MailAddress(email.From);
            if (!string.IsNullOrEmpty(email.Bcc))
                message.Bcc.Add(new MailAddress(email.Bcc));
            if (!string.IsNullOrEmpty(email.CC))
                message.CC.Add(new MailAddress(email.CC));

            // Send it via smtp.
            smtpClient.Send(message);
        }

        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">The details of the email being sent.</param>
        public static void SendEmail(EmailDetails email)
        {
            SmtpClient smtpClient = GetDefaultSmtpClient();
            SendEmail(smtpClient, email);
        }

        #endregion Methods
    }
}