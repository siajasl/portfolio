using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Resources.Contracts
{
    /// <summary>
    /// Represents a resource value in a spcific culture.
    /// </summary>
    /// <typeparam name="T">The type of resource bsing managed.</typeparam>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Resources")]
    [Serializable]
    public class ResourceValue<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the actual value.
        /// </summary>
        [DataMember()]
        public T Value
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the associated culture.
        /// </summary>
        [DataMember()]
        public int CultureId 
        { get; set; }

        #endregion Properties
    }
}
