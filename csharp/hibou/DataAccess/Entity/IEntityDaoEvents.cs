namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Encapuslates the various entity dao events.
    /// </summary>
    public interface IEntityDaoEvents
    {
        /// <summary>
        /// Event fired prior to the Get operation being executed.
        /// </summary>
        event EntityDaoEventHandler OnPreGetEvent;

        /// <summary>
        /// Event fired after the Get operation has executed.
        /// </summary>
        event EntityDaoEventHandler OnPostGetEvent;

        /// <summary>
        /// PreGetAll event delegate.
        /// </summary>
        event EntityDaoEventHandler OnPreGetAllEvent;

        /// <summary>
        /// PostGetAll event delegate.
        /// </summary>
        event EntityDaoEventHandler OnPostGetAllEvent;

        /// <summary>
        /// Event fired prior to the Delete operation being executed.
        /// </summary>
        event EntityDaoEventHandler OnPreDeleteEvent;

        /// <summary>
        /// Event fired after the Delete operation has executed.
        /// </summary>
        event EntityDaoEventHandler OnPostDeleteEvent;

        /// <summary>
        /// Event fired prior to the Insert operation being executed.
        /// </summary>
        event EntityDaoEventHandler OnPreInsertEvent;

        /// <summary>
        /// Event fired after the Insert operation has executed.
        /// </summary>
        event EntityDaoEventHandler OnPostInsertEvent;

        /// <summary>
        /// Event fired prior to the Update operation being executed.
        /// </summary>
        event EntityDaoEventHandler OnPreUpdateEvent;

        /// <summary>
        /// Event fired after the Update operation has executed.
        /// </summary>
        event EntityDaoEventHandler OnPostUpdateEvent;

        /// <summary>
        /// Event fired prior to the Update operation being executed.
        /// </summary>
        event EntityDaoEventHandler OnPreUpdateStateEvent;

        /// <summary>
        /// Event fired after the Update operation has executed.
        /// </summary>
        event EntityDaoEventHandler OnPostUpdateStateEvent;


    }
}
