using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Keane.CH.Framework.Core.Utilities.DataContract;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.DataAccess.Search.Configuration;
using Keane.CH.Framework.Core.Utilities.Caching;
using System.Configuration;

namespace Keane.CH.Framework.DataAccess.Configuration
{
    /// <summary>
    /// Manages data access configuration items.
    /// </summary>
    public sealed class ConfigurationCache
    {
        #region Constants

        private const string CACHE_STORE = "DaoConfigCacheStore";
        private const string CACHE_ITEM_DAO = "Dao";
        private const string CACHE_ITEM_ENTITY_DAO = "EntityDao";
        private const string CACHE_ITEM_SEARCH_DAO = "SearchDao";
        private const string CACHE_ITEM_IS_INITIALISED = "IsInitialised";
        private const string CACHE_ITEM_IS_INITIALISING = "IsInitialising";
        private const string CONFIG_ROOT_FOLDER_APP_SETTING = "Keane.CH.Framework.ConfigRootFolder";
        
        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets or sets a flag inidcating whether the cache has been initialised or not.
        /// </summary>
        private static bool IsInitialised
        {
            get 
            {
                bool result = false;
                if (CacheUtility.IsItemCached(CACHE_STORE, CACHE_ITEM_IS_INITIALISED))
                {
                    result = CacheUtility.GetItem<bool>(CACHE_STORE, CACHE_ITEM_IS_INITIALISED);
                }
                return result;
            }
            set 
            {
                CacheUtility.AddItem(CACHE_STORE, CACHE_ITEM_IS_INITIALISED, value);
            }
        }

        /// <summary>
        /// Gets or sets a flag inidcating whether the cache is in the process of being initialised.
        /// </summary>
        private static bool IsInitialising
        {
            get
            {
                bool result = false;
                if (CacheUtility.IsItemCached(CACHE_STORE, CACHE_ITEM_IS_INITIALISING))
                {
                    result = CacheUtility.GetItem<bool>(CACHE_STORE, CACHE_ITEM_IS_INITIALISING);
                }
                return result;
            }
            set
            {
                CacheUtility.AddItem(CACHE_STORE, CACHE_ITEM_IS_INITIALISING, value);
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Initialises the cache using the directory defined within configuration application settings.
        /// </summary>
        public static void Initialise()
        {
            if (!IsInitialised && !IsInitialising)
            {
                IsInitialising = true;
                if (ConfigurationManager.AppSettings[CONFIG_ROOT_FOLDER_APP_SETTING] != null)
                {
                    string directoryPath =
                        ConfigurationManager.AppSettings[CONFIG_ROOT_FOLDER_APP_SETTING];
                    if (!string.IsNullOrEmpty(directoryPath))
                    {
                        directoryPath = Path.Combine(directoryPath, "DataAccess");
                        DirectoryInfo directory = new DirectoryInfo(directoryPath);
                        Initialise(directory);
                    }
                }
            }
        }

        /// <summary>
        /// Initialises the cache with the directory within which the config files reside.
        /// </summary>
        /// <remarks>
        /// A search is performed over the passed directory and all data access config files are automatically cached.
        /// </remarks>
        /// <param name="directory">A directory in which data access config files exist.</param>
        public static void Initialise(DirectoryInfo directory)
        {
            // Defensive programming.
            Debug.Assert(directory != null, "directory parameter is null.");
            Debug.Assert(directory.Exists, string.Format("directory does not exist : {0}", directory.FullName));

            // Add all the files to the cache.
            Add(directory);

            // Assign associated dao's.
            AssignDaoToEntityDao();
            AssignDaoToSearchDao();

            // Merge with defaults.
            MergeEntityDaoWithDefault();
            MergeSearchDaoWithDefault();

            // Set initialised flag.
            CacheUtility.AddItem(CACHE_STORE, CACHE_ITEM_IS_INITIALISED, true);
        }

        /// <summary>
        /// Returns the count of items within the cache.
        /// </summary>
        /// <returns>The total count of cached items of the relevant type.</returns>
        public static int GetCount()
        {
            int result = 0;
            result += GetCount<DaoConfiguration>();
            result += GetCount<EntityDaoConfiguration>();
            result += GetCount<SearchDaoConfiguration>();
            return result;
        }

        /// <summary>
        /// Returns the count of items within the cache.
        /// </summary>
        /// <typeparam name="T">The type of supported config file.</typeparam>
        /// <returns>The count of cached items of the relevant type.</returns>
        public static int GetCount<T>()
        {
            int result = 0;
            Dictionary<string, object> collection = GetDictionary<T>();
            if (collection != null)
                result = collection.Count;
            return result;
        }

        /// <summary>
        /// Gets the default search dao config file.
        /// </summary>
        /// <returns>The default search dao config file.</returns>
        public static SearchDaoConfiguration GetDefaultSearchDaoConfig()
        {
            SearchDaoConfiguration result = null;

            // Derive cached collection.
            Dictionary<string, object> collection = GetDictionary<SearchDaoConfiguration>();

            // Derive first item with it's IsDefault flag set to true.
            if (collection != null)
            {
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    SearchDaoConfiguration item = kvp.Value as SearchDaoConfiguration;
                    if (item != null &&
                        item.IsDefault)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the default entity dao config file.
        /// </summary>
        /// <returns>The default entity dao config file.</returns>
        public static EntityDaoConfiguration GetDefaultEntityDaoConfig()
        {
            EntityDaoConfiguration result = null;

            // Derive cached collection.
            Dictionary<string, object> collection = GetDictionary<EntityDaoConfiguration>();

            // Derive first item with it's IsDefault flag set to true.
            if (collection != null)
            {
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    EntityDaoConfiguration item = kvp.Value as EntityDaoConfiguration;
                    if (item != null &&
                        item.IsDefault)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the default dao config file.
        /// </summary>
        /// <returns>The default cached dao config file.</returns>
        public static DaoConfiguration GetDefaultDaoConfig()
        {
            DaoConfiguration result = null;

            // Derive cached collection.
            Dictionary<string, object> collection = GetDictionary<DaoConfiguration>();

            // Return the first item with it's IsDefault property = true.
            if (collection != null)
            {
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    DaoConfiguration item = kvp.Value as DaoConfiguration;
                    if (item != null &&
                        item.IsDefault)
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }
        
        /// <summary>
        /// Gets the dao config file with a matching key.
        /// </summary>
        /// <param name="key">The key of the cached dao config.</param>
        /// <returns>The cached dao config.</returns>
        public static DaoConfiguration GetDaoConfig(string key)
        {
            DaoConfiguration result = null;

            if (!string.IsNullOrEmpty(key))
            {
                Dictionary<string, object> collection = GetDictionary<DaoConfiguration>();
                if (collection != null)
                {
                    foreach (KeyValuePair<string, object> kvp in collection)
                    {
                        DaoConfiguration item = kvp.Value as DaoConfiguration;
                        if (item != null &&
                            item.DbConnectionKey.Equals(key))
                        {
                            result = item;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the entity dao config file with a matching type id.
        /// </summary>
        /// <param name="typeId">The entity type id used to distinguish between entity types.</param>
        /// <returns>The cached entity dao config.</returns>
        public static EntityDaoConfiguration GetEntityDaoConfig(int typeId)
        {
            EntityDaoConfiguration result = null;

            Dictionary<string, object> collection = GetDictionary<EntityDaoConfiguration>();
            if (collection != null)
            {
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    EntityDaoConfiguration item = kvp.Value as EntityDaoConfiguration;
                    if (item != null &&
                        item.EntityTypeId.Equals(typeId))
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the search dao config file with a matching key.
        /// </summary>
        /// <param name="typeId">The search type id used to distinguish between search types.</param>
        /// <returns>The cached search dao config.</returns>
        public static SearchDaoConfiguration GetSearchDaoConfig(int typeId)
        {
            SearchDaoConfiguration result = null;

            Dictionary<string, object> collection = GetDictionary<SearchDaoConfiguration>();
            if (collection != null)
            {
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    SearchDaoConfiguration item = kvp.Value as SearchDaoConfiguration;
                    if (item != null &&
                        item.SearchTypeId.Equals(typeId))
                    {
                        result = item;
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public static void Clear()
        {
            CacheUtility.Clear(CACHE_STORE);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Merges search config items with the default search config item.
        /// </summary>
        private static void AssignDaoToSearchDao()
        {
            Dictionary<string, object> collection = GetDictionary<SearchDaoConfiguration>();
            if (collection != null)
            {
                DaoConfiguration defaultConfig = GetDefaultDaoConfig();
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    SearchDaoConfiguration config = kvp.Value as SearchDaoConfiguration;
                    if (config != null)
                    {
                        if (string.IsNullOrEmpty(config.DbConnectionKey))
                            config.DaoConfig = defaultConfig;
                        else
                            config.DaoConfig = GetDaoConfig(config.DbConnectionKey);
                    }
                }
            }
        }

        /// <summary>
        /// Merges search config items with the default search config item.
        /// </summary>
        private static void AssignDaoToEntityDao()
        {
            Dictionary<string, object> collection = GetDictionary<EntityDaoConfiguration>();
            if (collection != null)
            {
                DaoConfiguration defaultConfig = GetDefaultDaoConfig();
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    EntityDaoConfiguration config = kvp.Value as EntityDaoConfiguration;
                    if (config != null)
                    {
                        if (string.IsNullOrEmpty(config.DbConnectionKey))
                            config.DaoConfig = defaultConfig;
                        else
                            config.DaoConfig = GetDaoConfig(config.DbConnectionKey);
                    }
                }
            }
        }

        /// <summary>
        /// Merges search config items with the default search config item.
        /// </summary>
        private static void MergeSearchDaoWithDefault()
        {
            SearchDaoConfiguration defaultConfig = GetDefaultSearchDaoConfig();
            if (defaultConfig != null)
            {
                Dictionary<string, object> collection = GetDictionary<SearchDaoConfiguration>();
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    SearchDaoConfiguration config = kvp.Value as SearchDaoConfiguration;
                    if (config != null &&
                        !config.IsDefault)
                    {
                        config.Merge(defaultConfig);
                    }
                }
            }
        }

        /// <summary>
        /// Merges entity config items with the default entity config item.
        /// </summary>
        private static void MergeEntityDaoWithDefault()
        {
            EntityDaoConfiguration defaultConfig = GetDefaultEntityDaoConfig();
            if (defaultConfig != null)
            {
                Dictionary<string, object> collection = GetDictionary<EntityDaoConfiguration>();
                foreach (KeyValuePair<string, object> kvp in collection)
                {
                    EntityDaoConfiguration config = kvp.Value as EntityDaoConfiguration;
                    if (config != null &&
                        !config.IsDefault)
                    {
                        config.Merge(defaultConfig);
                    }
                }
            }
        }
        
        /// <summary>
        /// Add the passed file to the cache.
        /// </summary>
        /// <param name="file">The file to be cached.</param>
        private static void Add(FileInfo file)
        {
            // Deserialize file to supported config file.
            object deserializedFile = GetDeserializedFile(file);

            // Add deserialized file to relevant dictionary.
            if (deserializedFile != null)
            {
                // Derive dictionary & item key.
                string itemKey = GetKey(file, deserializedFile);
                Dictionary<string, object> collection = GetDictionary(deserializedFile);

                // Remove & add item to the dictionary.
                collection.Remove(itemKey);
                collection.Add(itemKey, deserializedFile);
            }
        }

        /// <summary>
        /// Adds all files within the passed directory to the cache.
        /// </summary>
        /// <param name="directory">The directory whose files will be cached.</param>
        private static void Add(DirectoryInfo directory)
        {
            // Cache files.
            foreach (FileInfo file in directory.GetFiles())
            {
                Add(file);
            }

            // Recurse.
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                Add(subDirectory);
            }
        }

        /// <summary>
        /// Deserializes the passed file to a supported data access configuration type.
        /// </summary>
        /// <param name="file">The file to be deserialized.</param>
        private static object GetDeserializedFile(FileInfo file)
        {
            object result = null;

            // Deserialize entity dao files.
            if (result == null)
            {
                result = GetDeserializedFile<EntityDaoConfiguration>(file);
            }

            // Deserialize search dao files.
            if (result == null)
            {
                result = GetDeserializedFile<SearchDaoConfiguration>(file);
            }

            // Deserialize standard dao files.            
            if (result == null)
            {
                result = GetDeserializedFile<DaoConfiguration>(file);
            }

            return result;
        }

        /// <summary>
        /// Deserializes the passed file to a supported data access configuration type.
        /// </summary>
        /// <typeparam name="T">The type of dao config file.</typeparam>
        /// <param name="file">The file to be deserialized.</param>
        private static T GetDeserializedFile<T>(FileInfo file)
        {
            T result = default(T);
            try
            {
                result =
                    DeserializationUtility.DeserializeFromFile<T>(file);
            }
            catch (Exception)
            {
                // Do nothing as exceptions are suppressed.
            }
            return result;
        }

        /// <summary>
        /// Gets the associated config file dictionary based on the passed deserialized config file type.
        /// </summary>
        /// <param name="file">The deserialized config file.</param>
        private static Dictionary<string, object> GetDictionary(object deserializedFile)
        {
            Dictionary<string, object> result = null;

            // Entity dao files.
            if (deserializedFile is EntityDaoConfiguration)
            {
                result = GetDictionary<EntityDaoConfiguration>();
            }

            // Search dao files.
            if (deserializedFile is SearchDaoConfiguration)
            {
                result = GetDictionary<SearchDaoConfiguration>();
            }

            // Standard dao files.            
            if (deserializedFile is DaoConfiguration)
            {
                result = GetDictionary<DaoConfiguration>();
            }

            return result;
        }

        /// <summary>
        /// Gets the associated config file dictionary based on the passed config file type.
        /// </summary>
        /// <typeparam name="T">The type of supported config file.</typeparam>
        /// <param name="file">The deserialized config file.</param>
        private static Dictionary<string, object> GetDictionary<T>()
        {
            Dictionary<string, object> result = null;

            // Entity dao files.
            if (typeof(T).Equals(typeof(EntityDaoConfiguration)))
            {
                result = GetDictionary(CACHE_ITEM_ENTITY_DAO);
            }

            // Search dao files.
            if (typeof(T).Equals(typeof(SearchDaoConfiguration)))
            {
                result = GetDictionary(CACHE_ITEM_SEARCH_DAO);
            }

            // Standard dao files.            
            if (typeof(T).Equals(typeof(DaoConfiguration)))
            {
                result = GetDictionary(CACHE_ITEM_DAO);
            }

            return result;
        }

        /// <summary>
        /// Gets the associated config file dictionary based on the passed config file type.
        /// </summary>
        /// <param name="dictionaryType">The key of the cached dictionary.</param>
        /// <returns>The cached config file dictionary.</returns>
        /// <remarks>This will JIT (just in time) instantiate a new instance of the relevant dictionary.</remarks>
        private static Dictionary<string, object> GetDictionary(string dictionaryKey)
        {
            Dictionary<string, object> result =
               CacheUtility.GetItem<Dictionary<string, object>>(CACHE_STORE, dictionaryKey);
            if (result == null)
            {
                result = new Dictionary<string, object>();
                CacheUtility.AddItem(CACHE_STORE, dictionaryKey, result);
            }
            return result;
        }

        /// <summary>
        /// Derives the cache item key to use when adding/removing an item from the cache.
        /// </summary>
        /// <param name="file">The physical file being processed.</param>
        /// <param name="deserializedFile">The deserialized file being processed.</param>
        private static string GetKey(FileInfo file, object deserializedFile)
        {
            string result = file.FullName;
            return result;
        }

        #endregion Private Methods
    }
}
