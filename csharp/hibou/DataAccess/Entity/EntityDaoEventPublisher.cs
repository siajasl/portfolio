namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Publishes entity dao events to registered subscribers.
    /// </summary>
    internal class EntityDaoEventPublisher : 
        IEntityDaoEvents
    {
        #region Events

        #region Get event

        /// <summary>
        /// Event fired prior to the Get operation being executed.
        /// </summary>
        public event EntityDaoEventHandler OnPreGetEvent;

        /// <summary>
        /// Virtual event handler for the OnPreGet  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreGet(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreGetEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Event fired after the Get operation has executed.
        /// </summary>
        public event EntityDaoEventHandler OnPostGetEvent;

        /// <summary>
        /// Virtual event handler for the OnGet  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostGet(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostGetEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Get event

        #region Get collection event

        /// <summary>
        /// PreGetAll event delegate.
        /// </summary>
        public event EntityDaoEventHandler OnPreGetAllEvent;

        /// <summary>
        /// OnPreGetAll event publisher.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreGetAll(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreGetAllEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// PostGetCollection event delegate.
        /// </summary>
        public event EntityDaoEventHandler OnPostGetAllEvent;

        /// <summary>
        /// OnPostGetAll event publisher.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostGetAll(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostGetAllEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Get collection event

        #region Delete event

        /// <summary>
        /// Event fired prior to the Delete operation being executed.
        /// </summary>
        public event EntityDaoEventHandler OnPreDeleteEvent;

        /// <summary>
        /// Virtual event handler for the OnPreDelete  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreDelete(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreDeleteEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Event fired after the Delete operation has executed.
        /// </summary>
        public event EntityDaoEventHandler OnPostDeleteEvent;

        /// <summary>
        /// Virtual event handler for the OnDelete  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostDelete(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostDeleteEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Delete event

        #region Delete protected event

        /// <summary>
        /// Event fired prior to the DeleteProtected operation being executed.
        /// </summary>
        public event EntityDaoEventHandler OnPreDeleteProtectedEvent;

        /// <summary>
        /// Virtual event handler for the OnPreDeleteProtected  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreDeleteProtected(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreDeleteProtectedEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Event fired after the DeleteProtected operation has executed.
        /// </summary>
        public event EntityDaoEventHandler OnPostDeleteProtectedEvent;

        /// <summary>
        /// Virtual event handler for the OnDeleteProtected  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostDeleteProtected(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostDeleteProtectedEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Delete protected event

        #region Insert event

        /// <summary>
        /// Event fired prior to the Insert operation being executed.
        /// </summary>
        public event EntityDaoEventHandler OnPreInsertEvent;

        /// <summary>
        /// Virtual event handler for the OnPreInsert  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreInsert(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreInsertEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Event fired after the Insert operation has executed.
        /// </summary>
        public event EntityDaoEventHandler OnPostInsertEvent;

        /// <summary>
        /// Virtual event handler for the OnInsert  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostInsert(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostInsertEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Insert event

        #region Update event

        /// <summary>
        /// Event fired prior to the Update operation being executed.
        /// </summary>
        public event EntityDaoEventHandler OnPreUpdateEvent;

        /// <summary>
        /// Virtual event handler for the OnPreUpdate  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreUpdate(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreUpdateEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Event fired after the Update operation has executed.
        /// </summary>
        public event EntityDaoEventHandler OnPostUpdateEvent;

        /// <summary>
        /// Virtual event handler for the OnUpdate  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostUpdate(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostUpdateEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Update event

        #region Update protected event

        /// <summary>
        /// Event fired prior to the UpdateProtected operation being executed.
        /// </summary>
        public event EntityDaoEventHandler OnPreUpdateProtectedEvent;

        /// <summary>
        /// Virtual event handler for the OnPreUpdateProtected  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreUpdateProtected(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreUpdateProtectedEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Event fired after the UpdateProtected operation has executed.
        /// </summary>
        public event EntityDaoEventHandler OnPostUpdateProtectedEvent;

        /// <summary>
        /// Virtual event handler for the OnUpdateProtected  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostUpdateProtected(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostUpdateProtectedEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Update protected event

        #region Update state event

        /// <summary>
        /// Event fired prior to the Update operation being executed.
        /// </summary>
        public event EntityDaoEventHandler OnPreUpdateStateEvent;

        /// <summary>
        /// Virtual event handler for the OnPreUpdate  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPreUpdateState(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPreUpdateStateEvent;
            if (handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Event fired after the Update operation has executed.
        /// </summary>
        public event EntityDaoEventHandler OnPostUpdateStateEvent;

        /// <summary>
        /// Virtual event handler for the OnUpdate  event.
        /// </summary>
        /// <param name="args">The event arguments.</param>
        internal void PublishOnPostUpdateState(EntityDaoEventArgs args)
        {
            EntityDaoEventHandler handler = this.OnPostUpdateStateEvent;
            if (handler != null)
                handler(this, args);
        }

        #endregion Update state event

        #endregion Events
    }
}