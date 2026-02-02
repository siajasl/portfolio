using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Core.Utilities.DataContract
{
    /// <summary>
    /// Enumeration over the types of data contract serialiation target.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    public enum SerializationType
    {
        [EnumMember()]
        Json,
        [EnumMember()]
        XmlDoc,
        [EnumMember()]
        XmlString
    }
}