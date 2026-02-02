using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Search.Configuration;
using Keane.CH.Framework.Services.Search.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Search
{
    /// <summary>
    /// Manages instantiation of search dao abstract pointers.
    /// </summary>
    public sealed class ISearchDaoFactory
    {
        #region Methods

        /// <summary>
        /// Instantiates & returns a search dao interface.
        /// </summary>
        /// <param name="configSet">A search dao configuration set.</param>
        /// <returns>A configured search dao interface.</returns>
        public static ISearchDao<C, I> Create<C, I>(
            SearchDaoConfigurationSet configSet)
            where C : new()
            where I : new()
        {
            // Defensive programming.
            if (configSet == null)
                throw new ArgumentNullException("configSet");

            // Instantiate concrete instance.
            SearchDao<C, I> concreteInstance = new SearchDao<C, I>();

            // Configure.
            concreteInstance.Configure(configSet);

            // Return abstract pointer.
            return (ISearchDao<C, I>)concreteInstance;
        }

        /// <summary>
        /// Instantiates & returns a search dao interface.
        /// </summary>
        /// <param name="configCacheStore">The configuration cache store key.</param>
        /// <param name="daoconfigFilePath">The dao config data file path.</param>
        /// <param name="dataFilePath">The entity dao config data file path.</param>
        /// <param name="defaultDataFilePath">The default entity dao config data file path.</param>
        /// <returns>A configured search dao interface.</returns>
        public static ISearchDao<C, I> Create<C, I>(
            string configCacheStore,
            string daoconfigFilePath,
            string dataFilePath,
            string defaultDataFilePath)
            where C : new()
            where I : new()
        {
            // Defensive programming.
            if (String.IsNullOrEmpty(configCacheStore))
                throw new ArgumentNullException("configCacheStore");
            if (String.IsNullOrEmpty(daoconfigFilePath))
                throw new ArgumentNullException("daoconfigFilePath");
            if (String.IsNullOrEmpty(dataFilePath))
                throw new ArgumentNullException("dataFilePath");

            // Instantiate config set & invoke overload.
            SearchDaoConfigurationSet configSet = new SearchDaoConfigurationSet()
            {
                CacheStore = configCacheStore,
                DaoFilePath = daoconfigFilePath,
                SearchDaoFilePath = dataFilePath,
                DefaultSearchDaoFilePath = defaultDataFilePath
            };
            return Create<C, I>(configSet);
        }

        /// <summary>
        /// Instantiates & returns a search dao interface.
        /// </summary>
        /// <param name="configCacheStore">The configuration cache store key.</param>
        /// <param name="daoconfigFilePath">The dao config data file path.</param>
        /// <param name="dataFilePath">The search dao config data file path.</param>
        /// <returns>A configured search dao interface.</returns>
        public static ISearchDao<C, I> Create<C, I>(
            string configCacheStore,
            string daoconfigFilePath,
            string dataFilePath)
            where C : new()
            where I : new()
        {
            return Create<C, I>(configCacheStore, daoconfigFilePath, dataFilePath, String.Empty);
        }

        #endregion Methods
    }
}