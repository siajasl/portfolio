using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Core.Utilities.Caching
{
    /// <summary>
    /// Encapsulates entity cache access operations.
    /// </summary>
    public class EntityCacheAccessor :
        IEntityCacheAccessor
    {
        #region Constants

        private const string DEFAULT_COLLECTION_KEY = "";
        private const string ENTITY_CACHE_STORE = "EntityCacheStore";

        #endregion Constants

        #region IEntityCacheAccessor1 Members

        #region Generic methods

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <returns>An entity from a cache collection.</returns>
        public T Get<T>(
            int entityId)
            where T : EntityBase, new()
        {
            return this.Get<T>(entityId, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <returns>An entity from a cache collection.</returns>
        public T Get<T>(
            int entityId,
            string collectionKey)
            where T : EntityBase, new()
        {
            return (this.Get(typeof(T), entityId) as T);
        }

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <returns>An entity collection from the cache.</returns>
        public IEnumerable<T> GetCollection<T>()
            where T : EntityBase, new()
        {
            return this.GetCollection<T>(DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <returns>An entity collection from the cache.</returns>
        public IEnumerable<T> GetCollection<T>(
            string collectionKey)
            where T : EntityBase, new()
        {
            IEnumerable<EntityBase> collection =
                this.GetCollection(typeof(T), collectionKey);
            if (collection != null)
                return collection.Cast<T>();
            else
                return null;
        }

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The of the entity being removed.</param>
        public void Remove<T>(
            int entityId)
            where T : EntityBase, new()
        {
            this.Remove<T>(entityId, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The of the entity being removed.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void Remove<T>(
            int entityId,
            string collectionKey)
            where T : EntityBase, new()
        {
            this.Remove(typeof(T), entityId, collectionKey);
        }

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        public void RemoveCollection<T>()
            where T : EntityBase, new()
        {
            this.RemoveCollection<T>(DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void RemoveCollection<T>(
            string collectionKey)
            where T : EntityBase, new()
        {
            this.RemoveCollection(typeof(T), collectionKey);
        }

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        public void UpdateState<T>(
            int entityId,
            EntityState entityState)
            where T : EntityBase, new()
        {
            this.UpdateState<T>(entityId, entityState, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void UpdateState<T>(
            int entityId,
            EntityState entityState,
            string collectionKey)
            where T : EntityBase, new()
        {
            this.UpdateState(typeof(T), entityId, entityState, collectionKey);
        }

        #endregion Generic methods

        #region Non-generic methods

        /// <summary>
        /// Caches an entity within the default collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        public void Add(EntityBase entity)
        {
            this.Add(entity, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Caches an entity within an associated collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void Add(EntityBase entity, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(entity != null, "entity");

            // Add entity to cache.
            EntityCache.Add(entity, collectionKey, ENTITY_CACHE_STORE);
        }

        /// <summary>
        /// Caches an entity collection.
        /// </summary>
        /// <typeparam name="T">A type inheriting from EntityBase.</typeparam>
        /// <param name="collection">The collection being cached.</param>
        public void AddCollection(IEnumerable<EntityBase> collection)
        {
            this.AddCollection(collection, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Caches an entity collection.
        /// </summary>
        /// <param name="collection">The collection being cached.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void AddCollection(IEnumerable<EntityBase> collection, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(collection != null, "collection");
            Debug.Assert(collection.Count() > 0, "collection is empty");

            // Derive entity type from first member of collection.
            Type entityType = collection.FirstOrDefault().GetType();

            // Add the collection to cache.            
            EntityCache.AddCollection(collection.ToList(), entityType, collectionKey, ENTITY_CACHE_STORE);
        }

        /// <summary>
        /// Updates an entity within an associated collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        public void Update(EntityBase entity)
        {
            this.Update(entity, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Updates an entity within an associated collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void Update(EntityBase entity, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(entity != null, "entity");

            // The add operation performs an implicit remove before readding.
            EntityCache.Add(entity, collectionKey, ENTITY_CACHE_STORE);
        }

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        public void UpdateState(Type entityType, int entityId, EntityState entityState)
        {
            this.UpdateState(entityType, entityId, entityState, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void UpdateState(Type entityType, int entityId, EntityState entityState, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(entityType != null, "entityType");

            // Get from collection & update the state (if found).
            EntityBase entity = this.Get(entityType, entityId, collectionKey);
            if (entity != null)
                entity.EntityInfo.EntityState = entityState;
        }

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The of the entity being removed.</param>
        public void Remove(Type entityType, int entityId)
        {
            this.Remove(entityType, entityId, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The of the entity being removed.</param>
        public void Remove(Type entityType, int entityId, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(entityType != null, "entityType");

            // Remove from cache.
            EntityCache.Remove(entityId, entityType, collectionKey, ENTITY_CACHE_STORE);
        }

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        public void RemoveCollection(Type entityType)
        {
            this.RemoveCollection(entityType, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        public void RemoveCollection(Type entityType, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(entityType != null, "entityType");

            // Remove from cache.
            EntityCache.RemoveCollection(entityType, collectionKey, ENTITY_CACHE_STORE);
        }

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <returns>An entity collection from the cache.</returns>
        public IEnumerable<EntityBase> GetCollection(Type entityType, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(entityType != null, "entityType");

            // Get the collection from the cache.
            List<EntityBase> collection = 
                EntityCache.GetCollection(entityType, collectionKey, ENTITY_CACHE_STORE);
            return collection.AsEnumerable();
        }

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <returns>An entity collection from the cache.</returns>
        public IEnumerable<EntityBase> GetCollection(Type entityType)
        {
            return this.GetCollection(entityType, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <returns>An entity from a cache collection.</returns>
        public EntityBase Get(Type entityType, int entityId)
        {
            return this.Get(entityType, entityId, DEFAULT_COLLECTION_KEY);
        }

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <returns>An entity from a cache collection.</returns>
        public EntityBase Get(Type entityType, int entityId, string collectionKey)
        {
            // Defensive programming.
            Debug.Assert(entityType != null, "entityType");

            // Attempt to get the entity from the cached collection.
            EntityBase result = null;
            IEnumerable<EntityBase> collection = this.GetCollection(entityType, collectionKey);
            if (collection != null)
                result = collection.FirstOrDefault(e => e.Id.Equals(entityId));
            return result;
        }

        #endregion Non-generic methods

        #endregion IEntityCacheAccessor1 Members
    }
}
