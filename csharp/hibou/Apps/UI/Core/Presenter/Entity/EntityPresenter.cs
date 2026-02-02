using System;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;
using Keane.CH.Framework.Core.Utilities.Caching;
using Keane.CH.Framework.Core.Utilities.DataContract;
using Keane.CH.Framework.Services.Entity.Contracts;
using Keane.CH.Framework.Services.Entity.Contracts.Data;
using Keane.CH.Framework.Services.Entity.Contracts.Message;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Entity
{
    /// <summary>
    /// An entity dedicated presenter.
    /// </summary>
    /// <typeparam name="E">A domain entity implemented as a sub-class of EntityBase.</typeparam>
    /// <typeparam name="EV">An entity view being processed.</typeparam>
    public class EntityPresenter<E, EV> :
        PresenterBase,
        IEntityPresenter<EV>
        where E : EntityBase, new()
        where EV : class, IEntityView
    {
        #region Constructor

        public EntityPresenter()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            this.EntityMapper = new EntityPresenterMapper<E, EV>();
        }

        #endregion Constructor

        #region Events

        /// <summary>
        /// The event handler invoked when the entity instance being processed has been assigned.
        /// </summary>
        protected event EventHandler<EntityPresenterEventArgs> OnSetInstance;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the entity being processed.
        /// </summary>
        protected E Entity
        { get; set; }

        /// <summary>
        /// Gets or sets the associated entity service.
        /// </summary>
        public IEntityService EntityService
        { get; set; }

        /// <summary>
        /// Gets or sets the associated entity cache accessor.
        /// </summary>
        public IEntityCacheAccessor EntityCache
        { get; set; }

        /// <summary>
        /// Gets or sets the associated entity mapper.
        /// </summary>
        public IEntityPresenterMapper<E, EV> EntityMapper
        { get; set; }

        #endregion Properties

        #region IEntityPresenter<EV> Members

        /// <summary>
        /// Loads an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public virtual void Load(
            EV view, 
            GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);
            if (view.EditMode != EditModeType.Insert &
                view.EntityId <= 0)
            {
                throw new ApplicationException("Cannot load an entity with an id of 0");            
            }

            // Derive model instance.
            this.SetInstance(view, viewContext);

            // Deserialize view state.
            this.EntityMapper.DeserializeView(view, this.Entity, viewContext);
        }

        /// <summary>
        /// Loads an entity directly from previously deserialized xml.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public virtual void LoadProtected(
            EV view, 
            GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);

            // Derive model instance.
            E model = 
                DeserializationUtility.DeserializeFromString<E>(view.EntityXml);

            // Deserialize view state.
            this.EntityMapper.DeserializeView(view, model, viewContext);
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public virtual void Delete(
            EV view, 
            GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);

            // Invoke service operation.
            DeleteRequest request = new DeleteRequest()
            {
                Context = base.GetRequestContext(viewContext),
                EntityId = view.EntityId,
                EntityType = typeof(E)
            };
            this.EntityService.Delete(request);

            // Synchronize cache (if necessary).
            if (this.EntityCache != null)
                this.EntityCache.Remove(typeof(E), view.EntityId);
        }

        /// <summary>
        /// Deletes a protected entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public virtual void DeleteProtected(
            EV view, 
            GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);

            // Invoke service operation.
            DeleteProtectedRequest request = new DeleteProtectedRequest()
            {
                Context = base.GetRequestContext(viewContext),
                EntityId = view.EntityId,
                EntityType = typeof(E)
            };
            this.EntityService.DeleteProtected(request);

            // Synchronize cache (if necessary).
            if (this.EntityCache != null)
            {
                this.EntityCache.UpdateState(
                    typeof(E),
                    request.EntityId,
                    EntityState.InActivePending);
            }
        }

        /// <summary>
        /// Inserts an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public virtual void Insert(
            EV view, 
            GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);

            // Derive model instance.
            this.SetInstance(view, viewContext);

            // Deserialize model state.
            this.EntityMapper.DeserializeModel(this.Entity, view, viewContext);

            // Invoke service operation.
            InsertRequest request = new InsertRequest()
            {
                Context = base.GetRequestContext(viewContext),
                Entity = this.Entity
            };
            InsertResponse response = this.EntityService.Insert(request);

            // Synchronize cache (if necessary).
            if (this.EntityCache != null)
                this.EntityCache.Add(this.Entity);

            // Update view.
            view.EntityId = response.EntityId;
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public virtual void Update(
            EV view, 
            GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);

            // Derive model instance.
            this.SetInstance(view, viewContext);

            // Deserialize model state.
            this.EntityMapper.DeserializeModel(this.Entity, view, viewContext);

            // Invoke service operation.
            UpdateRequest request = new UpdateRequest()
            {
                Context = base.GetRequestContext(viewContext),
                Entity = this.Entity
            };
            this.EntityService.Update(request);

            // Synchronize cache (if necessary).
            if (this.EntityCache != null)
                this.EntityCache.Update(this.Entity);
        }

        /// <summary>
        /// Updates a protected entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public virtual void UpdateProtected(
            EV view, 
            GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);

            // Derive model instance.
            this.SetInstance(view, viewContext);

            // Deserialize model state.
            this.EntityMapper.DeserializeModel(this.Entity, view, viewContext);

            // Invoke service operation.
            UpdateProtectedRequest request = new UpdateProtectedRequest()
            {
                Context = base.GetRequestContext(viewContext),
                Entity = this.Entity
            };
            this.EntityService.UpdateProtected(request);

            // Synchronize cache (if necessary).
            if (this.EntityCache != null)
            {
                this.EntityCache.UpdateState(
                    typeof(E),
                    this.Entity.Id,
                    EntityState.ActivePending);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets the instance that is being processed.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        protected virtual void SetInstance(
            EV view,
            GuiContext viewContext)
        {
            // Instantiate a new instance. 
            this.Entity = new E();

            // Read from repository (if necessary).
            if (view.EditMode != EditModeType.Insert)
            {
                // Invoke service operation.
                RetrieveRequest request = new RetrieveRequest() 
                { 
                    Context = base.GetRequestContext(viewContext),
                    EntityId = view.EntityId,
                    EntityType = typeof(E)                    
                };
                RetrieveResponse response = this.EntityService.Retrieve(request);

                // Cache returned instance.
                this.Entity = (E)response.Entity;
            }

            // Fire event.
            if (this.OnSetInstance != null)
            {
                EntityPresenterEventArgs args = new EntityPresenterEventArgs(this.Entity);
                this.OnSetInstance(this, args);
            }
        }

        #endregion Private methods
    }
}
