using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Entity.Contracts.Message
{
    /// <summary>
    /// Encapsulates delete entity response informaton.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Entity")]
    [Serializable]
    public class DeleteResponse : 
        OperationResponse
    { }
}