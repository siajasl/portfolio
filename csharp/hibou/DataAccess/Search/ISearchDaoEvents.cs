using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Encapsulates search data access operation events.
    /// </summary>
    /// <typeparam name="I">A sub-class representing the search result.</typeparam>
    public interface ISearchDaoEvents
    {
        /// <summary>
        /// Event fired prior to the search operation being executed.
        /// </summary>
        event SearchDaoEventHandler OnPreSearchEvent;

        /// <summary>
        /// Event fired after the search operation has executed.
        /// </summary>
        event SearchDaoEventHandler OnPostSearchEvent;    
    }
}
