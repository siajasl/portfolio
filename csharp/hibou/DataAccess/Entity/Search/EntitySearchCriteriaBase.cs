using System.Runtime.Serialization;
using System;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Search.Contracts.Data
{
    /// <summary>
    /// Abstract class inherited by entity search criteria classes.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Search")]
    [Serializable]
    public abstract class EntitySearchCriteriaBase :
        SearchCriteriaBase 
    {
        #region Properties

        /// <summary>
        /// Gets or sets the entity state search criteria.
        /// </summary>
        [DataMember()]
        public EntityState EntityState 
        { get; set; }

        #endregion Properties

        #region SearchCriteriaBase overrides

        /// <summary>
        /// Member initialisation routine.
        /// </summary>
        protected override void InitialiseMembers() 
        {
            base.InitialiseMembers();
            EntityState = EntityState.Active;
        }

        #endregion SearchCriteriaBase overrides
    }
}