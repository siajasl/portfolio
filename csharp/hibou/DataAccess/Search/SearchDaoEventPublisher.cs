using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Publishes search dao events to registered subscribers.
    /// </summary>
    /// <typeparam name="I">A sub-class representing the search result.</typeparam>
    internal class SearchDaoEventPublisher : 
        ISearchDaoEvents
    {
        #region ISearchDaoEvents Members

        /// <summary>
        /// Event fired prior to the search operation being executed.
        /// </summary>
        public event SearchDaoEventHandler OnPreSearchEvent;

        /// <summary>
        /// Event fired after the search operation has executed.
        /// </summary>
        public event SearchDaoEventHandler OnPostSearchEvent;

        #endregion

        #region Event publishing

        /// <summary>
        /// Virtual event handler for the OnPreSearch event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreSearchEvent(SearchDaoEventArgs args)
        {
            SearchDaoEventHandler handler = this.OnPreSearchEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Virtual event handler for the OnPostSearch event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostSearchEvent(SearchDaoEventArgs args)
        {
            SearchDaoEventHandler handler = this.OnPostSearchEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Event publishing
    }
}