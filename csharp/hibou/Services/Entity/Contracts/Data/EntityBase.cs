using System;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.Factory;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System.Xml;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data
{
    /// <summary>
    /// Base class inherited by all entities.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public abstract class EntityBase :
        IExtensibleDataObject,
        ICloneable,
        IStateInitialiser
    {
        #region Ctor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntityBase() 
        {
            InitialiseState();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        public EntityBase(int id)
            : this()
        {
            EntityInfo.EntityId = id;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        /// <remarks>
        /// This is delegated to the entity information.
        /// </remarks>
        public int Id
        {
            get { return EntityInfo.EntityId; }
            set { EntityInfo.EntityId = value; }
        }

        /// <summary>
        /// Gets or sets the entity information.
        /// </summary>
        [DataMember()]
        public EntityInfo EntityInfo
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Event fired when the entity id changes (i.e. typically upon insertion).
        /// </summary>
        protected virtual void OnIdChange() 
        { }

        /// <summary>
        /// Serializes the entity according to the target serialization type.
        /// </summary>
        /// <param name="serializationType">The target serialization type.</param>
        /// <returns>The serialized entity.</returns>
        public object Serialize(SerializationType serializationType)
        {
            object asObject = (object)this;
            object result = 
                SerializationUtility.Serialize(serializationType, asObject);
            return result;
        }

        #endregion Methods

        #region Event handlers

        /// <summary>
        /// Event handler listening for entity id events.
        /// </summary>
        /// <param name="sender">The event control.</param>
        /// <param name="args">The vent args.</param>
        private void OnIdChangedEventHandler(object sender, EventArgs args)
        {
            OnIdChange();
        }

        #endregion Event handlers

        #region IExtensibleDataObject Members

        /// <summary>
        /// Gets or sets the deserialization context data.
        /// </summary>
        public ExtensionDataObject ExtensionData
        { 
            get{ return this.extensionDataField; }
            set{ this.extensionDataField = value; }
        }
        [NonSerialized]
        private ExtensionDataObject extensionDataField;

        #endregion IExtensibleDataObject Members

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A cloned instance.</returns>
        public object Clone()
        {
            return CloneUtility.CloneViaText(this);
        }

        #endregion ICloneable Members

        #region IStateInitialiser Members

        /// <summary>
        /// State initialisation routine.
        /// </summary>
        public virtual void InitialiseState()
        {
            EntityInfo = new EntityInfo();
            EntityInfo.OnIdChange += OnIdChangedEventHandler;        
        }

        #endregion
    }
}