using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Core.Operation
{
    /// <summary>
    /// Enumeratoin over the types of context message that may be sent.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public enum OperationContextMessageType
    {
        /// <summary>
        /// For non initialized values only.
        /// </summary>
        [DataMember()]
        None = 0,
        /// <summary>
        /// Fatal error message
        /// </summary>
        [DataMember()]
        Fatal = 1,
        /// <summary>
        /// Error message
        /// </summary>
        [DataMember()]
        Error = 2,
        /// <summary>
        /// Warning message
        /// </summary>
        [DataMember()]
        Warning = 3,
        /// <summary>
        /// Informational message
        /// </summary>
        [DataMember()]
        Informational = 4,
        /// <summary>
        /// validation message
        /// </summary>
        [DataMember()]
        Validation = 5,
        /// <summary>
        /// validation message
        /// </summary>
        [DataMember()]
        Gui = 6
    }
}
