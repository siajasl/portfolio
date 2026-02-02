using System;
using System.Linq;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Core.ExtensionMethods;
using Keane.CH.Framework.Core.Utilities.DataContract;
using Keane.CH.Framework.DataAccess.ORM;
using Keane.CH.Framework.DataAccess.Core.Factory;
using System.Diagnostics;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Entity specific database access object base class.
    /// </summary>
    /// <remarks>
    /// 1.  This encapsulates typical entity data access operations such as: Get; Delete; Insert; Update.
    /// 2.  This also supports entity list operations.
    /// </remarks>
    /// <typeparam name="E">A sub-class inheriting from EntityBase.</typeparam>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    [Serializable]
    public class EntityDao<E> :
        IEntityDao
        where E : EntityBase, new()
    {
        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntityDao()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Collaborator instantiation event.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            EventPublisher = new EntityDaoEventPublisher();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the associated dao used to execute db commands.
        /// </summary>
        public IDao Dao
        { get; set; }

        /// <summary>
        /// Gets or sets the associated configuration data.
        /// </summary>
        internal EntityDaoConfiguration Config
        { get; set; }

        /// <summary>
        /// Gets or sets the associated event publisher.
        /// </summary>
        private EntityDaoEventPublisher EventPublisher
        { get; set; }

        /// <summary>
        /// Gets the associated mappings.
        /// </summary>
        protected ORMappingList Mappings
        {
            get { return Config.Mappings; }
        }

        #endregion Properties

        #region IEntityDao Members

        #region Event publisher

        /// <summary>
        /// Gets the associated events in order to permit event listen.
        /// </summary>
        public IEntityDaoEvents Events
        { 
            get 
            { 
                return (IEntityDaoEvents)EventPublisher; 
            } 
        }

        #endregion Event publisher

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entityType">The type of the entity being deleted.</param>
        /// <param name="entityId">The id of the entity being deleted.</param>
        public void Delete(
            Type entityType,
            int entityId)
        {
            // Defensive Programming.
            Debug.Assert(entityType != null);

            // Retrieve the entity (this allows delete cascading).
            EntityBase entity = this.Retrieve(entityType, entityId);
            
            // Exception if the entity does not exist.
            if (entity == null)
                throw new ApplicationException(String.Format("Entity does not exist and therefore cannot be deleted:  ID = {0};  Type={1}.", entityId, typeof(E).FullName));

            // Delete the entity.
            Delete(entity);
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">An entity to be deleted.</param>
        public virtual void Delete(
            EntityBase entity)
        {
            // Defensive programming.
            Debug.Assert(entity != null);

            // Fire pre-operation event.
            EventPublisher.PublishOnPreDelete(new EntityDaoEventArgs() { Instance = entity });

            // Execute operation.
            EntityDaoSprocExecutor.Delete(Dao, Config, entity);

            // Fire post-operation event.
            EventPublisher.PublishOnPostDelete(new EntityDaoEventArgs() { Instance = entity });
        }

        /// <summary>
        /// Deletes a collection.
        /// </summary>
        /// <param name="collection">A collection of entities to be deleted.</param>
        public void Delete(
            IEnumerable<EntityBase> collection)
        {
            foreach (EntityBase entity in collection)
            {
                Delete(entity);
            }
        }

        #endregion Delete

        #region Get

        /// <summary>
        /// Returns an entity.
        /// </summary>
        /// <param name="entityType">The type of the entity being retrieved.</param>
        /// <param name="entityId">The id of the entity being retrieved.</param>
        public EntityBase Retrieve(
            Type entityType, 
            int entityId)
        {
            // Defensive Programming.
            Debug.Assert(entityType != null);

            // Fire pre-operation event.
            EventPublisher.PublishOnPreGet(new EntityDaoEventArgs() { InstanceId = entityId });

            // Execute operation.
            EntityBase result =
                EntityDaoSprocExecutor.Get<E>(Dao, Config, entityId);

            // Fire post-operation event.
            if (result != null)
                EventPublisher.PublishOnPostGet(new EntityDaoEventArgs() { Instance = result });

            // Return result.
            return result;
        }

        /// <summary>
        /// Returns a collection of all entities.
        /// </summary>
        /// <param name="entityType">The type of the entity being retrieved.</param>
        /// <remarks>
        /// Used during unit tests & for caching purposes.
        /// </remarks>
        public IEnumerable<EntityBase> RetrieveAll(
            Type entityType)
        {
            // Fire pre-operation event.
            EventPublisher.PublishOnPreGetAll(new EntityDaoEventArgs() { });

            // Execute operation.
            IEnumerable<EntityBase> result =
                EntityDaoSprocExecutor.GetAll<E>(Dao, Config);

            // Fire post-operation event.
            if (result != null)
                EventPublisher.PublishOnPostGetAll(new EntityDaoEventArgs() { Collection = result.Cast<EntityBase>() });

            // Return result.
            return result;
        }

        /// <summary>
        /// Returns a collection of all entities filtered by the passed entity state.
        /// </summary>
        /// <param name="entityType">The type of the entity being retrieved.</param>
        /// <param name="entityState">The state used for performing the filter.</param>
        public IEnumerable<EntityBase> RetrieveAllByState(
            Type entityType, 
            EntityState entityState)
        {
            List<EntityBase> result = new List<EntityBase>();
            IEnumerable<EntityBase> all = RetrieveAll(typeof(E));
            foreach (EntityBase entity in all)
            {
                if (entity.EntityInfo.EntityState.Equals(entityState))
                    result.Add(entity);
            }
            return result.AsEnumerable();
        }

        /// <summary>
        /// Returns the count of entities.
        /// </summary>
        /// <param name="entityType">The type of the entity whose count is being retrieved.</param>
        /// <remarks>
        /// Used during unit tests & search operations.
        /// </remarks>
        public int RetrieveCount(
            Type entityType)
        {
            return EntityDaoSprocExecutor.GetCount(Dao, Config);
        }

        /// <summary>
        /// Returns the entity version of an entity.
        /// </summary>
        /// <param name="entityId">The id of the entity.</param>
        /// <param name="entityType">The type of the entity whose version is being retrieved.</param>
        /// <remarks>
        /// Used during concurrency validation & unit tests.
        /// </remarks>
        public int RetrieveVersion(
            Type entityType,
            int entityId)
        {
            return EntityDaoSprocExecutor.GetVersion(Dao, Config, entityId);
        }

        /// <summary>
        /// Returns the entity state of an entity.
        /// </summary>
        /// <param name="entityType">The type of the entity whose state is being retrieved.</param>
        /// <param name="entityId">The id of the entity.</param>
        /// <remarks>
        /// Used during unit tests.
        /// </remarks>
        public EntityState RetrieveState(
            Type entityType,
            int entityId)
        {
            return (EntityState)EntityDaoSprocExecutor.GetState(Dao, Config, entityId);
        }

        #endregion Get

        #region Save

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="entity">An entity being saved.</param>
        public virtual void Save(EntityBase entity)
        {
            // Abort execution if conditions are not right.
            if (entity == null)
                throw new ArgumentNullException("Cannot save a null pointer.");

            // Either insert or update as appropriate.
            if (entity.EntityInfo.IsNew)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
        }

        /// <summary>
        /// Save a collection.
        /// </summary>
        /// <param name="collection">A collection of entities being saved.</param>
        public void Save(IEnumerable<EntityBase> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("Cannot save a null pointer.");
            foreach (EntityBase entity in collection)
            {
                Save(entity);
            }
        }

        #endregion Save

        #region Insert

        /// <summary>
        /// Inserts a new entity.
        /// </summary>
        /// <param name="entity">The entity being inserted.</param>
        private void Insert(
            EntityBase entity)
        {
            // Fire pre-operation event.
            EventPublisher.PublishOnPreInsert(new EntityDaoEventArgs() { Instance = entity });

            // Execute operation.
            EntityDaoSprocExecutor.Insert(Dao, Config, entity);

            // Fire post-operation event.
            EventPublisher.PublishOnPostInsert(new EntityDaoEventArgs() { Instance = entity });
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity being updated.</param>
        private void Update(
            EntityBase entity)
        {
            // Fire pre-operation event.
            EventPublisher.PublishOnPreUpdate(new EntityDaoEventArgs() { Instance = entity });

            // Execute operation.
            EntityDaoSprocExecutor.Update(Dao, Config, entity);

            // Fire post-operation event.
            EventPublisher.PublishOnPostUpdate(new EntityDaoEventArgs() { Instance = entity });
        }

        #endregion Update

        #region Update state

        /// <summary>
        /// Updates the entity state.
        /// </summary>
        /// <param name="entityType">The type of the entity whose state is being updated.</param>
        /// <param name="entityId">The id of the entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        public void UpdateState(
            Type entityType,
            int entityId,
            EntityState entityState) 
        {
            // Defensive programming.
            Debug.Assert(entityType != null);

            // Fire pre-operation event.
            EventPublisher.PublishOnPreUpdateState(new EntityDaoEventArgs() { InstanceId = entityId });

            // Update the state of the entity.
            EntityDaoSprocExecutor.UpdateState(
                Dao, Config, entityId, entityState);

            // Fire post-operation event.
            EventPublisher.PublishOnPostUpdateState(new EntityDaoEventArgs() { InstanceId = entityId });
        }

        /// <summary>
        /// Updates the entity state.
        /// </summary>
        /// <param name="entityTypeId">The type id of the entity.</param>
        /// <param name="entityId">The id of the entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        public void UpdateState(
            int entityTypeId,
            int entityId,
            EntityState entityState)
        {
            // Defensive programming.
            Debug.Assert(entityTypeId > 0);

            // Update the state of the entity.
            EntityDaoSprocExecutor.UpdateState(
                Dao, entityTypeId, entityId, entityState);
        }

        #endregion Update state

        #endregion IEntityDao Members
    }
}