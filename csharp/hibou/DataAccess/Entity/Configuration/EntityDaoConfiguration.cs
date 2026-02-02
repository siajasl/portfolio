using System.Runtime.Serialization;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using System.IO;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System;
using Keane.CH.Framework.DataAccess.ORM;

namespace Keane.CH.Framework.DataAccess.Entity.Configuration
{
    /// <summary>
    /// Entity database access object configuration data.
    /// </summary>
    /// <remarks>
    /// This class can be:
    /// 1.  Instantiated from code:
    /// 2.  Injected;
    /// 3.  Deserialized directly from xml.
    /// </remarks>    
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class EntityDaoConfiguration : 
        IDeserializationCallback
    {
        #region Constants

        private const string OPERATION_DATE = "OPERATION_DATE";
        private const string OPERATION_ENTITY_ID = "OPERATION_ENTITY_ID";
        private const string OPERATION_ENTITY_STATE = "OPERATION_ENTITY_STATE";
        private const string OPERATION_ENTITY_VERSION = "OPERATION_ENTITY_VERSION";
        private const string OPERATION_RESULT = "OPERATION_RESULT";
        private const string OPERATION_TYPE = "OPERATION_TYPE";
        private const string OPERATION_USER = "OPERATION_USER";

        private const string ENTITY_VERSION = "EntityVersion";
        private const string ENTITY_CREATE_DATE = "EntityCreateDate";
        private const string ENTITY_CREATE_USER = "EntityCreateUser";
        private const string ENTITY_UPDATE_DATE = "EntityUpdateDate";
        private const string ENTITY_UPDATE_USER = "EntityUpdateUser";
        private const string ENTITY_STATE_AS_BYTE = "EntityStateAsByte";
        private const string ENTITY_STATE = "EntityState";

        #endregion Constants

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntityDaoConfiguration()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            // Initialise standard parameters.
            if (this.StandardParameters == null)
            {
                this.StandardParameters = new EntityDaoStandardParameters()
                {
                    OperationDate = OPERATION_DATE,
                    OperationEntityId = OPERATION_ENTITY_ID,
                    OperationEntityState = OPERATION_ENTITY_STATE,
                    OperationEntityVersion = OPERATION_ENTITY_VERSION,
                    OperationResult = OPERATION_RESULT,
                    OperationType = OPERATION_TYPE,
                    OperationUser = OPERATION_USER
                };
            }

            // Initialise mappings.
            if (this.Mappings == null)
            {
                ORMappingList mappings = new ORMappingList();
                mappings.Add(ORMapping.Create(ENTITY_VERSION));
                mappings.Add(ORMapping.Create(ENTITY_CREATE_DATE));
                mappings.Add(ORMapping.Create(ENTITY_CREATE_USER));
                mappings.Add(ORMapping.Create(ENTITY_UPDATE_DATE));
                mappings.Add(ORMapping.Create(ENTITY_UPDATE_USER));
                mappings.Add(ORMapping.Create(ENTITY_STATE_AS_BYTE, ENTITY_STATE));            
                this.Mappings = mappings;
            }
        }

        #endregion Constructor

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
        /// Gets or sets the db command (a stored procedure that manages all required entity operations).
        /// </summary>
        [DataMember(IsRequired = false, Order = 3)]
        public string DbCommand
        { get; set; }

        /// <summary>
        /// Gets or sets the entity type id.
        /// </summary>
        /// <remarks>
        /// This is used in both configuration & protected entity scenarios.
        /// </remarks>
        [DataMember(IsRequired = false, Order = 4)]
        public int EntityTypeId
        { get; set; }

        /// <summary>
        /// Gets or sets the list of standard parameters used in the entity operations stored procedure.
        /// </summary>
        [DataMember(IsRequired = false, Order = 5)]
        public EntityDaoStandardParameters StandardParameters
        { get; set; }

        /// <summary>
        /// Gets or sets the object to relational mappings.
        /// </summary>
        [DataMember(IsRequired = false, Order = 6)]
        public ORMappingList Mappings
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether a parse has occurred or not.
        /// </summary>
        public bool IsParsed
        { get; internal set; }

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
        internal void Merge(EntityDaoConfiguration config)
        {
            // Dao.
            if (config.DaoConfig != null)
                this.DaoConfig = config.DaoConfig;

            // Stored procedure.
            if (!String.IsNullOrEmpty(config.DbCommand))
                this.DbCommand = config.DbCommand;

            // Db connection key.
            if (!String.IsNullOrEmpty(config.DbConnectionKey))
                this.DbConnectionKey = config.DbConnectionKey;

            // Entity type id.
            if (config.EntityTypeId > 0 && this.EntityTypeId == 0)
                this.EntityTypeId = config.EntityTypeId;

            // Standard paramters.
            if (config.StandardParameters != null)
            {
                if (this.StandardParameters == null)
                    this.StandardParameters = new EntityDaoStandardParameters();
                this.StandardParameters.Merge(config.StandardParameters);
            }

            // Mappings.
            if (config.Mappings != null)
            {
                if (this.Mappings == null)
                    this.Mappings = new ORMappingList();
                this.Mappings.Merge(config.Mappings);
            }

            // Indicate that a merge has been performed.
            config.IsMerged = true;
        }

        #endregion Methods

        #region IDeserializationCallback Members

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            this.InitialiseMembers();
        }

        #endregion
    }
}