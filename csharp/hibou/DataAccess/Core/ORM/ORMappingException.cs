using System;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.DataAccess.Core
{
    /// <summary>
    /// OR mappign exception.
    /// </summary>
    public class ORMappingException : 
        Exception
    {
        #region Constructors

        public ORMappingException()
            : base()
        { }
        public ORMappingException(string message)
            : base(message)
        { }
        public ORMappingException(string message, Exception innerException)
            : base(message, innerException)
        { }
        public ORMappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        #endregion Constructors
    }
}