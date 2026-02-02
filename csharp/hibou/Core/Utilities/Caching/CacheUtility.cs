using System;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Keane.CH.Framework.Core.Utilities.Caching
{
    /// <summary>
    /// Manages access to caches managed by Enterprise Library.
    /// </summary>
    public sealed class CacheUtility
    {
        #region Private methods

        /// <summary>
        /// Retrieves a cache manager from the managed collection.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        private static ICacheManager GetCache(
            string storeKey)
        {
            ICacheManager cache = CacheFactory.GetCacheManager(storeKey);
            if (cache == null)
                throw new ApplicationException("ServiceCache manager must be assigned in EntLib 4.0 configuration.");
            return cache;
        }

        #endregion Private methods

        /// <summary>
        /// Clears the cache.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        public static void Clear(
            string storeKey)
        {
            ICacheManager cache = GetCache(storeKey);
            cache.Flush();
        }

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        /// <param name="itemKey">The cache item key.</param>
        public static void RemoveItem(
            string storeKey, string itemKey)
        {
            ICacheManager cache = GetCache(storeKey);
            cache.Remove(itemKey);
        }

        /// <summary>
        /// Determines whether the cache item is already cached.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>True if the item is already cached.</returns>
        public static bool IsItemCached(
            string storeKey, string itemKey)
        {
            ICacheManager cache = GetCache(storeKey);
            return (cache.Contains(itemKey));
        }

        /// <summary>
        /// Returns the cached item.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        /// <param name="itemKey">The cache item key.</param>
        /// <returns>An item from the cache.</returns>
        public static object GetItem(
            string storeKey, string itemKey)
        {
            ICacheManager cache = GetCache(storeKey);
            return cache.GetData(itemKey);
        }

        /// <summary>
        /// Returns the cached item.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        /// <param name="itemKey">The cache item key.</param>
        /// <typeparam name="T">The type of item to be returned.</typeparam>
        /// <returns>An item from the cache.</returns>
        public static T GetItem<T>(
            string storeKey, string itemKey)
        {
            ICacheManager cache = GetCache(storeKey);
            return (T)cache.GetData(itemKey);
        }

        /// <summary>
        /// Returns the count of items within the cache.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        public static int GetCount(
            string storeKey)
        {
            ICacheManager cache = GetCache(storeKey);
            return cache.Count;
        }

        /// <summary>
        /// Caches an item within the underlying cache.
        /// </summary>
        /// <param name="storeKey">A cache store key within which items are cached.</param>
        /// <param name="itemKey">The cache item key.</param>
        /// <param name="item">The item being cached.</param>
        /// <returns>An item from the cache.</returns>
        /// <remarks>Performs an implicit remove prior to inseretion.</remarks>
        public static void AddItem(
            string storeKey, string itemKey, object item)
        {
            ICacheManager cache = GetCache(storeKey);
            cache.Remove(itemKey);
            cache.Add(itemKey, item);
        }
    }
}