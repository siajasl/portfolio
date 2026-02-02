using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Search dao event handler delegate.
    /// </summary>
    /// <param name="control">The event control.</param>
    /// <param name="args">The event arguments.</param>
    public delegate void SearchDaoEventHandler(object source, SearchDaoEventArgs args);
}