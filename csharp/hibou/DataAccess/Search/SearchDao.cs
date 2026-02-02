using System;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Search.Configuration;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.DataAccess.Core.Factory;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Encapsulates a search operation.
    /// </summary>
    /// <typeparam name="C">A sub-class representing the search criteria.</typeparam>
    /// <typeparam name="I">A sub-class representing the search result.</typeparam>
    [Serializable]
    public class SearchDao<I> : 
        ISearchDao
        where I : new()
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SearchDao()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Collaborator instantiation event.
        /// </summary>
        private void InitialiseMembers()
        {
            EventPublisher = new SearchDaoEventPublisher();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the associated dao used to execute db commands.
        /// </summary>
        internal IDao Dao
        { get; set; }

        /// <summary>
        /// Gets or sets the associated configuration information.
        /// </summary>
        internal SearchDaoConfiguration Config
        { get; set; }

        /// <summary>
        /// Gets or sets the associated event publisher used to publish events.
        /// </summary>
        private SearchDaoEventPublisher EventPublisher
        { get; set; }

        #endregion Properties

        #region ISearchDao Members

        /// <summary>
        /// Gets the associated events in order to permit event listen.
        /// </summary>
        public ISearchDaoEvents Events
        {
            get
            {
                return (ISearchDaoEvents)EventPublisher;
            }
        }

        /// <summary>
        /// Executes the search.
        /// </summary>
        /// <param name="criteria">The search criteria.</param>
        /// <returns>The search results.</returns>
        public SearchResult Search(SearchCriteriaBase criteria)
        {
            // Fire pre-operation event.
            EventPublisher.PublishOnPreSearchEvent(SearchDaoEventArgs.Create(criteria));

            // Execute operation.
            SearchResult result = 
                SearchDaoSprocExecutor.Search<I>(Dao, Config, criteria);

            // Fire post-operation event.
            EventPublisher.PublishOnPostSearchEvent(SearchDaoEventArgs.Create(criteria, result));

            // Return result.
            return result;
        }

        #endregion ISearchDao Members
    }
}