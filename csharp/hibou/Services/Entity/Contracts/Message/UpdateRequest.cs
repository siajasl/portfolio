using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Entity.Contracts.Message
{
    /// <summary>
    /// Encapsulates information required to update an entity.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Entity")]
    [Serializable]
    public class UpdateRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the instance being inserted.
        /// </summary>        
        [DataMember()]
        public EntityBase Entity
        { get; set; }

        #endregion Properties
    }
}
