using System;
using System.Linq;
using Keane.CH.Framework.Core.Utilities.Caching;
using Keane.CH.Framework.Services.Core.Operation;
using System.Data;
using Keane.CH.Framework.Services.Codes.Contracts;
using Keane.CH.Framework.Services.Entity.Contracts.Data;
using Keane.CH.Framework.Apps.UI.Core.Application;

namespace Keane.CH.Framework.Apps.UI.Core.Application
{
    /// <summary>
    /// Manages the application's local cache.
    /// </summary>
    public abstract class ApplicationCache : 
        IApplicationCache
    {
        #region Constants

        private const string CACHE_INIT_FLAG = "CachesAreInitialised";
        private const string CACHE_STORE_APPLICATION = "ApplicationCacheStore";
        private const string CACHE_STORE_DAO_CONFIG = "DaoConfigCacheStore";
        private const string CACHE_STORE_ENTITY = "EntityCacheStore";
        private const string CACHE_STORE_TEMPLATE = "TemplateCacheStore";        

        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets or sets whether the cache has been initialised or not.
        /// </summary>
        private bool IsInitialised
        {
            get 
            {
                return CacheUtility.IsItemCached(CACHE_STORE_APPLICATION, CACHE_INIT_FLAG);
            }
            set 
            {
                CacheUtility.AddItem(CACHE_STORE_APPLICATION, CACHE_INIT_FLAG, value);
            }
        }

        #endregion Properties

        #region IApplicationCache Members

        /// <summary>
        /// Gets the associated cache accessor.
        /// </summary>
        public IApplicationCacheAccessor Accessor
        { get; set; }

        /// <summary>
        /// Initialises the cache.
        /// </summary>
        public virtual void Initialise()
        {
            if (!IsInitialised)
            {
                // Ensure caches are clear.
                this.Clear();

                // Initialise caches.
                this.InitialiseApplicationCache();
                this.InitialiseDaoConfigCache();
                this.InitialiseEntityCache();
                this.InitialiseTemplateCache();
                
                // Set the initialisation flag.
                IsInitialised = true;
            }
        }

        /// <summary>
        /// Resets the caches.
        /// </summary>
        public virtual void Reset()
        {
            this.Clear();
            this.IsInitialised = false;
            this.Initialise();
        }

        /// <summary>
        /// Empties the caches.
        /// </summary>
        public virtual void Clear()
        {
            CacheUtility.Clear(CACHE_STORE_APPLICATION);
            CacheUtility.Clear(CACHE_STORE_DAO_CONFIG);
            CacheUtility.Clear(CACHE_STORE_ENTITY);
            CacheUtility.Clear(CACHE_STORE_TEMPLATE);
        }

        /// <summary>
        /// Initialises the local application cache.
        /// </summary>
        public virtual void InitialiseApplicationCache()
        { }

        /// <summary>
        /// Initialises the local dao config cache.
        /// </summary>
        public virtual void InitialiseDaoConfigCache()
        { }

        /// <summary>
        /// Initialises the local entity cache.
        /// </summary>
        public virtual void InitialiseEntityCache()
        { }

        /// <summary>
        /// Initialises the template cache.
        /// </summary>
        public virtual void InitialiseTemplateCache()
        { }

        #endregion IApplicationCache members
    }
}