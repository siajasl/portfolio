using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Notification.Contracts
{
    /// <summary>
    /// Encapsulates the information required for to send a protected entity modification request notification.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public class SendModificationNotificationRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>        
        [DataMember()]
        public int EntityId
        { get; set; }

        /// <summary>
        /// Gets or sets the entity type id.
        /// </summary>        
        [DataMember()]
        public int EntityTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the type of modification being requested.
        /// </summary>        
        [DataMember()]
        public ProtectedEntityModificationType ModificationType
        { get; set; }

        #endregion Properties
    }
}