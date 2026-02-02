using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Enumeration over commonly used logging priorities.
    /// </summary>
    /// <created by="Mark Morgan" date="21-Sep-2007" />
    /// <created by="Mark Morgan" date="01-Jul-2008" />
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public enum LogMessageVerbosityType
    {
        /// <summary>
        /// Low level of verbosity.
        /// </summary>
        [EnumMember()]
        Low = 1,
        /// <summary>
        /// Medium level of verbosity.
        /// </summary>
        [EnumMember()]
        Medium,
        /// <summary>
        /// High level of verbosity.
        /// </summary>
        [EnumMember()]
        High,
    }
}
