using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Logging.Contracts.Service;
using Keane.CH.Framework.Services.Logging.Implementation.EntLib;
using Keane.CH.Framework.Services.Logging.Contracts.Data;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Threading;
using KeaneLogRequest = Keane.CH.Framework.Services.Logging.Contracts.Message.LogRequest;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Global application base class.
    /// </summary>
    public abstract class WebGlobalBase : 
        System.Web.HttpApplication
    {
        #region Ctor

        public WebGlobalBase()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            base.AuthenticateRequest += Application_AuthenticateRequest;
            base.Error += Application_Error;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets the application display name.
        /// </summary>
        protected abstract string ApplicationDisplayName
        { get; }

        /// <summary>
        /// Gets a flag indicating whether a page fault should be placed in the session cache for later use.
        /// </summary>
        protected abstract bool CachePageFaultInSession
        { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Executed when the application starts.
        /// </summary>
        protected virtual void OnApplicationStart()
        { }

        /// <summary>
        /// Executed when the application stops.
        /// </summary>
        protected virtual void OnApplicationStop()
        { }

        /// <summary>
        /// Executed when the application errors.
        /// </summary>
        /// <param name="fault">The fault that hsa occurred.</param>
        protected virtual void OnApplicationError(Exception fault)
        { }

        /// <summary>
        /// Executed when a security related error occurs.
        /// </summary>
        /// <param name="fault">The fault that has occurred.</param>
        protected virtual void OnApplicationSecurityError(Exception fault)
        { }

        #endregion Methods

        #region HttpApplication event handlers

        /// <summary>
        /// Application start event handler.
        /// </summary>
        /// <param name="sender">The event control.</param>
        /// <param name="e">The event args.</param>
        protected void Application_Start(object sender, EventArgs e)
        {
            // Log.
            this.SendLogMessage(string.Format(@"{0} Application starting", this.ApplicationDisplayName));

            // Start.
            this.OnApplicationStart();

            // Log.
            this.SendLogMessage(string.Format(@"{0} Application started", this.ApplicationDisplayName));
        }

        /// <summary>
        /// Application end event handler.
        /// </summary>
        /// <param name="sender">The event control.</param>
        /// <param name="e">The event args.</param>
        protected void Application_End(object sender, EventArgs e)
        {
            // Stop the application (& suppress exceptions).
            try
            {
                // Log.
                this.SendLogMessage(string.Format(@"{0} Application stopping", this.ApplicationDisplayName));

                // Stop.
                this.OnApplicationStop();

                // Log.
                this.SendLogMessage(string.Format(@"{0} Application stopped", this.ApplicationDisplayName));
            }
            catch { }
        }

        /// <summary>
        /// Application error event handler.
        /// </summary>
        /// <param name="sender">The event control.</param>
        /// <param name="e">The event args.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception fault = Server.GetLastError();
            if (fault == null)
                return;

            // Derive inner exception.
            if (fault is HttpUnhandledException && 
                fault.InnerException != null)
            {
                fault = fault.InnerException;
            }

            // Derive http error code.
            int httpErrorCode = default(int);
            if (fault is HttpException)
            {
                httpErrorCode = (fault as HttpException).GetHttpCode();
            }
            
            // Log error.
            if (IsSecurityRelatedHttpCode(httpErrorCode))
            {
                SendLogMessage(
                    string.Format(@"{0} Application exception!", this.ApplicationDisplayName), 
                    fault, 
                    LogMessageWriterType.Security);
                OnApplicationSecurityError(fault);
            }
            else
            {
                SendLogMessage(
                    string.Format(@"{0} Application exception!", this.ApplicationDisplayName), 
                    fault);            
                OnApplicationError(fault);
                if (CachePageFaultInSession && Context.Session != null)
                    Context.Session[@"SessionFault"] = fault;                
            }

            // Remove error from stack.
            Server.ClearError();
        }

        /// <summary>
        /// Application authentication event handler.
        /// </summary>
        /// <param name="sender">The event control.</param>
        /// <param name="e">The event args.</param>
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            if (context.Request.IsAuthenticated)
            {
                // Extract roles from authentication ticket.
                FormsIdentity id = (FormsIdentity)context.User.Identity;
                string[] roles = id.Ticket.UserData.Split('|');

                // Assign the principal.
                GenericPrincipal p =
                    new GenericPrincipal(context.User.Identity, roles);
                context.User = Thread.CurrentPrincipal = p;
            }
        }

        #endregion HttpApplication event handlers

        #region Private methods

        /// <summary>
        /// Determines if the http code is security related.
        /// </summary>
        /// <param name="httpCode">An http code.</param>
        /// <returns>True if security related.</returns>
        private bool IsSecurityRelatedHttpCode(int httpCode)
        {
            // TODO refactor to a better white list.
            const int HTTP_ERROR_FILE_NOT_FOUND = 404;
            const int HTTP_ERROR_PERMISSION_DENIED = 403;
            if (httpCode == HTTP_ERROR_FILE_NOT_FOUND ||
                httpCode == HTTP_ERROR_PERMISSION_DENIED)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Sends a message to the logging service.
        /// </summary>
        /// <param name="message">The message to be sent to the service.</param>
        /// <param name="fault">The fault that may have occurred.</param>
        /// <param name="writerType">The type of writer to send the message to.</param>
        public void SendLogMessage(string message, Exception fault, LogMessageWriterType writerType)
        {
            // Prepare log message.
            LogMessage logMessage = new LogMessage();
            logMessage.Message = message;
            logMessage.WriterType = writerType;
            if (fault != null)
                logMessage.SetFault(fault);

            // Invoke service operation.
            KeaneLogRequest request = new KeaneLogRequest() 
            { 
                Message = logMessage
            };
            ILoggingService service = new LoggingService();
            service.Log(request);
        }

        /// <summary>
        /// Sends a message to the logging service.
        /// </summary>
        /// <param name="message">The message to be sent to the service.</param>
        /// <param name="fault">The fault that may have occurred.</param>
        public void SendLogMessage(string message, Exception fault)
        {
            SendLogMessage(message, fault, LogMessageWriterType.Information);
        }

        /// <summary>
        /// Sends a message to the logging service.
        /// </summary>
        /// <param name="message">the message to be sent to the service.</param>
        public void SendLogMessage(string message)
        {
            SendLogMessage(message, null);
        }

        #endregion Private methods
    }
}
