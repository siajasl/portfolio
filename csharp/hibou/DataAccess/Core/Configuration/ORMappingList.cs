using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.ExtensionMethods;

namespace Keane.CH.Framework.DataAccess.Core.Configuration
{
    /// <summary>
    /// Represents a managed list of mappings.
    /// </summary>
    [CollectionDataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class ORMappingList : 
        List<ORMapping>, 
        ICloneable
    {
        #region Static methods

        #region Factory methods

        /// <summary>
        /// Returns a filtered mapping list derived from the passed type.
        /// </summary>
        /// <remarks>
        /// 1.  Only public instance properties require mapping.
        /// 2.  The passed mapping list acts as the filter control.
        /// </remarks>
        /// <param name="mappingList">The existing mapping list.</param>
        /// <param name="type">The type from which the mapping list is to be derived.</param>
        /// <returns>A mapping list.</returns>
        public static ORMappingList CreateFromType(
            ORMappingList mappingList,
            Type type)
        {
            ORMappingList result = new ORMappingList();
            var typeProperties =
                from typeProperty in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where (!mappingList.MappingExists(typeProperty.Name))
                select typeProperty;
            typeProperties.ToList().ForEach(pi => result.Add(pi));
            return result;
        }

        /// <summary>
        /// Returns a mapping list derived from the passed type.
        /// </summary>
        /// <remarks>
        /// Only public instance properties require mapping.
        /// </remarks>
        /// <param name="type">The type from which the mapping list is to be derived.</param>
        /// <returns>A mapping list.</returns>
        public static ORMappingList CreateFromType(
            Type type)
        {
            ORMappingList result = new ORMappingList();
            var typeProperties =
                from typeProperty in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                select typeProperty;
            typeProperties.ToList().ForEach(pi => result.Add(pi));
            return result;
        }

        #endregion Factory methods

        #endregion Static methods

        #region Instance methods

        #region Filter methods

        /// <summary>
        /// Filters the mapping list in readiness for an object to relational mapping operation.
        /// </summary>
        /// <returns>The mapping list for an object to relational mapping operation.</returns>
        internal ORMappingList FilterForO2R()
        {
            var mappings =
                from mapping in this
                where (!String.IsNullOrEmpty(mapping.DbParameter))
                select mapping;
            return mappings.AsCustomCollection<ORMappingList, ORMapping>();
        }

        /// <summary>
        /// Filters the mapping list in readiness for an relational to object mapping operation.
        /// </summary>
        /// <returns>The mapping list for an relational to object mapping operation.</returns>
        internal ORMappingList FilterForR2O()
        {
            var mappings =
                from mapping in this
                where (!String.IsNullOrEmpty(mapping.DbColumn))
                select mapping;
            return mappings.AsCustomCollection<ORMappingList, ORMapping>();
        }

        #endregion Filter methods

        #region ICloneable implementation

        /// <summary>
        /// Returns a clone.
        /// </summary>
        public object Clone()
        {            
            ORMappingList clone = new ORMappingList();
            base.ForEach(m => clone.Add((ORMapping)m.Clone()));
            return clone;
        }

        /// <summary>
        /// Returns a clone.
        /// </summary>
        public ORMappingList Clone(bool resetWasMappedFlag)
        {
            ORMappingList clone = (ORMappingList)this.Clone();
            if (resetWasMappedFlag)
                clone.ResetWasMappedFlag();
            return clone;
        }

        #endregion ICloneable implementation

        /// <summary>
        /// Merges the passed mapping list with this one.
        /// </summary>
        /// <param name="mappingList">The mapping list with which to merge.</param>
        internal void Merge(ORMappingList mappingList)
        {
            if (mappingList != null)
            {
                foreach (ORMapping mapping in mappingList)
                {
                    ORMapping clone = (ORMapping)mapping.Clone();
                    Add(clone);
                }
            }
        }

        /// <summary>
        /// Resets the was mapped flag.
        /// </summary>
        internal void ResetWasMappedFlag()
        {
            base.ForEach(m => m.WasMapped = false);
        }
        
        /// <summary>
        /// Returns the numder of mappings that match the passed property name.
        /// </summary>
        /// <param name="propertyName">The name of the property against which to perform the mapping search.</param>
        /// <returns>The number of matched mappings.</returns>
        public int MappingCount(string propertyName)
        {
            return this.Count(m => 
                String.Equals(propertyName, m.Property, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Returns a flag indicating whether there is a mapping for the passed property.
        /// </summary>
        /// <param name="propertyName">The name of the property against which to perform the mapping search.</param>
        /// <returns>True if there is a corresponding mapping.</returns>
        internal bool MappingExists(string propertyName)
        {
            return MappingCount(propertyName) > 0;
        }

        /// <summary>
        /// Returns the first mapping that matches the passed property name.
        /// </summary>
        /// <param name="propertyName">The name of the property against which to perform the mapping search.</param>
        /// <returns>A mapping.</returns>
        public ORMapping GetMapping(string propertyName)
        {
            return
                this.FirstOrDefault(m => String.Equals(m.Property, propertyName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Adds the passed property to the collection.
        /// </summary>
        /// <param name="pi">The reflection property information from whic to derive & add a mapping.</param>
        private void Add(PropertyInfo pi)
        {
            // Abort if the execution criteria are not met.
            if (pi == null)
                throw new ArgumentNullException("pi must not be null.");
            if (MappingExists(pi.Name))
                throw new ArgumentException(String.Format("There is already a mapping for property {0}.", pi.Name));

            // Add to the collection.
            ORMapping mapping = new ORMapping()
            {
                DbColumn = pi.Name,
                DbParameter = pi.Name,
                Property = pi.Name
            };
            Add(mapping);
        }

        /// <summary>
        /// Adds the passed mapping to the collection.
        /// </summary>
        /// <param name="mapping">The mapping to be added to the collection.</param>
        public new void Add(ORMapping mapping)
        {
            // Abort if the execution criteria are not met.
            if (mapping == null)
                throw new ArgumentNullException("mapping must not be null.");
            if (MappingExists(mapping.Property))
                throw new ArgumentException(String.Format("There is already a mapping for property {0}.", mapping.Property));

            // Add to the collection.
            base.Add(mapping);
        }

        /// <summary>
        /// Returns the collection of unmapped mappings.
        /// </summary>
        /// <returns>A collection of unmapped mappings.</returns>
        internal ORMappingList GetUnmappedMappings()
        {
            var mappings =
                from mapping in this
                where (!mapping.WasMapped)
                select mapping;
            return mappings.AsCustomCollection<ORMappingList, ORMapping>();
        }

        #endregion Instance methods
    }
}