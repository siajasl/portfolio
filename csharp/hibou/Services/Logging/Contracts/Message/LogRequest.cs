using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Logging.Contracts.Data;

namespace Keane.CH.Framework.Services.Logging.Contracts.Message
{
    /// <summary>
    /// Encapsulates information required to write a log entry.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public class LogRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the log message to be logged.
        /// </summary>        
        [DataMember()]
        public LogMessage Message
        { get; set; }

        #endregion Properties
    }
}
