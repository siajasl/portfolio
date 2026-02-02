using System.Runtime.Serialization;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using System.IO;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System;
using Keane.CH.Framework.DataAccess.ORM;

namespace Keane.CH.Framework.DataAccess.Search.Configuration
{
    /// <summary>
    /// Search database access object configuration data.
    /// </summary>
    /// <remarks>
    /// This class can be:
    /// 1.  Instantiated from code:
    /// 2.  Injected;
    /// 3.  Deserialized directly from xml.
    /// </remarks>    
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class SearchDaoConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets a flag indicating whether the config represents a default to be applied to all entity configurations.
        /// </summary>
        [DataMember(IsRequired = false, Order = 1)]
        public bool IsDefault
        { get; set; }

        /// <summary>
        /// Gets or sets the database connection key to be used.
        /// </summary>
        /// <remarks>
        /// If this is set to null then the default db connection is used.
        /// </remarks>
        [DataMember(IsRequired = false, Order = 2)]
        public string DbConnectionKey
        { get; set; }

        /// <summary>
        /// Gets or sets the db command (a stored procedure that manages all required search operations).
        /// </summary>
        [DataMember(IsRequired = false, Order = 3)]
        public string DbCommand
        { get; set; }

        /// <summary>
        /// Gets or sets the search type id.
        /// </summary>
        /// <remarks>
        /// This is used in configuration scenarios.
        /// </remarks>
        [DataMember(IsRequired = false, Order = 5)]
        public int SearchTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the mapping collection used for processing the search criteria.
        /// </summary>
        [DataMember(IsRequired = false, Order = 6)]
        public ORMappingList CriteriaMappingList
        { get; set; }

        /// <summary>
        /// Gets or sets the mapping collection used for processing the search result.
        /// </summary>
        [DataMember(IsRequired = false, Order = 7)]
        public ORMappingList ResultMappingList
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whther a parse has occurred or not.
        /// </summary>
        internal bool IsParsed
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether a merge has occurred or not.
        /// </summary>
        internal bool IsMerged
        { get; set; }

        /// <summary>
        /// Gets or sets the associated dao configuration.
        /// </summary>
        public DaoConfiguration DaoConfig
        { get; internal set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Merges the passed configuration with this one.
        /// </summary>
        /// <param name="config">The configuration from which to merge.</param>
        internal void Merge(SearchDaoConfiguration config)
        {
            // Dao.
            if (config.DaoConfig != null)
                this.DaoConfig = config.DaoConfig;

            // Db connection key.
            if (!String.IsNullOrEmpty(config.DbConnectionKey))
                this.DbConnectionKey = config.DbConnectionKey;

            // Stored procedure.
            if (!String.IsNullOrEmpty(config.DbCommand))
                this.DbCommand = config.DbCommand;

            // Criteria mappings.
            if (config.CriteriaMappingList != null)
            {
                if (this.CriteriaMappingList == null)
                    this.CriteriaMappingList = new ORMappingList();
                this.CriteriaMappingList.Merge(config.CriteriaMappingList);
            }

            // Result item mappings.
            if (config.ResultMappingList != null)
            {
                if (this.ResultMappingList == null)
                    this.ResultMappingList = new ORMappingList();
                this.ResultMappingList.Merge(config.ResultMappingList);
            }

            // Indicate that a merge has been performed.
            config.IsMerged = true;
        }

        #endregion Methods
    }
}