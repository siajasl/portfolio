using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using Keane.CH.Framework.Services.Logging.Contracts.Service;
using Keane.CH.Framework.Services.Logging.Implementation.EntLib.Resources;
using Keane.CH.Framework.Services.Logging.Contracts.Data;
using Keane.CH.Framework.Services.Logging.Contracts.Message;

namespace Keane.CH.Framework.Services.Logging.Implementation.EntLib
{
    /// <summary>
    /// Wrapper around class Microsoft.Practices.EnterpriseLibrary.Logging.Logger.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jul-2008" />
    public class LoggingService :
        ILoggingService
    {
        #region ILogger Members

        /// <summary>
        /// Writes a log message to the underlying log repositories.
        /// </summary>
        /// <param name="logRequest">The log request.</param>
        public void Log(LogRequest logRequest)
        {
            // Defensive programming.
            Debug.Assert(logRequest != null);
            Debug.Assert(logRequest.Message != null);

            // Derive the writer & write.
            LogMessage logMessage = logRequest.Message;
            LogMessageWriter writer = 
                LogMessageWriterFactory.GetWriter(logMessage.WriterType);
            writer.Write(logMessage);
        }

        #endregion ILogger Members
    }
}