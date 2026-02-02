using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Enumeration over commonly used logging priorities.
    /// </summary>
    /// <created by="Mark Morgan" date="21-Sep-2007" />
    /// <created by="Mark Morgan" date="01-Jul-2008" />
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public enum LogMessagePriorityType 
    {
        /// <summary>
        /// Undetermined priority.
        /// </summary>
        [EnumMember()]
        Undetermined = 0,
        /// <summary>
        /// Low priority.
        /// </summary>
        [EnumMember()]
        Low = 1,
        /// <summary>
        /// Medium priority.
        /// </summary>
        [EnumMember()]
        Medium = 2,
        /// <summary>
        /// High priority.
        /// </summary>
        [EnumMember()]
        High = 3,
        /// <summary>
        /// Critical priority.
        /// </summary>
        [EnumMember()]
        Critical = 9
    }
}
