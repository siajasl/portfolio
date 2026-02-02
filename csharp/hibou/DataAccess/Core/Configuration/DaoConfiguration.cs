using System;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System.IO;
using Keane.CH.Framework.DataAccess.Search.Configuration;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using System.Diagnostics;

namespace Keane.CH.Framework.DataAccess.Core.Configuration
{
    /// <summary>
    /// Database access object configuration data.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public class DaoConfiguration
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
        /// 1.  If this is set to null then the default db connection is used.
        /// 2.  This maps to the Enterprise Library connection string.
        /// </remarks>
        [DataMember(IsRequired = false, Order = 2)]
        public string DbConnectionKey
        { get; set; }

        /// <summary>
        /// Gets or sets the type of database in use.
        /// </summary>
        [DataMember(IsRequired = false, Order = 3)]
        public DaoDbType DatabaseType
        { get; set; }

        /// <summary>
        /// Gets or sets the prefix to be applied to parameters.
        /// </summary>
        [DataMember(IsRequired = false, Order = 4)]
        public string ParameterPrefix
        { get; set; }

        /// <summary>
        /// Gets or sets the schema name to use.
        /// </summary>
        [DataMember(IsRequired = false, Order = 5)]
        public string SchemaName
        { get; set; }

        /// <summary>
        /// Gets or sets the package name to use (see Oracle packages).
        /// </summary>
        [DataMember(IsRequired = false, Order = 6)]
        public string PackageName
        { get; set; }

        /// <summary>
        /// Gets or sets the time (in seconds) after which an operation is deemed to have timed out.
        /// </summary>
        [DataMember(IsRequired = false, Order = 7)]
        public int CommandTimeout
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Parses the passed entity dao configuration.
        /// </summary>
        /// <param name="entityDaoConfig">The entity dao configuration to parse.</param>
        internal void Parse(EntityDaoConfiguration entityDaoConfig)
        {
            // Stored procedure.
            if (!String.IsNullOrEmpty(entityDaoConfig.DbCommand))
            {
                entityDaoConfig.DbCommand =
                    ParseStoredProcedureName(entityDaoConfig.DbCommand);
            }

            // Standard parameters.
            if (entityDaoConfig.StandardParameters != null)
            {
                entityDaoConfig.StandardParameters.OperationDate =
                    ParseParameterName(entityDaoConfig.StandardParameters.OperationDate);
                entityDaoConfig.StandardParameters.OperationEntityId =
                    ParseParameterName(entityDaoConfig.StandardParameters.OperationEntityId);
                entityDaoConfig.StandardParameters.OperationEntityState =
                    ParseParameterName(entityDaoConfig.StandardParameters.OperationEntityState);
                entityDaoConfig.StandardParameters.OperationEntityVersion =
                    ParseParameterName(entityDaoConfig.StandardParameters.OperationEntityVersion);
                entityDaoConfig.StandardParameters.OperationResult =
                    ParseParameterName(entityDaoConfig.StandardParameters.OperationResult);
                entityDaoConfig.StandardParameters.OperationType =
                    ParseParameterName(entityDaoConfig.StandardParameters.OperationType);
                entityDaoConfig.StandardParameters.OperationUser =
                    ParseParameterName(entityDaoConfig.StandardParameters.OperationUser);
            }

            // Mappings.
            if (entityDaoConfig.Mappings != null)
            {
                entityDaoConfig.Mappings.ForEach(
                    m => m.DbParameter = ParseParameterName(m.DbParameter));
            }

            // Set flag.
            entityDaoConfig.IsParsed = true;
        }

        /// <summary>
        /// Parses the passed search dao configuration.
        /// </summary>
        /// <param name="searchDaoConfig">The search dao configuration to parse.</param>
        internal void Parse(SearchDaoConfiguration searchDaoConfig)
        {
            // Stored procedure.
            if (!String.IsNullOrEmpty(searchDaoConfig.DbCommand))
            {
                searchDaoConfig.DbCommand =
                    ParseStoredProcedureName(searchDaoConfig.DbCommand);
            }

            // Mappings.
            if (searchDaoConfig.CriteriaMappingList != null)
            {
                searchDaoConfig.CriteriaMappingList.ForEach(
                    m => m.DbParameter = ParseParameterName(m.DbParameter));            
            }
            if (searchDaoConfig.ResultMappingList != null)
            {
                searchDaoConfig.ResultMappingList.ForEach(
                    m => m.DbParameter = ParseParameterName(m.DbParameter));
            }

            // Set flag.
            searchDaoConfig.IsParsed = true;
        }

        /// <summary>
        /// Parses the parameter name (i.e. adds relevant prefix/suffix).
        /// </summary>
        /// <param name="storedProcedureName">The name of the parameter to be parsed.</param>
        /// <returns>A parsed parameter name.</returns>
        public string ParseParameterName(string parameterName)
        {
            string result = parameterName;
            if (!String.IsNullOrEmpty(parameterName))
            {
                if (!string.IsNullOrEmpty(ParameterPrefix))
                {
                    result = ParameterPrefix.Trim();
                    result += parameterName.Trim();
                }
                else
                {
                    result = parameterName.Trim();
                }
            }
            return result;
        }

        /// <summary>
        /// Parses the stored procedure name (i.e. adds relevant prefix/suffix).
        /// </summary>
        /// <param name="storedProcedureName">The name of the stored procedure to be parsed.</param>
        /// <returns>A parsed stored procedure name.</returns>
        public string ParseStoredProcedureName(string storedProcedureName)
        {
            // Defensive programming.
            if (String.IsNullOrEmpty(storedProcedureName))
                return storedProcedureName;

            // Prefix with schema name.
            string result = string.Empty;
            if (!String.IsNullOrEmpty(SchemaName))
            {
                result += SchemaName;
                result += @".";
            }

            // Prefix with package name (if using Oracle).
            if ((DatabaseType == DaoDbType.Oracle) &&
                (!String.IsNullOrEmpty(PackageName)))
            {
                result += PackageName;
                result += @".";
            }

            // Append stored procedure name.
            result += storedProcedureName.Trim();

            // Return.
            return result.Trim();
        }

        #endregion Methods
    }
}