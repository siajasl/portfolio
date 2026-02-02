
namespace Keane.CH.Framework.Apps.UI.Core.View.Search
{
    /// <summary>
    /// Represents a standard search view.
    /// </summary>
    /// <typeparam name="SC">The associated search criteria view.</typeparam>
    public interface ISearchView<SC>
    {
        /// <summary>
        /// Gets the associated search criteria view.
        /// </summary>
        SC Criteria { get; }

        /// <summary>
        /// Gets the associated data bound search criteria results view.
        /// </summary>
        IDataBoundView Results { get; }
    }
}