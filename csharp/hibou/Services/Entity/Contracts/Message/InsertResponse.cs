using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Entity.Contracts.Message
{
    /// <summary>
    /// Encapsulates insert entity response informaton.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Entity")]
    [Serializable]
    public class InsertResponse : 
        OperationResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id of the inserted instance.
        /// </summary>        
        [DataMember()]
        public int EntityId
        { get; set; }

        #endregion Properties
    }
}