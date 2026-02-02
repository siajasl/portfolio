using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data.Search
{
    /// <summary>
    /// Criteria used to perform a search.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public class AjudicationSearchCriteria : 
        SearchCriteriaBase 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id of the entity being modified.
        /// </summary>
        [DataMember()]
        public int EntityTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the modification type being requested.
        /// </summary>
        [DataMember()]
        public int RequestTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the decision being modified.
        /// </summary>
        [DataMember()]
        public int DecisionTypeId
        { get; set; }

        #endregion Properties
    }
}