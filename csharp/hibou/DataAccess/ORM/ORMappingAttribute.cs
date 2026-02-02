using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Keane.CH.Framework.Core.ExtensionMethods;

namespace Keane.CH.Framework.DataAccess.ORM
{
    /// <summary>
    /// An attribute used to define an OR mapping information.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ORMappingAttribute : Attribute
    {
        #region Constructor

        public ORMappingAttribute()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected void InitialiseMembers()
        {
            this.DbColumn = string.Empty;
            this.DbColumnFormat = ORMappingAttributeFormatType.None;
            this.DbParameter = string.Empty;
            this.DbParameterFormat = ORMappingAttributeFormatType.UpperCaseUnderscore;
            this.NullValue = string.Empty;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the name of the associated db column.
        /// </summary>
        public string DbColumn
        { get; set; }

        /// <summary>
        /// Gets or sets the format of the associated db column.
        /// </summary>
        /// <remarks>
        /// This is used when auto-generating column names.
        /// </remarks>
        public ORMappingAttributeFormatType DbColumnFormat
        { get; set; }

        /// <summary>
        /// Gets or sets the name of the associated db parameter.
        /// </summary>
        public string DbParameter
        { get; set; }

        /// <summary>
        /// Gets or sets the format of the associated db parameter type.
        /// </summary>
        /// <remarks>
        /// This is used when auto-generating parameter names.
        /// </remarks>
        public ORMappingAttributeFormatType DbParameterFormat
        { get; set; }

        /// <summary>
        /// Gets or sets a value to substitute in place of null.
        /// </summary>
        public string NullValue
        { get; set; }

        /// <summary>
        /// Gets the property info against which the atribute was defined.
        /// </summary>
        public PropertyInfo PropertyInfo
        { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialises the instance with reflected property information.
        /// </summary>
        /// <param name="pi">Reflected property information.</param>
        internal void Initialise(PropertyInfo propertyInfo)
        {
            // Defensive programing.
            if (propertyInfo == null)
                throw new ArgumentNullException("propertyInfo");

            // Cache for later use.
            this.PropertyInfo = propertyInfo;

            // Db column (derive default value if unspecified).
            if (!string.IsNullOrEmpty(this.DbColumn))
                this.DbColumn = this.DbColumn.Trim();
            else
                this.DbColumn = ApplyFormat(propertyInfo.Name, this.DbColumnFormat);

            // Db parameter (derive default value if unspecified).
            if (!string.IsNullOrEmpty(this.DbParameter))
                this.DbParameter = this.DbParameter.Trim();
            else
                this.DbParameter = ApplyFormat(propertyInfo.Name, this.DbParameterFormat);
        }

        /// <summary>
        /// Applies the relevant string format.
        /// </summary>
        /// <param name="stringToFormat">The string being formatted.</param>
        /// <param name="formatType">The mapping format type.</param>
        /// <returns>A formatted string.</returns>
        private string ApplyFormat(
            string stringToFormat,
            ORMappingAttributeFormatType formatType)
        {
            string result = stringToFormat;
            switch (formatType)
            {
                case ORMappingAttributeFormatType.UpperCaseUnderscore:
                    result = stringToFormat.ToUpperCaseUnderscore();
                    break;
                case ORMappingAttributeFormatType.UpperCase:
                    result = stringToFormat.ToUpperInvariant();
                    break;
                default:
                    break;
            }
            return result.Trim();
        }

        /// <summary>
        /// Converts to an instance as an OR mapping.
        /// </summary>
        /// <returns>The converted or mapping.</returns>
        public ORMapping AsORMapping()
        {
            ORMapping result = new ORMapping();
            result.DbColumn = this.DbColumn;
            result.DbParameter = this.DbParameter;
            result.NullValue = this.NullValue;
            result.Property = this.PropertyInfo.Name;
            return result;
        }

        #endregion Methods
    }
}
