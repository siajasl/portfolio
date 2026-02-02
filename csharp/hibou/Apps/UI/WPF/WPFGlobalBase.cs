using System;
using System.Security;
using System.Windows;
using System.Windows.Threading;
using Keane.CH.Framework.Services.Logging.Contracts.Data;
using Keane.CH.Framework.Services.Logging.Contracts.Service;
using Keane.CH.Framework.Services.Logging.Implementation.EntLib;
using KeaneLogRequest = Keane.CH.Framework.Services.Logging.Contracts.Message.LogRequest;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Global application base class.
    /// </summary>
    public abstract class WPFGlobalBase : Application
    {
        #region Ctor

        public WPFGlobalBase()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            base.DispatcherUnhandledException += Application_Error;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets the application display name.
        /// </summary>
        protected abstract string ApplicationDisplayName
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

        #region WPF Application event handlers

        /// <summary>
        /// Application start.
        /// </summary>
        /// <param name="e">The startup event args.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Log.
            this.SendLogMessage(string.Format(@"{0} Application starting", this.ApplicationDisplayName));

            // Start.
            this.OnApplicationStart();

            // Log.
            this.SendLogMessage(string.Format(@"{0} Application started", this.ApplicationDisplayName));
        }

        /// <summary>
        /// Application end.
        /// </summary>
        /// <param name="e">The exit event args.</param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

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
        protected void Application_Error(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Exception fault = e.Exception;
            if (fault == null)
                return;

            // Log error.
            if (IsSecurityRelated(fault))
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
            }
            
            MessageBoxResult result = MessageBox.Show("Application must exit:\n\n" + e.Exception.Message + "!", 
                ApplicationDisplayName, MessageBoxButton.OK, MessageBoxImage.Error);

            // Return exit code.
            this.Shutdown(-1);

            // Prevent default unhandled exception processing.
            e.Handled = true;
        }

        #endregion WPF Application event handlers

        #region Private methods

        /// <summary>
        /// Determines if the http code is security related.
        /// </summary>
        /// <param name="httpCode">An http code.</param>
        /// <returns>True if security related.</returns>
        private bool IsSecurityRelated(Exception fault)
        {
            // TODO refactor to a better white list.
            if (fault is SecurityException ||
                fault is UnauthorizedAccessException)
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
