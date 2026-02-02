using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data.Search
{
    /// <summary>
    /// An item returned from a search.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public class AjudicationSearchItem 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the modification id.
        /// </summary>
        [DataMember()]
        public int EntityModificationId
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the request type.
        /// </summary>
        [DataMember()]
        public ProtectedEntityModificationType RequestTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the request type description.
        /// </summary>
        [DataMember()]
        public string RequestType
        { get; set; }

        /// <summary>
        /// Gets or sets the date upon which the modification request occurred.
        /// </summary>
        [DataMember()]
        public DateTime RequestDate
        { get; set; }

        /// <summary>
        /// Gets the formatted date upon which the modification request occurred.
        /// </summary>
        public string RequestDateFormatted
        { get { return RequestDate.ToShortDateString(); } }

        /// <summary>
        /// Gets or sets the name of the user who requested the modification.
        /// </summary>
        [DataMember()]
        public string RequestUserName
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the target entity type.
        /// </summary>
        [DataMember()]
        public int TargetEntityTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the target entity type description.
        /// </summary>
        [DataMember()]
        public string TargetEntityType
        { get; set; }
        
        /// <summary>
        /// Gets or sets the target entity id.
        /// </summary>
        [DataMember()]
        public int TargetEntityId
        { get; set; }

        /// <summary>
        /// Gets or sets the target entity application specific reference number.
        /// </summary>
        [DataMember()]
        public string TargetEntityReferenceNumber
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the decision type.
        /// </summary>
        [DataMember()]
        public AjudicationDecisionType DecisionTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the decision type.
        /// </summary>        
        [DataMember()]
        public string DecisionType
        { get; set; }

        /// <summary>
        /// Gets or sets the decision date.
        /// </summary>        
        [DataMember()]
        public DateTime DecisionDate
        { get; set; }

        /// <summary>
        /// Gets the formatted decision date.
        /// </summary>        
        public string DecisionDateFormatted
        { 
            get 
            {
                if (DecisionDate.Equals(DateTime.MinValue))
                    return String.Empty;
                else
                    return DecisionDate.ToShortDateString(); 
            } 
        }

        /// <summary>
        /// Gets or sets the name of the user who made the modification decision.
        /// </summary>
        [DataMember()]
        public string DecisionUserName
        { get; set; }

        #endregion Properties
    }
}