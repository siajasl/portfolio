using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Enumeration over the array of supported log writer types.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public enum LogMessageWriterType
    {
        /// <summary>
        /// Application log message writer.
        /// </summary>
        [EnumMember()]
        Application = 10,
        /// <summary>
        /// Configuration log message writer.
        /// </summary>
        [EnumMember()]
        Configuration,
        /// <summary>
        /// Debug log message writer.
        /// </summary>
        [EnumMember()]
        Debug,
        /// <summary>
        /// Error log message writer.
        /// </summary>
        [EnumMember()]
        Error,
        /// <summary>
        /// ExternalApi log message writer.
        /// </summary>
        [EnumMember()]
        ErrorCritical,
        /// <summary>
        /// Application log message writer.
        /// </summary>
        [EnumMember()]
        ExternalApi,
        /// <summary>
        /// ExternalWebService log message writer.
        /// </summary>
        [EnumMember()]
        ExternalWebService,
        /// <summary>
        /// Gui log message writer.
        /// </summary>
        [EnumMember()]
        General,
        /// <summary>
        /// Gui log message writer.
        /// </summary>
        [EnumMember()]
        Gui,
        /// <summary>
        /// Operational log message writer.
        /// </summary>
        [EnumMember()]
        Information,
        /// <summary>
        /// Application log message writer.
        /// </summary>
        [EnumMember()]
        Operational,
        /// <summary>
        /// SecurityCritical log message writer.
        /// </summary>
        [EnumMember()]
        Security,
        /// <summary>
        /// Application log message writer.
        /// </summary>
        [EnumMember()]
        SecurityCritical,
        /// <summary>
        /// Workflow log message writer.
        /// </summary>
        [EnumMember()]
        Workflow,
    }
}