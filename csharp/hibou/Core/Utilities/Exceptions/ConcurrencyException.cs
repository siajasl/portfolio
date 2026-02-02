using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Core.Utilities.Exceptions
{
    /// <summary>
    /// An exception raised when a concurrent update has been attempted.
    /// </summary>
    /// <remarks>
    /// This is raised during race conditions where 2 users are 
    /// attempting to update the same entity.
    /// </remarks>
    public class ConcurrencyException : Exception
    {
        #region Constructors

        public ConcurrencyException()
        { }
        public ConcurrencyException(string message)
            : base(message)
        { }
        public ConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
        public ConcurrencyException(string message, Exception innerException)
            : base(message, innerException)
        { }

        #endregion Constructors
    }
}