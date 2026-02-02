
namespace Keane.CH.Framework.Apps.UI.Core.View.Search
{
    /// <summary>
    /// Exposes the core aspects of a grid view.
    /// </summary>
    public interface IGridView
    {
        /// <summary>
        /// Gets the sort expression.
        /// </summary>
        string SortExpression
        { get; }

        /// <summary>
        /// Gets a flag indicating the sort direction.
        /// </summary>
        bool SortIsDescending
        { get; }

        /// <summary>
        /// Gets the page size.
        /// </summary>
        int PageSize
        { get; }

        /// <summary>
        /// Gets or sets the current page index.
        /// </summary>
        int PageIndex
        { get; set; }

        /// <summary>
        /// Sets the total number in the underlying repository.
        /// </summary>
        int Total
        { set; }

        /// <summary>
        /// Sets the datasource.
        /// </summary>
        object DataSource
        { set; }

        /// <summary>
        /// Performs the relevant data binding.
        /// </summary>
        void DataBind();
    }
}
