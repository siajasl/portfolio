using System;
using System.Linq;
using Keane.CH.Framework.DataAccess.Search;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Services.Core;
using Keane.CH.Framework.Services.Search.Contracts;
using Keane.CH.Framework.Services.Search.Contracts.Message;

namespace Keane.CH.Framework.Services.Search.Implementation
{
    /// <summary>
    /// Search specific service base class for encapsulating common functionality.
    /// </summary>
    /// <typeparam name="SR">The type of search result item returned by the search.</typeparam>
    public class SearchService<SR> :
        ServiceImplementationBase,
        ISearchService
        where SR : new()
    {
        #region Properties

        #region Collaborators

        /// <summary>
        /// Gets or sets the associated entity dao.
        /// </summary>
        public ISearchDao SearchDao
        { get; set; }

        #endregion Collaborators

        #endregion Properties

        #region Methods

        /// <summary>
        /// Executes a search.
        /// </summary>
        /// <param name="request">The search criteria.</param>
        /// <returns>Search results.</returns>
        public SearchResponse
            Search(SearchRequest request)
        {
            try
            {
                // Search the repository.
                SearchResult searchResult = SearchDao.Search(request.Criteria);

                // Restrict the number to be returned.
                searchResult.SetMaximumSize(request.Criteria.MaximumResults);

                // TODO caclulate time.

                // Generate service response.
                SearchResponse response = new SearchResponse();
                response.Status = OperationResponseStatus.Success;
                response.Result = searchResult;
                return response;
            }
            catch (Exception ex)
            {
                SearchResponse response = new SearchResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        #endregion Methods
    }
}