using System;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using Keane.CH.Framework.Services.Logging.Contracts.Data;
using Keane.CH.Framework.Services.Logging.Contracts.Message;

namespace Keane.CH.Framework.Services.Logging.Contracts.Service
{
    /// <summary>
    /// Encapsulates standardized logging service operations.
    /// </summary>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    public interface ILoggingService
    {
        /// <summary>
        /// Writes a log message to the underlying log repositories.
        /// </summary>
        /// <param name="logRequest">The log request.</param>
        /// <remarks>
        /// This is marked as a one way operation so as to permit MSMQ.
        /// </remarks>
        [OperationContract(IsOneWay = true)]
        void Log(LogRequest logRequest);
    }
}