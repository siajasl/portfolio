using System;
using System.Diagnostics;
using System.Linq;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.Core.Utilities.Caching;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System.IO;

namespace Keane.CH.Framework.DataAccess.Entity.Configuration
{
    /// <summary>
    /// Manages caching of entity dao configuration data.
    /// </summary>
    public sealed class EntityDaoConfigurationCache
    {
        #region Constants

        private const string DAO_CONFIG_CACHE_STORE = "DaoConfigCacheStore";

        #endregion Constants

        /// <summary>
        /// Returns a cached configuration instance.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        /// <returns>Corresponding configuration instance.</returns>
        public static EntityDaoConfiguration Get(
            FileInfo daoFile,
            FileInfo entityDaoFile)
        {
            return Get(daoFile, entityDaoFile, null);
        }

        /// <summary>
        /// Returns a cached configuration instance.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        /// <param name="defaultEntityDaoFile">The default entity dao file.</param>
        /// <returns>Corresponding configuration instance.</returns>
        public static EntityDaoConfiguration Get(
            FileInfo daoFile,
            FileInfo entityDaoFile,
            FileInfo defaultEntityDaoFile)
        {
            // Add if neccessary.
            if (!Exists(entityDaoFile))
            {
                Add(daoFile, entityDaoFile, defaultEntityDaoFile);
            }

            // Return cached instance.
            return
                CacheUtility.GetItem<EntityDaoConfiguration>(DAO_CONFIG_CACHE_STORE, entityDaoFile.FullName);
        }

        /// <summary>
        /// Adds a configuration instance to the cache.
        /// </summary>
        /// <remarks>
        /// This implicitly performs a remove.
        /// </remarks>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        /// <param name="defaultEntityDaoFile">The default entity dao file.</param>
        public static void Add(
            FileInfo daoFile,
            FileInfo entityDaoFile,
            FileInfo defaultEntityDaoFile)
        {
            // Remove.
            Remove(entityDaoFile);

            // Deserialize.
            EntityDaoConfiguration config =
                DeserializationUtility.DeserializeFromFile<EntityDaoConfiguration>(entityDaoFile);

            // Merge with default (if necessary).
            if (defaultEntityDaoFile != null)
            {
                EntityDaoConfiguration defaultConfig =
                    DeserializationUtility.DeserializeFromFile<EntityDaoConfiguration>(defaultEntityDaoFile);
                if (defaultConfig != null)
                    config.Merge(defaultConfig);
            }

            // Parse.
            DaoConfiguration daoConfig =
                DaoConfigurationCache.Get(daoFile, true);
            if (daoConfig != null)
                daoConfig.Parse(config);

            // Add.
            CacheUtility.AddItem(DAO_CONFIG_CACHE_STORE, entityDaoFile.FullName, config);
        }

        /// <summary>
        /// Adds a configuration instance to the cache.
        /// </summary>
        /// <remarks>
        /// This implicitly performs a remove.
        /// </remarks>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        public static void Add(
            FileInfo daoFile,
            FileInfo entityDaoFile)
        {
            Add(daoFile, entityDaoFile, null);
        }

        /// <summary>
        /// Removes a configuration instance from the cache.
        /// </summary>
        /// <param name="entityDaoFile">The entity dao file.</param>
        public static void Remove(
            FileInfo entityDaoFile)
        {
            CacheUtility.RemoveItem(DAO_CONFIG_CACHE_STORE, entityDaoFile.FullName);
        }

        /// <summary>
        /// Determines whether the config sets is already cached.
        /// </summary>
        /// <param name="entityDaoFile">The entity dao file.</param>
        public static bool Exists(
            FileInfo entityDaoFile)
        {
            return
                CacheUtility.IsItemCached(DAO_CONFIG_CACHE_STORE, entityDaoFile.FullName);
        }
    }
}