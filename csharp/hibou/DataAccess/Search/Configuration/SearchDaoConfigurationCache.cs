using System;
using System.Diagnostics;
using System.Linq;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.Core.Utilities.Caching;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System.IO;

namespace Keane.CH.Framework.DataAccess.Search.Configuration
{
    /// <summary>
    /// Manages caching of search dao configuration data.
    /// </summary>
    public sealed class SearchDaoConfigurationCache
    {
        #region Constants

        private const string DAO_CONFIG_CACHE_STORE = "DaoConfigCacheStore";

        #endregion Constants

        /// <summary>
        /// Returns a cached configuration instance.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="searchDaoFile">The search dao file.</param>
        /// <returns>Corresponding configuration instance.</returns>
        public static SearchDaoConfiguration Get(
            FileInfo daoFile,
            FileInfo searchDaoFile)
        {
            return Get(daoFile, searchDaoFile, null);
        }

        /// <summary>
        /// Returns a cached configuration instance.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="searchDaoFile">The search dao file.</param>
        /// <param name="defaultSearchDaoFile">The default search dao file.</param>
        /// <returns>Corresponding configuration instance.</returns>
        public static SearchDaoConfiguration Get(
            FileInfo daoFile,
            FileInfo searchDaoFile,
            FileInfo defaultSearchDaoFile)
        {
            // Add if neccessary.
            if (!Exists(searchDaoFile))
            {
                Add(daoFile, searchDaoFile, defaultSearchDaoFile);
            }

            // Return cached instance.
            return
                CacheUtility.GetItem<SearchDaoConfiguration>(DAO_CONFIG_CACHE_STORE, searchDaoFile.FullName);
        }

        /// <summary>
        /// Adds a configuration instance to the cache.
        /// </summary>
        /// <remarks>
        /// This implicitly performs a remove.
        /// </remarks>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="searchDaoFile">The search dao file.</param>
        /// <param name="defaultEntityDaoFile">The default entity dao file.</param>
        public static void Add(
            FileInfo daoFile,
            FileInfo searchDaoFile,
            FileInfo defaultSearchDaoFile)
        {
            // Remove.
            Remove(searchDaoFile);

            // Deserialize.
            SearchDaoConfiguration config =
                DeserializationUtility.DeserializeFromFile<SearchDaoConfiguration>(searchDaoFile);

            // Merge with default (if necessary).
            if (defaultSearchDaoFile != null)
            {
                SearchDaoConfiguration defaultConfig =
                    DeserializationUtility.DeserializeFromFile<SearchDaoConfiguration>(defaultSearchDaoFile);
                if (defaultConfig != null)
                    config.Merge(defaultConfig);
            }

            // Parse.
            DaoConfiguration daoConfig =
                DaoConfigurationCache.Get(daoFile, true);
            if (daoConfig != null)
                daoConfig.Parse(config);

            // Add.
            CacheUtility.AddItem(DAO_CONFIG_CACHE_STORE, searchDaoFile.FullName, config);
        }

        /// <summary>
        /// Adds a configuration instance to the cache.
        /// </summary>
        /// <remarks>
        /// This implicitly performs a remove.
        /// </remarks>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="searchDaoFile">The search dao file.</param>
        public static void Add(
            FileInfo daoFile,
            FileInfo searchDaoFile)
        {
            Add(daoFile, searchDaoFile, null);
        }

        /// <summary>
        /// Removes a configuration instance from the cache.
        /// </summary>
        /// <param name="searchDaoFile">The search dao file.</param>
        public static void Remove(
            FileInfo searchDaoFile)
        {
            CacheUtility.RemoveItem(DAO_CONFIG_CACHE_STORE, searchDaoFile.FullName);
        }

        /// <summary>
        /// Determines whether the config file is already cached.
        /// </summary>
        /// <param name="searchDaoFile">The search dao file.</param>
        public static bool Exists(
            FileInfo searchDaoFile)
        {
            return
                CacheUtility.IsItemCached(DAO_CONFIG_CACHE_STORE, searchDaoFile.FullName);
        }
    }
}