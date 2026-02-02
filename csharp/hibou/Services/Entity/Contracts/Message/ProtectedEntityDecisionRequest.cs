using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Entity;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Core.Entity.Protected;

namespace Keane.CH.Framework.Services.Entity.Contracts
{
    /// <summary>
    /// Encapsulates the information required to send a protected entity modification decision notification.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Entity")]
    [Serializable]
    public class ProtectedEntityDecisionRequest :
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the protected entity modification id.
        /// </summary>        
        [DataMember()]
        public int ModificationId
        { get; set; }

        /// <summary>
        /// Gets or sets the type of modification being requested.
        /// </summary>        
        [DataMember()]
        public ProtectedEntityModificationDecisionType DecisionType
        { get; set; }

        #endregion Properties
    }
}