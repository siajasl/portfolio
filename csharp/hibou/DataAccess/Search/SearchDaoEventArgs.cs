using System;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using System.Collections.Generic;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Search dao event arguments.
    /// </summary>
    public class SearchDaoEventArgs : 
        EventArgs
    {
        #region Properties

        /// <summary>
        /// The search criteria.
        /// </summary>
        public SearchCriteriaBase Criteria
        { get; set; }

        /// <summary>
        /// The search result.
        /// </summary>
        public SearchResult Result
        { get; set; }

        #endregion Properties

        #region Static factory

        /// <summary>
        /// Returns an instance.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <returns>An instance.</returns>
        internal static SearchDaoEventArgs Create(
            SearchCriteriaBase searchCriteria)
        {
            SearchDaoEventArgs result = new SearchDaoEventArgs();
            result.Criteria = searchCriteria;
            return result;
        }

        /// <summary>
        /// Returns an instance.
        /// </summary>
        /// <param name="searchCriteria">The search criteria.</param>
        /// <param name="searchResult">The search result.</param>
        /// <returns>An instance.</returns>
        internal static SearchDaoEventArgs Create(
            SearchCriteriaBase searchCriteria,
            SearchResult searchResult)
        {
            SearchDaoEventArgs result = new SearchDaoEventArgs();
            result.Criteria = searchCriteria;
            result.Result = searchResult;
            return result;
        }

        #endregion Static factory
    }
}
