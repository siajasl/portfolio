using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Enumeration over the log trace types.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jul-2008" />
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public enum LogEventType
    {
        /// <summary>
        /// Fatal error or application crash.
        /// </summary>
        [EnumMember()]
        Critical = 1,
        /// <summary>
        /// Recoverable error.
        /// </summary>
        [EnumMember()]
        Error = 2,
        /// <summary>
        /// Noncritical problem.
        /// </summary>
        [EnumMember()]
        Warning = 4,
        /// <summary>
        /// Informational message.
        /// </summary>
        [EnumMember()]
        Information = 8,
        /// <summary>
        /// Debugging trace.
        /// </summary>
        [EnumMember()]
        Verbose = 16,
        /// <summary>
        /// Starting of a logical operation.
        /// </summary>
        [EnumMember()]
        Start = 256,
        /// <summary>
        /// Stopping of a logical operation.
        /// </summary>
        [EnumMember()]
        Stop = 512,
        /// <summary>
        /// Suspension of a logical operation.
        /// </summary>
        [EnumMember()]
        Suspend = 1024,
        /// <summary>
        /// Resumption of a logical operation.
        /// </summary>
        [EnumMember()]
        Resume = 2048,
        /// <summary>
        /// Changing of correlation identity.
        /// </summary>
        [EnumMember()]
        Transfer = 4096,
    }
}
