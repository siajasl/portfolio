using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using Keane.CH.Framework.Core.Utilities.Caching;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Core.Utilities.Caching
{
    /// <summary>
    /// An entity cache.
    /// </summary>
    internal sealed class EntityCache
    {
        #region Add

        /// <summary>
        /// Caches an entity within the associated collection.
        /// </summary>
        /// <param name="entity">The entitiy being cached.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <param name="cacheStore">A cache store key within which items are cached.</param>
        internal static void Add(
            EntityBase entity,
            string collectionKey,
            string cacheStore)
        {
            // Defensive programming.
            Debug.Assert(!string.IsNullOrEmpty(cacheStore), "Cache store is unspecified.");
            Debug.Assert(entity != null, "Cannot add null pointers to the entity cache.");

            // Retrieve existing collection.
            List<EntityBase> collection =
                GetCollection(entity.GetType(), collectionKey, cacheStore);

            // For existing collections remove & add (as appropriate).
            if (collection != null)
            {
                EntityBase oldEntity = collection.FirstOrDefault(e => e.Id.Equals(entity.Id));
                if (oldEntity != null)
                    collection.Remove(oldEntity);
                collection.Add(entity);
            }
            // For new collections create & add.
            else
            {
                collection = new List<EntityBase>();
                collection.Add(entity);
                AddCollection(collection, entity.GetType(), collectionKey, cacheStore);
            }
        }

        /// <summary>
        /// Caches an entity collection.
        /// </summary>
        /// <param name="collection">The collection being cached.</param>
        /// <param name="entityType">The clr type of the cached entity collection.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <param name="cacheStore">A cache store key within which items are cached.</param>
        internal static void AddCollection(
            List<EntityBase> collection,
            Type entityType,
            string collectionKey,
            string cacheStore)
        {
            // Defensive programming.
            Debug.Assert(!string.IsNullOrEmpty(cacheStore), "Cache store is unspecified.");
            Debug.Assert(collection != null, "collection");
            Debug.Assert(collection.Count() > 0, "collection is empty");

            // Implicitly remove old collection.
            RemoveCollection(entityType, collectionKey, cacheStore);

            // Derive cache key.
            string cacheKey = entityType.ToString();
            cacheKey += collectionKey;

            // Set the cache item.
            CacheUtility.AddItem(cacheStore, cacheKey, collection);
        }

        #endregion Add

        #region Remove

        /// <summary>
        /// Removes an entity from an associated collection.
        /// </summary>
        /// <param name="entityId">The id of the entity being removed from the cache.</param>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <param name="cacheStore">A cache store key within which items are cached.</param>
        internal static void Remove(
            int entityId,
            Type entityType,
            string collectionKey,
            string cacheStore)
        {
            // Defensive programming.
            Debug.Assert(!string.IsNullOrEmpty(cacheStore), "Cache store is unspecified.");
            Debug.Assert(entityType != null, "entityType");

            // Remove the item if found within the cached collection.
            List<EntityBase> collection = GetCollection(entityType, collectionKey, cacheStore);
            if (collection != null)
            {
                EntityBase entity = collection.FirstOrDefault(e => e.Id.Equals(entityId));
                if (entity != null)
                {
                    collection.Remove(entity);
                }
            }
        }

        /// <summary>
        /// Removes an entity collection.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <param name="cacheStore">A cache store key within which items are cached.</param>
        internal static void RemoveCollection(
            Type entityType,
            string collectionKey,
            string cacheStore)
        {
            // Defensive programming.
            Debug.Assert(!string.IsNullOrEmpty(cacheStore), "Cache store is unspecified.");
            Debug.Assert(entityType != null, "entityType");

            // Derive the unique cache key.
            string cacheKey = entityType.ToString();
            cacheKey += collectionKey;

            // Remove the cache item.
            CacheUtility.RemoveItem(cacheStore, cacheKey);
        }

        #endregion Remove

        #region Read

        /// <summary>
        /// Gets an entity collection from the cache.
        /// </summary>
        /// <param name="entityType">The clr type of the cached entity collection.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <param name="cacheStore">A cache store key within which items are cached.</param>
        /// <returns>An entity collection from the cache.</returns>
        internal static List<EntityBase> GetCollection(
            Type entityType,
            string collectionKey,
            string cacheStore)
        {
            // Defensive programming.
            Debug.Assert(!string.IsNullOrEmpty(cacheStore), "Cache store is unspecified.");
            Debug.Assert(entityType != null, "entityType");

            // Derive the unique cache key.
            string cacheKey = entityType.ToString();
            cacheKey += collectionKey;

            // Return from the cache.
            return
                CacheUtility.GetItem<List<EntityBase>>(cacheStore, cacheKey);
        }

        /// <summary>
        /// Gets an entity from a cached collection.
        /// </summary>
        /// <param name="entityId">The id of the entiy being returned.</param>
        /// <param name="entityType">The clr type of the cached entity collection.</param>
        /// <param name="collectionKey">The collection's cache key.</param>
        /// <param name="cacheStore">A cache store key within which items are cached.</param>
        /// <returns>An entity from a cache collection.</returns>
        internal static EntityBase Get(
            int entityId,
            Type entityType,
            string collectionKey,
            string cacheStore)
        {
            // Defensive programming.
            Debug.Assert(!string.IsNullOrEmpty(cacheStore), "Cache store is unspecified.");
            Debug.Assert(entityType != null, "entityType");

            // Get collection and return first with matching id.
            EntityBase result = null;
            List<EntityBase> collection =
                GetCollection(entityType, collectionKey, cacheStore);
            if (collection != null)
            {
                result = collection.FirstOrDefault(e => e.Id.Equals(entityId));
            }
            return result;
        }

        #endregion Read
    }
}