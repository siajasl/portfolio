
namespace Keane.CH.Framework.Apps.UI.WPF.State
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
        /// <param name="window">The window currently being processed.</param>
        public StateManager(WPFWindowBase window)
        {
            Application = new ApplicationStateManager(window);
            Session = new SessionStateManager(window);
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
