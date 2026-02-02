using System;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data
{
    /// <summary>
    /// Enumeration over the modification decision types.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public enum AjudicationDecisionType
    {
        /// <summary>
        /// The initial decision state.
        /// </summary>
        [EnumMember()]
        Undecided  = 1,

        /// <summary>
        /// It was decided to accept the modification.
        /// </summary>
        [EnumMember()]
        Accept = 2,

        /// <summary>
        /// It was decided to reject the modification.
        /// </summary>
        [EnumMember()]
        Reject = 3
    }
}