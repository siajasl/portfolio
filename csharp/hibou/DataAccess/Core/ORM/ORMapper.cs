using System;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.Core.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Keane.CH.Framework.DataAccess.Core
{
    /// <summary>
    /// An object to relational mapper.
    /// </summary>
    /// <remarks>
    /// This maps an object to a DbCommand.
    /// </remarks>
    public sealed class ORMapper
    {
        #region Public methods

        /// <summary>
        /// Performs object to relational mapping.
        /// </summary>
        /// <param name="mappingList">A list of mappings.</param>
        /// <param name="db">A database against which the command will be executed.</param>
        /// <param name="cmd">A command being prepared for execution.</param>
        /// <param name="instance">An object being mapped to a DbCommand.</param>
        public static void MapO2R(
            ORMappingList mappingList,
            Database db,
            DbCommand cmd,
            object instance)
        {
            // Abort if the execution conditions are not correct.
            if (mappingList == null)
                throw new ArgumentNullException("mappingList");
            if (db == null)
                throw new ArgumentNullException("db");
            if (cmd == null)
                throw new ArgumentNullException("cmd");
            if (instance == null)
                throw new ArgumentNullException("instance");

            // Filter.
            ORMappingList mappings = mappingList.FilterForO2R();

            // Map accordingly.
            mappings.ForEach(m => MapO2R(m, db, cmd, instance));
        }

        /// <summary>
        /// Performs relational to object mapping.
        /// </summary>
        /// <param name="mappingList">A list of mappings.</param>
        /// <param name="dr">A data reader containing data for mapping.</param>
        /// <param name="instance">An object being mapped to a DbCommand.</param>
        public static void MapR2O(
            ORMappingList mappingList,
            IDataReader dr,
            object instance)
        {
            // Abort if the execution conditions are not correct.
            if (mappingList == null)
                throw new ArgumentNullException("mappingList");
            if (dr == null)
                throw new ArgumentNullException("db");
            if (instance == null)
                throw new ArgumentNullException("instance");

            // Filter.
            ORMappingList mappings = mappingList.FilterForR2O();

            // Clone (resetting the "WasMapped" flag).
            mappings = mappings.Clone(true);

            // Map default members.
            mappings.ForEach(m => MapR2O(m, dr, instance));

            // Map entity members.
            mappings = mappings.GetUnmappedMappings();
            if (mappings.Count > 0)
            {
                EntityBase entity = instance as EntityBase;
                if (entity != null)
                    mappings.ForEach(m => MapR2O(m, dr, entity.EntityInfo));
            }

            // Raise exception if any mappings failed.
            mappings = mappings.GetUnmappedMappings();
            if (mappings.Count > 0)
            {
                string exception = String.Empty;
                mappings.ForEach(m => exception += m.Property + @" ; ");
                if (!String.IsNullOrEmpty(exception))
                    throw new ORMappingException(String.Format("The following properties were unmappable: {0}.", exception));            
            }
        }

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Performs object to relational mapping.
        /// </summary>
        /// <param name="mapping">A mapping.</param>
        /// <param name="db">A database against which the command will be executed.</param>
        /// <param name="cmd">A command being prepared for execution.</param>
        /// <param name="instance">An object being mapped to a DbCommand.</param>
        private static void MapO2R(
            ORMapping mapping,
            Database db,
            DbCommand cmd,
            object instance)
        {
            // Throw mapping exception if property information is not found.
            PropertyInfo pi = 
                instance.GetType().GetProperty(mapping.Property);
            if (pi == null)
                throw new ORMappingException(String.Format("Property {0} is unmappable.", mapping.Property));

            // Parse db parameter type (if necessary).
            if (!mapping.DbParameterTypeIsParsed)
                ParseDbParameterType(mapping, pi);

            // Get object value.
            object objectValue =
                GetObjectValue(mapping, instance, pi);

            // Assign relational value.
            db.AddInParameter(cmd, mapping.DbParameter, mapping.ParsedDbType, objectValue);        
        }

        /// <summary>
        /// Returns the mapping's db parameter type.
        /// </summary>
        /// <param name="mapping">The custom mapping being processed.</param>
        /// <param name="pi">The property information of the reflected type.</param>
        /// <returns>The mappings db paramter type.</returns>
        private static void ParseDbParameterType(
            ORMapping mapping, PropertyInfo pi)
        {
            // If necessary perform a parse over the parameter type.
            if ((!mapping.DbParameterTypeIsParsed) &&
                (mapping.DbParameterType == ORMappingDbParameterType.Undefined))
            {
                ORMappingDbParameterType derived = ORMappingDbParameterType.Undefined;
                if (typeof(int).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.Int32;
                else if (typeof(string).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.String;
                else if (typeof(bool).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.Boolean;
                else if (typeof(DateTime).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.DateTime;
                else if (typeof(double).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.Double;
                else if (typeof(byte).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.Byte;
                else if (typeof(Guid).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.Guid;
                else if (typeof(byte[]).Equals(pi.PropertyType))
                    derived = ORMappingDbParameterType.Binary;
                else if (typeof(Enum).Equals(pi.PropertyType.BaseType))
                    derived = ORMappingDbParameterType.Int32;
                else
                    derived = ORMappingDbParameterType.Object;
                mapping.DbParameterType = derived;
            }

            // Set the parameter db type.
            mapping.ParsedDbType = 
                (DbType)Enum.Parse(typeof(DbType), mapping.DbParameterType.ToString(), true);

            // Flag that the parse has been performed.
            mapping.DbParameterTypeIsParsed = true;
        }

        /// <summary>
        /// Performs relational to object mapping.
        /// </summary>
        /// <param name="mapping">A mapping.</param>
        /// <param name="dr">A data reader containing data for mapping.</param>
        /// <param name="instance">An object being mapped to a DbCommand.</param>
        private static void MapR2O(
            ORMapping mapping,
            IDataReader dr,
            object instance)
        {
            // Derive the property information via reflection.
            PropertyInfo pi =
                instance.GetType().GetProperty(mapping.Property);
            if (pi != null)
            {
                // Derive relational value.
                object relationalValue = 
                    GetRelationalValue(dr, mapping.DbColumn, mapping.NullValue);

                // Derive casted relational value.
                object castedValue = 
                    GetCastedRelationalValue(relationalValue, pi);

                // Set object value.
                if (castedValue != null)
                    pi.SetValue(instance, castedValue, null);                

                // Flag that the mapping occurred.
                mapping.WasMapped = true;
            }
        }

        /// <summary>
        /// Returns the derived relational value form the datareader.
        /// </summary>
        /// <param name="dr">A data reader containing data for mapping.</param>
        /// <param name="dbColumnName">The database column name (as defined within the config file).</param>
        /// <param name="nullValue">The null value (as defined within the config file).</param>
        /// <returns>The relational value.</returns>
        private static object GetObjectValue(
            ORMapping mapping,
            object instance,
            PropertyInfo pi)
        {
            object result;

            // Use reflection to get the object value.
            result =
                pi.GetValue(instance, null);

            // Ensure sql overflow safety.
            if (pi.PropertyType.Equals(typeof(DateTime)) &&
                (result != null))
            {
                DateTime dateTime = Convert.ToDateTime(result);
                if (dateTime.Equals(DateTime.MinValue))
                    result = null;
            }

            // Perform null switch (if applicable).
            if ((!String.IsNullOrEmpty(mapping.NullValue)) &&
                (result != null) &&
                (result.ToString().Trim().ToUpperInvariant().Equals(mapping.NullValue.Trim().ToUpperInvariant())))
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Returns the derived relational value form the datareader.
        /// </summary>
        /// <param name="dr">A data reader containing data for mapping.</param>
        /// <param name="dbColumnName">The database column name (as defined within the config file).</param>
        /// <param name="nullValue">The null value (as defined within the config file).</param>
        /// <returns>The relational value.</returns>
        private static object GetRelationalValue(
            IDataReader dr,
            string dbColumnName,
            string nullValue)
        {
            object result;
            
            // Pull value from the data reader.
            try
            {
                result = dr[dbColumnName];
            }
            catch
            {
                // Throw meaningful exception.
                throw new ORMappingException(String.Format("Column {0} does not exist within the datareader", dbColumnName));
            }

            // Perform null switch (if applicable).
            if ((result is System.DBNull) &&
                (!String.IsNullOrEmpty(nullValue)))
            {
                result = nullValue;
            }

            return result;
        }

        /// <summary>
        /// Casts the passed relational value according to the target property type.
        /// </summary>
        /// <param name="pi">The property info.</param>
        /// <param name="relationalValue">The relational value.</param>
        /// <returns>The casted object value.</returns>
        private static object GetCastedRelationalValue(
            object relationalValue,
            PropertyInfo pi)
        {
            // Return null if the relational value is null.
            if (relationalValue is System.DBNull)
                return null;

            // Perform Enum or Type conversion (as appropriate).            
            object result = null;
            if (pi.PropertyType.BaseType.Equals(typeof(System.Enum)))
            {
                result = 
                    Enum.Parse(pi.PropertyType, relationalValue.ToString(), true); ;
            }
            else
            {
                result =
                    Convert.ChangeType(relationalValue, pi.PropertyType);
            }
            return result;
        }

        #endregion Private methods
    }
}
