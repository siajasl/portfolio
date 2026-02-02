using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.DataAccess.Entity.Configuration
{
    /// <summary>
    /// The standard parameters used during execution of the entity stored procedure.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class EntityDaoStandardParameters
    {
        #region Properties

        /// <summary>
        /// Gets or sets the operation date parameter name.
        /// </summary>
        [DataMember(IsRequired = false, Order = 1)]
        public string OperationDate
        { get; set; }

        /// <summary>
        /// Gets or sets the operation entity id parameter name.
        /// </summary>
        [DataMember(IsRequired = false, Order = 2)]
        public string OperationEntityId
        { get; set; }

        /// <summary>
        /// Gets or sets the operation entity state parameter name.
        /// </summary>
        [DataMember(IsRequired = false, Order = 3)]
        public string OperationEntityState
        { get; set; }

        /// <summary>
        /// Gets or sets the operation entity version parameter name.
        /// </summary>
        [DataMember(IsRequired = false, Order = 4)]
        public string OperationEntityVersion
        { get; set; }

        /// <summary>
        /// Gets or sets the operation result parameter name.
        /// </summary>
        [DataMember(IsRequired = false, Order = 5)]
        public string OperationResult
        { get; set; }

        /// <summary>
        /// Gets or sets the operation type parameter name.
        /// </summary>
        [DataMember(IsRequired = false, Order = 6)]
        public string OperationType
        { get; set; }

        /// <summary>
        /// Gets or sets the operation user parameter name.
        /// </summary>
        [DataMember(IsRequired = false, Order = 7)]
        public string OperationUser
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Merges the passed standard parameter list with this one.
        /// </summary>
        /// <param name="mappingList">The mapping list with which to merge.</param>
        internal void Merge(EntityDaoStandardParameters standardParameterList)
        {
            if (standardParameterList != null)
            {
                if (!String.IsNullOrEmpty(standardParameterList.OperationDate))
                    OperationDate = standardParameterList.OperationDate;
                if (!String.IsNullOrEmpty(standardParameterList.OperationEntityId))
                    OperationEntityId = standardParameterList.OperationEntityId;
                if (!String.IsNullOrEmpty(standardParameterList.OperationEntityState))
                    OperationEntityState = standardParameterList.OperationEntityState;
                if (!String.IsNullOrEmpty(standardParameterList.OperationEntityVersion))
                    OperationEntityVersion = standardParameterList.OperationEntityVersion;
                if (!String.IsNullOrEmpty(standardParameterList.OperationResult))
                    OperationResult = standardParameterList.OperationResult;
                if (!String.IsNullOrEmpty(standardParameterList.OperationType))
                    OperationType = standardParameterList.OperationType;
                if (!String.IsNullOrEmpty(standardParameterList.OperationUser))
                    OperationUser = standardParameterList.OperationUser;
            }
        }

        #endregion Methods
    }
}
