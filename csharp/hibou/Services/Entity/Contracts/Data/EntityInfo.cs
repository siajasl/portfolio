using System;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.ExtensionMethods;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data
{
    /// <summary>
    /// Encapsulates default information pertaining to an entity.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public class EntityInfo 
    {
        #region Ctor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntityInfo()
        {
            InitialiseMembers();
        }

        #endregion Ctor

        #region Fields

        private int idField;

        #endregion Fields

        #region Properties

        /// <summary>
        /// The Id of the entity (typically from a database primary key).
        /// </summary>
        [DataMember()]
        public Int32 EntityId
        {
            get 
            {
                return idField;
            }
            set 
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                if (idField != value)
                {
                    idField = value;
                    if (OnIdChange != null)
                        OnIdChange(this, new EventArgs());                
                }
            } 
        }

        /// <summary>
        /// Gets a flag indicating whether the entity is new.
        /// </summary>
        public Boolean IsNew
        { 
            get 
            {
                return (EntityId == 0) || (EntityId == int.MaxValue); 
            } 
        }

        /// <summary>
        /// Gets or set the entity type id.
        /// </summary>
        /// <remarks>
        /// Used during protected entity dao operations.
        /// </remarks>
        [DataMember]
        public int EntityTypeId
        { get; set; }

        /// <summary>
        /// Gets or set the entity version.
        /// </summary>
        [DataMember]
        public int EntityVersion
        { get; set; }

        /// <summary>
        /// Gets or set the entity state.
        /// </summary>
        [DataMember]
        public EntityState EntityState
        { get; set; }

        /// <summary>
        /// Gets or set the entity state as a byte.
        /// </summary>
        /// <remarks>This is to simplify enumeration casting.</remarks>
        public byte EntityStateAsByte
        {
            get 
            {
                return (byte)EntityState;
            }
            set 
            {
                EntityState = (EntityState)value;
            } 
        }

        /// <summary>
        /// The date when the entity was created.
        /// </summary>
        [DataMember]
        public DateTime EntityCreateDate
        { get; set; }

        /// <summary>
        /// The user when the entity was created.
        /// </summary>
        [DataMember]
        public string EntityCreateUser
        { get; set; }

        /// <summary>
        /// The date when the entity was last updated.
        /// </summary>
        [DataMember]
        public DateTime EntityUpdateDate
        { get; set; }

        /// <summary>
        /// The user when the entity was last updated.
        /// </summary>
        [DataMember]
        public string EntityUpdateUser
        { get; set; }

        #endregion Properties

        #region Events

        /// <summary>
        /// The event handler invoked when the entity id has changed.
        /// </summary>
        internal event EventHandler OnIdChange;

        #endregion Events

        #region Member initialization

        /// <summary>
        /// Collaborator instantiation event.
        /// </summary>
        private void InitialiseMembers()
        {
            EntityId = 0;
            EntityVersion = 0;
            EntityState = EntityState.Active;
            EntityCreateDate = DateTime.Now.PrecisionSafe();
            EntityCreateUser = Environment.UserName;
            EntityUpdateDate = DateTime.Now.PrecisionSafe();
            EntityUpdateUser = Environment.UserName;
        }

        #endregion Member initialization
    }
}