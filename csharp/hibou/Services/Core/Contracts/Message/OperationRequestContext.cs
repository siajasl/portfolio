using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Core.Operation
{
    /// <summary>
    /// Encapsulates standard request context information.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public class OperationRequestContext
    {
        #region Properties

        /// <summary>
        /// Gets or sets the request originator user.
        /// </summary>
        [DataMember()]
        public string UserName
        { get; set; }

        /// <summary>
        /// Gets or sets the originator user id.
        /// </summary>
        [DataMember()]
        public int UserId
        { get; set; }

        /// <summary>
        /// Gets or sets the current user role type id list.
        /// </summary>
        public int[] UserRoleTypeIds
        { get; set; }

        /// <summary>
        /// Gets or sets the request originator culture.
        /// </summary>
        [DataMember()]
        public int CultureId { get; set; }

        #endregion Properties
    }
}