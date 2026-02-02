using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data
{
    /// <summary>
    /// Enumeration over the supported protected entity modification types.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public enum ProtectedEntityModificationType
    {
        /// <summary>
        /// The user has requested a delete modification.
        /// </summary>
        [EnumMember()]
        Delete = 1,

        /// <summary>
        /// The user has requested an update modification.
        /// </summary>
        [EnumMember()]
        Update = 2
    }
}