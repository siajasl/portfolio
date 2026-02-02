namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Entity dao event handler.
    /// </summary>
    /// <typeparam name="E">The type of entity being processed.</typeparam>
    /// <param name="control">The event control.</param>
    /// <param name="args">The svent arguments.</param>
    public delegate void EntityDaoEventHandler(object source, EntityDaoEventArgs args);
}
