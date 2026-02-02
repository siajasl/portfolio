using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data
{
    /// <summary>
    /// Enumeration over the array of possible entity states.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public enum EntityState 
    {
        /// <summary>
        /// The unspecificed & default state.
        /// </summary>
        [EnumMember()]
        Unspecified = 999,

        /// <summary>
        /// The entity has been instantiated but is not officially active within the system.
        /// </summary>
        [EnumMember()]
        ActivePending = 0,

        /// <summary>
        /// The entity is active withinthe system.
        /// </summary>
        [EnumMember()]
        Active = 1,

        /// <summary>
        /// Dectivation pending.
        /// </summary>
        [EnumMember()]
        InActivePending = 9,

        /// <summary>
        /// The entity is no longer active within the system.
        /// </summary>
        /// <remarks>
        /// It may still exist for archinving purposes.
        /// </remarks>
        [EnumMember()]
        InActive = 99
    }
}