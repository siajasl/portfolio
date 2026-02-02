using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Keane.CH.Framework.Core.Utility.Reflection;

namespace Keane.CH.Framework.DataAccess.ORM
{
    /// <summary>
    /// A class level ORM related attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ORMappingContextAttribute : Attribute
    {
        #region Constructor

        public ORMappingContextAttribute()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected void InitialiseMembers()
        {
            DbCommandFormat = "usp{0}EntityOperations";
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Get or sets the name of the associated db command.
        /// </summary>
        public string DbCommand
        { get; set; }

        /// <summary>
        /// Get or sets the name of the default db command format used to auto-generate the dB command.
        /// </summary>
        public string DbCommandFormat
        { get; set; }

        /// <summary>
        /// Gets or sets the associated class's type info.
        /// </summary>
        internal Type TypeInfo
        { get; set; }

        /// <summary>
        /// Gets or sets the associated collection of mapping attributes.
        /// </summary>
        private List<ORMappingAttribute> MappingAttributes
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialises the instance with the associated reflected type information.
        /// </summary>
        /// <param name="typeInfo">The associated reflacted type information.</param>
        internal void Initialise(Type typeInfo)
        {
            // Defensive programing.
            if (typeInfo == null)
                throw new ArgumentNullException("typeInfo");

            // Cache for later use.
            this.TypeInfo = typeInfo;

            // Derive command (if unspecified).
            if (string.IsNullOrEmpty(this.DbCommand) &&
                !string.IsNullOrEmpty(this.DbCommandFormat))
            {
                this.DbCommand =
                    string.Format(this.DbCommandFormat, typeInfo.Name);
            }

            // Initialise associated mapping attributes.
            PropertyInfo[] properties =
                typeInfo.GetPropertiesSupportingAttribute<ORMappingAttribute>(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in properties)
            {
                ORMappingAttribute mappingAttribute = pi.GetAttribute<ORMappingAttribute>();
                mappingAttribute.Initialise(pi);
                this.MappingAttributes.Add(mappingAttribute);
            }
        }

        #endregion Methods
    }
}