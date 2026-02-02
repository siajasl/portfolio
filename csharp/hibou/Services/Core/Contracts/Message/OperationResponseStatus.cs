using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Core.Operation
{
    /// <summary>
    /// Enumeration over the various operation response statae.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public enum OperationResponseStatus
    {
        [DataMember()]
        Unknown = 0,
        [DataMember()]
        Success = 1,
        [DataMember()]
        Failure = 2,
        [DataMember()]
        Exception = 3,
    }
}