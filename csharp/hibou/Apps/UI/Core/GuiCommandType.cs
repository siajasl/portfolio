
namespace Keane.CH.Framework.Apps.UI.Core
{
    /// <summary>
    /// Enumeration over common gui command types.
    /// </summary>
    public enum GuiCommandType
    {
        /// <summary>
        /// Unknown - the default value.
        /// </summary>
        Unknown = 1,

        /// <summary>
        /// Save changes event.
        /// </summary>
        Save = 100,

        /// <summary>
        /// Save changes pending confirmation by an administrator.
        /// </summary>
        SaveProtected,

        /// <summary>
        /// Save changes & close window event.
        /// </summary>
        SaveClose,

        /// <summary>
        /// Save changes pending & close window confirmation by an administrator.
        /// </summary>
        SaveCloseProtected,

        /// <summary>
        /// Cancel current process.
        /// </summary>
        Cancel,

        /// <summary>
        /// Cancel current process with immediate effect.
        /// </summary>
        CancelImmediate,

        /// <summary>
        /// Delete current work item.
        /// </summary>
        Delete,

        /// <summary>
        /// Delete current work item pending confirmation by an administrator.
        /// </summary>
        DeleteProtected,

        /// <summary>
        /// Executes a search.
        /// </summary>
        Search = 200,

        /// <summary>
        /// Executes a search reset.
        /// </summary>
        SearchReset,

        /// <summary>
        /// Search result page or sort.
        /// </summary>
        SearchResultsPageOrSort,

        /// <summary>
        /// Open a document in print preview mode.
        /// </summary>
        DocumentPrintPreview = 300,

        /// <summary>
        /// Open a document in print mode.
        /// </summary>
        DocumentPrint,

        /// <summary>
        /// Sort a list.
        /// </summary>
        SortListView,

        /// <summary>
        /// On multi-tab gui's this comand is executed when the current tab is changed.
        /// </summary>
        TabSwitch,

        /// <summary>
        /// Send an email command.
        /// </summary>
        SendEmail,

        /// <summary>
        /// Accept decision.
        /// </summary>
        AdjudicateAccept = 400,

        /// <summary>
        /// Reject decision.
        /// </summary>
        AdjudicateReject,
    }
}