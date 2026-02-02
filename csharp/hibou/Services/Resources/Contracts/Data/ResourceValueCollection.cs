using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Resources.Contracts
{
    /// <summary>
    /// Repersents a collection of resource values associated with a resource.
    /// </summary>
    /// <typeparam name="T">The type of resource being managed.</typeparam>
    [CollectionDataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Resources")]
    [Serializable]
    public class ResourceValueCollection<T>
    {
        #region Constructor

        public ResourceValueCollection()
        {
            Values = new List<ResourceValue<T>>();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the managed list of resource values.
        /// </summary>
        private List<ResourceValue<T>> Values
        { get; set; }

        /// <summary>
        /// Gets a flag indicating whether there are cached values.
        /// </summary>
        public bool HasValues
        {
            get { return ((Values != null) && (Values.Count > 0)); }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the default value. 
        /// </summary>
        /// <returns>The default value.</returns>
        public T GetDefaultValue()
        {
            T result = default(T);
            if (HasValues)
                result = Values[0].Value;
            return result;
        }

        /// <summary>
        /// Gets the value associated with the passed culture id. 
        /// </summary>
        /// <param name="cultureId">The supported culture id.</param>
        /// <returns>The resource value associated with the passed culture id.</returns>
        public T GetValue(int cultureId)
        {
            T result = default(T);
            ResourceValue<T> resourceValue = 
                Values.FirstOrDefault(v => v.CultureId.Equals(cultureId));
            if (resourceValue != null)
                result = resourceValue.Value;
            return result;
        }

        /// <summary>
        /// Adds a value to the managed collection.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="cultureId">The supported culture id.</param>
        public void AddValue(T value, int cultureId)
        {
            RemoveValue(cultureId);
            ResourceValue<T> resourceValue = new ResourceValue<T>() 
            {
                Value = value,
                CultureId = cultureId 
            };
            Values.Add(resourceValue);
        }

        /// <summary>
        /// Removes a value from the managed collection.
        /// </summary>
        /// <param name="cultureId">The supported culture id.</param>
        public void RemoveValue(int cultureId)
        {
            ResourceValue<T> resourceValue =
                Values.FirstOrDefault(v => v.CultureId.Equals(cultureId));
            if (resourceValue != null)
                Values.Remove(resourceValue);
        }

        #endregion Methods
    }
}
