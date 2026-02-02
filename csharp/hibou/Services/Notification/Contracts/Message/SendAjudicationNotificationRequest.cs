using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Notification.Contracts
{
    /// <summary>
    /// Encapsulates information required to send a protected entity decision notification request.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public class SendAjudicationNotificationRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id of the modification being decided upon.
        /// </summary>        
        [DataMember()]
        public int ModificationId
        { get; set; }

        /// <summary>
        /// Gets or sets the decision type.
        /// </summary>        
        [DataMember()]
        public AjudicationDecisionType DecisionType
        { get; set; }

        #endregion Properties
    }
}
