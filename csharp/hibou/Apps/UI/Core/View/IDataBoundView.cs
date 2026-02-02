
namespace Keane.CH.Framework.Apps.UI.Core.View
{
    /// <summary>
    /// Represents a technology neutral abstraction of a data bound view.
    /// </summary>
    public interface IDataBoundView
    {
        /// <summary>
        /// Gets or set the view datasource.
        /// </summary>
        object DataSource
        { set; }

        /// <summary>
        /// Performs the relevant data binding.
        /// </summary>
        void DataBind();
    }
}
