using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Search.Configuration;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.DataAccess.Core.Factory;
using System.IO;
using System.Diagnostics;

namespace Keane.CH.Framework.DataAccess.Search.Factory
{
    /// <summary>
    /// Manages instantiation of search dao abstract pointers.
    /// </summary>
    public sealed class SearchDaoCreator
    {
        #region Methods

        /// <summary>
        /// Instantiates & returns a search dao interface.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="searchDaoFile">The search dao file.</param>
        /// <param name="defaultEntityDaoFile">The default search dao file.</param>
        /// <returns>A configured search dao interface.</returns>
        public static ISearchDao Create<I>(
            FileInfo daoFile,
            FileInfo searchDaoFile,
            FileInfo defaultSearchDaoFile)
            where I : new()
        {
            // Defensive programming.
            Debug.Assert(daoFile != null, "daoFile parameter is null");
            Debug.Assert(searchDaoFile != null, "searchDaoFile parameter is null");
            Debug.Assert(daoFile.Exists, "daoFile does not exist");
            Debug.Assert(searchDaoFile.Exists, "searchDaoFile does not exist");
            if (defaultSearchDaoFile != null)
                Debug.Assert(defaultSearchDaoFile.Exists, "defaultSearchDaoFile does not exist");

            // Instantiate concrete instance.
            SearchDao<I> concreteInstance = new SearchDao<I>();

            // Configure.
            concreteInstance.Config =
                SearchDaoConfigurationCache.Get(daoFile, searchDaoFile, defaultSearchDaoFile);
            concreteInstance.Dao =
                DaoCreator.Create(daoFile);

            // Return abstract pointer.
            return (ISearchDao)concreteInstance;
        }

        /// <summary>
        /// Instantiates & returns a search dao interface.
        /// </summary>
        /// <param name="configSet">A search dao configuration set.</param>
        /// <returns>A configured search dao interface.</returns>
        public static ISearchDao Create<I>(
            SearchDaoConfigurationSet configSet)
            where I : new()
        {
            // Defensive programming.
            if (configSet == null)
                throw new ArgumentNullException("configSet");

            // Instantiate concrete instance.
            SearchDao<I> concreteInstance = new SearchDao<I>();

            // Configure.
            concreteInstance.Config =
                SearchDaoConfigurationCache.Get(configSet.DaoFile, configSet.SearchDaoFile, configSet.DefaultSearchDaoFile);
            concreteInstance.Dao =
                DaoCreator.Create(configSet.DaoFile);

            // Return abstract pointer.
            return (ISearchDao)concreteInstance;
        }

        #endregion Methods
    }
}