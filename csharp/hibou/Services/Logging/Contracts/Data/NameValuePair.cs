using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Represents a name value pair.
    /// </summary>
    /// <typeparam name="T">The type of value.</typeparam>
    /// <remarks>
    /// Necessary due to the fact that dictionaries cannot be deserialized by WCF.
    /// </remarks>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public class NameValuePair<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember()]
        public string Name
        { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DataMember()]
        public T Value
        { get; set; }

        #endregion Properties
    }
}
