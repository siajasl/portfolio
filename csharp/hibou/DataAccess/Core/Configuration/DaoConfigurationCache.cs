using System.IO;
using Keane.CH.Framework.Core.Utilities.Caching;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.DataAccess.Search.Configuration;

namespace Keane.CH.Framework.DataAccess.Core.Configuration
{
    /// <summary>
    /// Manages caching of dao configuration file data.
    /// </summary>
    public sealed class DaoConfigurationCache
    {
        #region Constants

        private const string DAO_CONFIG_CACHE_STORE = "DaoConfigCacheStore";

        #endregion Constants

        /// <summary>
        /// Returns a cached configuration instance.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="addIfNotFound">Flag indicating whether to add the configuration data if not already cached.</param>
        /// <returns>Corresponding configuration instance.</returns>
        public static DaoConfiguration Get(
            FileInfo daoFile,
            bool addIfNotFound)
        {
            // Get.
            DaoConfiguration result =
                CacheUtility.GetItem<DaoConfiguration>(DAO_CONFIG_CACHE_STORE, daoFile.FullName);

            // Add if not found & instructed to do so.
            if (result == null && 
                addIfNotFound)
            {
                Add(daoFile);
                result =
                    CacheUtility.GetItem<DaoConfiguration>(DAO_CONFIG_CACHE_STORE, daoFile.FullName);
            }

            // Return.
            return result;
        }

        /// <summary>
        /// Returns a cached configuration instance.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <returns>Corresponding configuration instance.</returns>
        public static DaoConfiguration Get(
            FileInfo daoFile)
        {
            return Get(daoFile, false);
        }

        /// <summary>
        /// Adds a configuration instance to the cache.
        /// </summary>
        /// <remarks>
        /// This implicitly performs a remove.
        /// </remarks>
        /// <param name="daoFile">The dao file.</param>
        public static void Add(
            FileInfo daoFile)
        {
            // Remove.
            Remove(daoFile);

            // Deserialize.
            DaoConfiguration config =
                DeserializationUtility.DeserializeFromFile<DaoConfiguration>(daoFile);

            // Add.
            CacheUtility.AddItem(DAO_CONFIG_CACHE_STORE, daoFile.FullName, config);
        }

        /// <summary>
        /// Removes a configuration instance from the cache.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        public static void Remove(
            FileInfo daoFile)
        {
            CacheUtility.RemoveItem(DAO_CONFIG_CACHE_STORE, daoFile.FullName);
        }

        /// <summary>
        /// Determines whether the config sets is already cached.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        public static bool Exists(
            FileInfo daoFile)
        {
            return
                CacheUtility.IsItemCached(DAO_CONFIG_CACHE_STORE, daoFile.FullName);
        }
    }
}