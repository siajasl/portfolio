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
    /// Encapsulates information required to insert an entity.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Entity")]
    [Serializable]
    public class SearchResponse : 
        OperationResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the search result returned from the search.
        /// </summary>        
        [DataMember()]
        public SearchResult Result
        { get; set; }

        #endregion Properties
    }
}
