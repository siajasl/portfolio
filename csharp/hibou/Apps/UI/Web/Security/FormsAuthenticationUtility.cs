using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web;
using System.Web.Configuration;

namespace Keane.CH.Framework.Apps.UI.Web.Security
{
    /// <summary>
    /// Encapsualtes forms authentication utility methods.
    /// </summary>
    public static class FormsAuthenticationUtility
    {
        /// <summary>
        /// Creates a forms authentication ticket.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="userData">The user data.</param>
        /// <returns>a forms authentication ticket to be used during the authentication process.</returns>
        public static FormsAuthenticationTicket
            CreateFormsAuthenticationTicket(
                HttpContext context, string userName, string userData)
        {
            return 
                CreateFormsAuthenticationTicket(context, userName, userData, false);
        }

        /// <summary>
        /// Creates a forms authentication ticket.
        /// </summary>
        /// <param name="context">The http context.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="userData">The user data.</param>
        /// <param name="setTicket">Flag indicating whether the ticket should be set.</param>
        /// <returns>a forms authentication ticket to be used during the authentication process.</returns>
        public static FormsAuthenticationTicket
            CreateFormsAuthenticationTicket(
                HttpContext context, string userName, string userData, bool setTicket)
        { 
            // Defensive programming.
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            // Derive the authentication section configuration
            AuthenticationSection section = 
                (AuthenticationSection)context.GetSection("system.web/authentication");
            if (section.Mode != AuthenticationMode.Forms)
                throw new HttpException("Custom forms authentication tickets can only be issued when the web-site is using Formas authentication.");
            
            // Derive default values.
            int timeout = (int)section.Forms.Timeout.TotalMinutes;
            if (string.IsNullOrEmpty(userData))
                userData = string.Empty;

            // Create values.
            FormsAuthenticationTicket ticket =
                new FormsAuthenticationTicket(
                    1,                                      // Version
                    userName,                               // User Name
                    DateTime.Now,                           // Creation time
                    DateTime.Now.AddMinutes(timeout),       // Expiration time
                    false,                                  // Persistent ?
                    userData,                               // User data
                    FormsAuthentication.FormsCookiePath);   // Cookie path

            // If instructed set the ticket.
            if (setTicket)
                SetAuthenticationTicket(context, ticket);

            // Return value.
            return ticket;
        }

        /// <summary>
        /// Issues the passed authentication ticket.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ticket"></param>
        public static void SetAuthenticationTicket(
            HttpContext context, FormsAuthenticationTicket ticket)
        {
            // Defensive programming.
            if (context == null)
                throw new ArgumentNullException("context");
            if (ticket == null)
                throw new ArgumentNullException("ticket");
            if (!context.Request.IsSecureConnection && FormsAuthentication.RequireSSL)
                throw new HttpException("Forms authentication ticket requires SSL");

            // Encrypt the ticket.
            string authCoookie = FormsAuthentication.Encrypt(ticket);

            // Initialise authorization cookie.
            HttpCookie cookie = new 
                HttpCookie(FormsAuthentication.FormsCookieName);
            cookie.Value = authCoookie;

            // Respect ssl & domain settings.
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Domain = FormsAuthentication.CookieDomain;

            // ensure that cookie is not available to client script.
            cookie.HttpOnly = true;

            // Set the cookie.
            context.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Authenticates using the passed user name and user roles array.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="userRoles">the array of user roles.</param>
        public static void Authenticate(
            HttpContext context, string userName, string[] userRoles)
        {
            // Defensive programming.
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");
            if (userRoles == null)
                throw new ArgumentNullException("userRoles");

            // Userdata = roles encoded as a delimited string.
            string userData = string.Join("|", userRoles);

            // Sink to core method.
            Authenticate(context, userName, userData);
        }

        /// <summary>
        /// Authenticates using the passed user name & data.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="userData">The user data.</param>
        public static void Authenticate(
            HttpContext context, string userName, string userData)
        {
            // Defensive programming.
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            // Create & issue an authentication ticket.
            CreateFormsAuthenticationTicket(
               context, userName, userData, true);

            // Redirect back to the originially requested resource.
            string redirectUrl = FormsAuthentication.GetRedirectUrl(userName, false);
            if (string.IsNullOrEmpty(redirectUrl))
                redirectUrl = FormsAuthentication.LoginUrl;
            context.Response.Redirect(redirectUrl, false);
        }
    }
}