using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Encapsulates a search data access operation.
    /// </summary>
    /// <typeparam name="I">A sub-class representing the search result.</typeparam>
    public interface ISearchDao
    {
        /// <summary>
        /// Gets the associated events (permits event listening).
        /// </summary>
        ISearchDaoEvents Events
        { get; }
        
        /// <summary>
        /// Executes the search.
        /// </summary>
        /// <param name="criteria">The search criteria.</param>
        /// <returns>The search results.</returns>
        SearchResult Search(SearchCriteriaBase criteria);
    }
}