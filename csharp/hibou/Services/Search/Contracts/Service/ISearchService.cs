using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using System.ServiceModel;
using Keane.CH.Framework.Services.Search.Contracts.Message;

namespace Keane.CH.Framework.Services.Search.Contracts
{
    /// <summary>
    /// Search specific service interface.
    /// </summary>
    /// <typeparam name="SC">The search criteria.</typeparam>
    /// <typeparam name="SR">The search result item.</typeparam>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01/Services/Search")]
    public interface ISearchService
    {
        [OperationContract()]
        SearchResponse Search(SearchRequest request);   
    }
}