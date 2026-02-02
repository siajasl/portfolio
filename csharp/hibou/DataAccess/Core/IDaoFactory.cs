using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.DataAccess.Search.Configuration;

namespace Keane.CH.Framework.DataAccess.Core
{
    /// <summary>
    /// Manages instantiation of dao abstract pointers.
    /// </summary>
    public sealed class IDaoFactory
    {
        #region Constructors

        private IDaoFactory() { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Instantiates & returns a dao interface.
        /// </summary>
        /// <param name="configSet">A dao configuration set.</param>
        /// <returns>A configured dao interface.</returns>
        public static IDao Create(
            DaoConfigurationSet configSet)
        {
            // Defensive programming.
            if (configSet == null)
                throw new ArgumentNullException("configSet");

            // Instantiate concrete instance.
            Dao concreteInstance = new Dao();

            // Configure.
            concreteInstance.Configure(configSet);

            // Return abstract pointer.
            return (IDao)concreteInstance;
        }

        /// <summary>
        /// Instantiates & returns a dao interface.
        /// </summary>
        /// <param name="configCacheStore">A configuration cache store key.</param>
        /// <param name="daoconfigFilePath">A dao config data file path.</param>
        /// <returns>A configured dao interface.</returns>
        public static IDao Create(
            string configCacheStore, 
            string daoconfigFilePath)
        {
            // Defensive programming.
            if (String.IsNullOrEmpty(configCacheStore))
                throw new ArgumentNullException("configCacheStore");
            if (String.IsNullOrEmpty(daoconfigFilePath))
                throw new ArgumentNullException("daoConfigFilePath");

            // Instantiate config set & invoke overload.
            DaoConfigurationSet configSet = new DaoConfigurationSet() 
            { 
                CacheStore = configCacheStore,
                DaoDataFilePath = daoconfigFilePath
            };
            return Create(configSet);
        }

        #endregion Methods
    }
}