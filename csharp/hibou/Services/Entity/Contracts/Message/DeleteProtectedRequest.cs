using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Entity.Contracts.Message
{
    /// <summary>
    /// Encapsulates information required to delete a protected entity.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Entity")]
    [Serializable]
    public class DeleteProtectedRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id of the entity being deleted.
        /// </summary>        
        [DataMember()]
        public int EntityId
        { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity being deleted.
        /// </summary>        
        [DataMember()]
        public Type EntityType
        { get; set; }

        #endregion Properties
    }
}
