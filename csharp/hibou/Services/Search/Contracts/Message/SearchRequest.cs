using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.Services.Search.Contracts.Message
{
    /// <summary>
    /// Encapsulates information required to execute a search.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Entity")]
    [Serializable]
    public class SearchRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the search criteria used to drive the search.
        /// </summary>        
        [DataMember()]
        public SearchCriteriaBase Criteria
        { get; set; }

        #endregion Properties
    }
}
