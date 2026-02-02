
namespace Keane.CH.Framework.Apps.UI.Core.Adaptors
{
    /// <summary>
    /// An adaptor over an object data source.
    /// </summary>
    public class ObjectDataSourceAdaptor
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data control.
        /// </summary>
        public object DataSource
        { get; set; }

        /// <summary>
        /// Gets or sets the total count of records within the underlying repository.
        /// </summary>
        public int TotalCount
        { get; set; }

        #endregion Properties

        #region Object data control methods

        /// <summary>
        /// Search helper method that an object data source will invoke.
        /// </summary>
        /// <param name="maximumRows">Pseudo parameter to support paging.</param>
        /// <param name="startRowIndex">Pseudo parameter to support paging.</param>
        /// <param name="sortParameter">Pseudo parameter to support sorting.</param>
        /// <returns>The search results.</returns>
        public object GetDataSource(
            int maximumRows,
            int startRowIndex,
            string sortParameter)
        {
            return DataSource;
        }

        /// <summary>
        /// Total helper method that an object data source will invoke.
        /// </summary>
        /// <param name="maximumRows">Pseudo parameter to support paging.</param>
        /// <param name="startRowIndex">Pseudo parameter to support paging.</param>
        /// <returns>The total count.</returns>
        public int GetTotal(
            int maximumRows,
            int startRowIndex)
        {
            return TotalCount;
        }

        /// <summary>
        /// Insertion simulation method for managing the insertion process.
        /// </summary>
        public void InsertionSimulation()
        {

        }

        #endregion Object data control methods
    }
}
