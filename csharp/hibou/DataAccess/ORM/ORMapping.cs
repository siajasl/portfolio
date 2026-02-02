using System;
using System.Data;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.DataAccess.ORM
{
    /// <summary>
    /// Represents an object to relational mapping.
    /// </summary>
    /// <remarks>This can be either deserialized from a config file or derived from code attributes.</remarks>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01", Name = "Mapping")]
    [Serializable]
    public class ORMapping : 
        ICloneable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the associated object property.
        /// </summary>
        [DataMember(IsRequired = true, Order = 0)]
        public string Property
        { get; set; }

        /// <summary>
        /// Gets or sets the name of the associated datareader column.
        /// </summary>
        [DataMember(IsRequired = false, Order = 1)]
        public string DbColumn
        { get; set; }

        /// <summary>
        /// Gets or sets the name of the associated database parameter.
        /// </summary>
        [DataMember(IsRequired = false, Order = 2)]
        public string DbParameter
        { get; set; }

        /// <summary>
        /// Gets or sets the type of associated database parameter.
        /// </summary>
        [DataMember(IsRequired = false, Order = 3)]
        public ORMappingDbParameterType DbParameterType
        { get; set; }

        /// <summary>
        /// Gets or sets the null value.
        /// </summary>
        [DataMember(IsRequired = false, Order = 4)]
        public string NullValue
        { get; set; }

        /// <summary>
        /// Gets or sets the parsed type of associated database parameter.
        /// </summary>
        /// <remarks>
        /// This maps to the ado.net DbType enumeration.
        /// </remarks>
        public DbType ParsedDbType
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the type of associated database parameter has been parsed.
        /// </summary>
        public bool DbParameterTypeIsParsed
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the mapping was successful.
        /// </summary>
        public bool WasMapped
        { get; set; }

        #endregion Properties

        #region ICloneable implementation

        /// <summary>
        /// Returns a clone.
        /// </summary>
        public object Clone()
        {
            return new ORMapping() 
            {
                DbColumn = DbColumn,
                DbParameter = DbParameter,
                DbParameterType = DbParameterType,
                DbParameterTypeIsParsed = DbParameterTypeIsParsed,
                NullValue = NullValue,
                ParsedDbType = ParsedDbType,
                Property = Property,
                WasMapped = WasMapped
            };
        }

        #endregion ICloneable implementation

        #region Factory

        /// <summary>
        /// Factory method to return an instance.
        /// </summary>
        /// <param name="property">The name of the mapping property.</param>
        /// <param name="dbColumn">The name of the mapping db column.</param>
        /// <returns>An OR mapping.</returns>
        public static ORMapping Create(string property)
        {
            // Defensive programming.
            ORMapping result = new ORMapping()
            {
                DbColumn = property,
                Property = property
            };
            return result;
        }

        /// <summary>
        /// Factory method to return an instance.
        /// </summary>
        /// <param name="property">The name of the mapping property.</param>
        /// <param name="dbColumn">The name of the mapping db column.</param>
        /// <returns>An OR mapping.</returns>
        public static ORMapping Create(string property, string dbColumn)
        {
            // Defensive programming.
            ORMapping result = new ORMapping() 
            {
                DbColumn = dbColumn,
                Property = property
            };
            return result;
        }

        #endregion Factory
    }
}