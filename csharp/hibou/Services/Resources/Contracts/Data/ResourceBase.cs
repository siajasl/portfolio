using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Resources.Contracts
{
    /// <summary>
    /// Base class for all resources.
    /// </summary>
    /// <typeparam name="T">The type of resource.</typeparam>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Resources")]
    [Serializable]
    public abstract class ResourceBase<T> : 
        EntityBase
    {
        #region Properties

        /// <summary>
        /// Gets the resource category.
        /// </summary>
        [DataMember()]
        public string Category
        { get; set; }

        /// <summary>
        /// Gets the resource sub-category.
        /// </summary>
        [DataMember()]
        public string SubCategory
        { get; set; }

        /// <summary>
        /// Gets or sets the list of resource values.
        /// </summary>
        [DataMember()]
        public ResourceValueCollection<T> Values 
        { get; set; }

        /// <summary>
        /// Gets the resource type.
        /// </summary>
        public abstract ResourceType ResourceType
        { get; }
        
        /// <summary>
        /// Gets the default value (i.e. the first resource in the managed collection).
        /// </summary>
        public T DefaultValue
        {
            get 
            {
                T result = default(T);
                if ((Values != null) && (Values.HasValues))
                    result = Values.GetDefaultValue();
                return result;
            }        
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Intialise the value for each supported culture.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureId">The count of supported cultures.</param>
        public void InitialiseValue(T value, uint cultureCount)
        {
            if (cultureCount == 0)
                throw new ArgumentException("cultureCount must be > 0");
            for (int i = 1; i < cultureCount; i++)
            {
                AddValue(value, i);
            }
        }

        /// <summary>
        /// Adds a value to the managed collection.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureId">The supported culture id.</param>
        public void AddValue(T value, int cultureId)
        {
            Values.AddValue(value, cultureId);
        }

        /// <summary>
        /// Gets the value associated with the passed culture id. 
        /// </summary>
        /// <param name="cultureId">The supported culture id.</param>
        /// <returns>The resource value associated with the passed culture id.</returns>
        public T GetValue(int cultureId)
        {
            T result = default(T);
            T resourceValue = Values.GetValue(cultureId);
            if (resourceValue != null)
                result = resourceValue;
            return result;
        }

        #endregion Methods
    }
}
