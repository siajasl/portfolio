using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Core.Utilities.Caching
{
    /// <summary>
    /// Encapsulates entity cache access operations.
    /// </summary>
    public interface IEntityCacheAccessor
    {
        #region Generic methods

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <returns>An entity from a cache collection.</returns>
        T Get<T>(
            int entityId)
            where T : EntityBase, new();

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <returns>An entity from a cache collection.</returns>
        T Get<T>(
            int entityId,
            string collectionKey)
            where T : EntityBase, new();

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <returns>An entity collection from the cache.</returns>
        IEnumerable<T> GetCollection<T>(
            string collectionKey)
            where T : EntityBase, new();

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <returns>An entity collection from the cache.</returns>
        IEnumerable<T> GetCollection<T>()
            where T : EntityBase, new();

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The of the entity being removed.</param>
        void Remove<T>(
            int entityId)
            where T : EntityBase, new();

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The of the entity being removed.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void Remove<T>(
            int entityId,
            string collectionKey)
            where T : EntityBase, new();

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        void RemoveCollection<T>()
            where T : EntityBase, new();

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="collectionKey">The collection's cache key.</param>
        void RemoveCollection<T>(
            string collectionKey)
            where T : EntityBase, new();

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        void UpdateState<T>(
            int entityId,
            EntityState entityState)
            where T : EntityBase, new();

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void UpdateState<T>(
            int entityId,
            EntityState entityState,
            string collectionKey)
            where T : EntityBase, new();

        #endregion Generic methods

        #region Non-generic methods

        /// <summary>
        /// Caches an entity within the default collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        void Add(
            EntityBase entity);

        /// <summary>
        /// Caches an entity within an associated collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void Add(
            EntityBase entity,
            string collectionKey);

        /// <summary>
        /// Caches an entity collection.
        /// </summary>
        /// <param name="collection">The collection being cached.</param>
        void AddCollection(
            IEnumerable<EntityBase> collection);

        /// <summary>
        /// Caches an entity collection.
        /// </summary>
        /// <param name="collection">The collection being cached.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void AddCollection(
            IEnumerable<EntityBase> collection,
            string collectionKey);

        /// <summary>
        /// Updates an entity within an associated collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        void Update(
            EntityBase entity);

        /// <summary>
        /// Updates an entity within an associated collection.
        /// </summary>
        /// <param name="entity">The entity being cached.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void Update(
            EntityBase entity,
            string collectionKey);

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        void UpdateState(
            Type entityType,
            int entityId,
            EntityState entityState);

        /// <summary>
        /// Updates the state of an entity within an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the cached entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void UpdateState(
            Type entityType,
            int entityId,
            EntityState entityState,
            string collectionKey);

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The of the entity being removed.</param>
        void Remove(
            Type entityType,
            int entityId);

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The of the entity being removed.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void Remove(
            Type entityType,
            int entityId,
            string collectionKey);

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        void RemoveCollection(
            Type entityType);

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        void RemoveCollection(
            Type entityType,
            string collectionKey);

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <returns>An entity collection from the cache.</returns>
        IEnumerable<EntityBase> GetCollection(
            Type entityType,
            string collectionKey);

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <returns>An entity collection from the cache.</returns>
        IEnumerable<EntityBase> GetCollection(
            Type entityType);

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <returns>An entity from a cache collection.</returns>
        EntityBase Get(
            Type entityType,
            int entityId);

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <returns>An entity from a cache collection.</returns>
        EntityBase Get(
            Type entityType,
            int entityId,
            string collectionKey);

        #endregion Non-generic methods
    }
}
