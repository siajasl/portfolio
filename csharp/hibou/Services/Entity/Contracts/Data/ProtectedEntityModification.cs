using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.DataContract;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data
{
    /// <summary>
    /// Encapsulates the details required to store an entity modification.
    /// </summary>
    /// <remarks>
    /// This is used within scenarios in which entities are protected.  
    /// Typically administrators conduct a review & decide whether 
    /// to accept or reject the modification.
    /// </remarks>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class ProtectedEntityModification :
        EntityBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the date upon which the modification request occurred.
        /// </summary>
        [DataMember()]
        public DateTime RequestDate
        { get; set; }

        /// <summary>
        /// Gets or sets the type of modification being requested.
        /// </summary>
        [DataMember()]
        public ProtectedEntityModificationType RequestType
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the user who requested the modification.
        /// </summary>
        [DataMember()]
        public int RequestUserId
        { get; set; }

        /// <summary>
        /// Gets or sets the target entity id.
        /// </summary>
        [DataMember()]
        public int TargetEntityId
        { get; set; }

        /// <summary>
        /// Gets or sets the id of target entity type.
        /// </summary>
        /// <remarks>
        /// Typically this will correspond to an enumeration value.
        /// </remarks>
        [DataMember()]
        public int TargetEntityTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the name of target entity type.
        /// </summary>
        /// <remarks>
        /// This will be during instance deserialization/identification.
        /// </remarks>
        [DataMember()]
        public string TargetEntityTypeName
        { get; set; }

        /// <summary>
        /// Gets or sets the target entity serialized.
        /// </summary>
        [DataMember()]
        public string TargetEntitySerialized
        { get; set; }

        /// <summary>
        /// Gets or sets the decision type.
        /// </summary>        
        [DataMember()]
        public AjudicationDecisionType DecisionType
        { get; set; }

        /// <summary>
        /// Gets whether a decision is required.
        /// </summary>
        public bool RequiresDecision
        { get { return (DecisionType == AjudicationDecisionType.Undecided); } }

        /// <summary>
        /// Gets or sets the decision date.
        /// </summary>        
        [DataMember()]
        public DateTime DecisionDate
        { get; set; }

        /// <summary>
        /// Gets or sets the name of the user who made the modification decision.
        /// </summary>
        [DataMember()]
        public string DecisionUserName
        { get; set; }

        #endregion Properties
    }
}