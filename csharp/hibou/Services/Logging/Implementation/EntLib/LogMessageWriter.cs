using System;
using System.Diagnostics;
using Keane.CH.Framework.Services.Logging.Contracts.Data;
using MSEntLibLogger = Microsoft.Practices.EnterpriseLibrary.Logging.Logger;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Keane.CH.Framework.Services.Logging.Implementation.EntLib
{
    /// <summary>
    /// Encapsulates logging by wrapping overloads around the Microsoft.Practices.EnterpriseLibrary.Logging.Logger.
    /// </summary>
    /// <created by="Mark Morgan" date="21-Sep-2007" />
    /// <created by="Mark Morgan" date="01-Jul-2008" />
    internal class LogMessageWriter
    {
        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        internal LogMessageWriter()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected void InitialiseMembers()
        {
            this.DefaultEventId = default(int);
            this.Name = @"Untitled";
            this.Title = @"Keane.CH.Services.Logging";
        }

        #endregion Ctor.

        #region Properties

        /// <summary>
        /// Gets/Sets the name of the log writer.
        /// </summary>
        public string Title
        { get; set; }

        /// <summary>
        /// Gets/Sets the name of the log writer.
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// Gets/Sets the default event id.
        /// </summary>
        public int DefaultEventId
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Writes a log entry.
        /// </summary>
        /// <param name="logMessage">The logging message.</param>
        public virtual void Write(
            LogMessage logMessage)
        {
            // Assign default values (if necessary).
            if (logMessage.Id == 0)
                logMessage.Id  = DefaultEventId;

            // Sink the  entry to the EntLib Logging API.
            MSEntLibLogger.Write(
                LogMessageXmlWriter.AsXml(logMessage),
                new string[] { logMessage.WriterType.ToString() },
                (int)logMessage.Priority,
                logMessage.Id,
                GetTraceEventType(logMessage.EventType),
                this.Title);
        }

        /// <summary>
        /// Gets the System.Diagnostics.TraceEventType from the logging messaage event type.
        /// </summary>
        /// <param name="logMessage">The log message being processed.</param>
        /// <returns>The System.Diagnostics.TraceEventType.</returns>
        private TraceEventType GetTraceEventType(LogEventType logEventType)
        {
            return (TraceEventType)Enum.Parse(typeof(TraceEventType), logEventType.ToString());
        }

        #endregion Methods
    }
}