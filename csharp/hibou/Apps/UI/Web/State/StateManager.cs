
using Keane.CH.Framework.Apps.UI.Web;
using System;
namespace Keane.CH.Framework.Apps.UI.Web.State
{
    /// <summary>
    /// Encapsulates state management funtionality.
    /// </summary>
    public class StateManager
    {
        #region Ctor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="page">The page currently being processed.</param>
        public StateManager(WebPageBase page) 
        {
            Application = new ApplicationStateManager(page);
            Session = new SessionStateManager(page);
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets the session state manager.
        /// </summary>
        public SessionStateManager Session
        { get; private set; }

        /// <summary>
        /// Gets the application state manager.
        /// </summary>
        public ApplicationStateManager Application
        { get; private set; }

        #endregion Properties
    }
}
