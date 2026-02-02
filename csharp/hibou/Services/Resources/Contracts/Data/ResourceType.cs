using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Resources.Contracts
{
    /// <summary>
    /// Enumeration over supported resource types.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Resources")]
    [Serializable]
    public enum ResourceType
    {
        /// <summary>
        /// A string resource.
        /// </summary>
        [EnumMember()]
        String,

        /// <summary>
        /// A binary resource.
        /// </summary>
        [EnumMember()]
        Binary,

        /// <summary>
        /// An xml resource.
        /// </summary>
        [EnumMember()]
        Xml
    }
}
