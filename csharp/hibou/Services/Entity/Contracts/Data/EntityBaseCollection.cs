using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.DataContract;
using Keane.CH.Framework.Core.Utilities.Factory;

namespace Keane.CH.Framework.Services.Entity.Contracts.Data
{
    /// <summary>
    /// Represents a list of entities of some type or other.
    /// </summary>
    /// <typeparam name="E">The type of entity being managed within the list.</typeparam>
    [CollectionDataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public class EntityBaseCollection<E> : 
        List<E>,
        IEntityBaseCollection,
        IExtensibleDataObject,
        ICloneable,
        IStateInitialiser
        where E : EntityBase, new()
    {
        #region Ctor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntityBaseCollection()
        {
            InitialiseState();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="capacity">The list capacity.</param>
        public EntityBaseCollection(int capacity)
            : base(capacity)
        {
            InitialiseState();
        }

        #endregion Ctor

        #region IStateInitialiser Members

        /// <summary>
        /// State initialisation routine.
        /// </summary>
        public virtual void InitialiseState()
        { }

        #endregion IStateInitialiser Members

        #region IExtensibleDataObject Members
               
        /// <summary>
        /// Gets or sets the deserialization context data.
        /// </summary>
        public ExtensionDataObject ExtensionData
        {
            get { return this.extensionDataField; }
            set { this.extensionDataField = value; }
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
        
        #region Public methods

        /// <summary>
        /// Returns whether there is an entity matched by id.
        /// </summary>
        /// <param name="entityId">The id of the entity being sought.</param>
        /// <returns>True if an entity with a matching id is found.</returns>
        public bool IsMatchById(int entityId)
        {
            return (MatchById(entityId) != null);
        }

        /// <summary>
        /// Returns the first entity with a matching id within the managed collection.
        /// </summary>
        /// <param name="entityId">The id of the entity being sought.</param>
        /// <returns>An entity (if found).</returns>
        public E MatchById(int entityId)
        {
            return this.FirstOrDefault(e => e.Id == entityId); 
        }

        /// <summary>
        /// Adds an item to the underlying collection.
        /// </summary>
        /// <param name="entity">The entity to add to the collection.</param>
        public new void Add(E entity)
        {
            Remove(entity);
            base.Add(entity);
        }

        /// <summary>
        /// Removes an item from the underlying collection.
        /// </summary>
        /// <param name="entity">The entity to remove from the collection.</param>
        public new void Remove(E entity)
        {
            if (IsMatchById(entity.Id))
                base.Remove(entity);
        }

        /// <summary>
        /// Removes an item from the list by id.
        /// </summary>
        /// <param name="entityId">The id of the entity being removed from the collection.</param>
        public void Remove(int entityId)
        {
            E item = MatchById(entityId);
            if (item != null)
                base.Remove(item);
        }

        /// <summary>
        /// Updates the collection by either inserting or overwriting the passed instance.
        /// </summary>
        /// <param name="entity"></param>
        public void Update(E entity)
        {
            Remove(entity.Id);
            base.Add(entity);
        }

        #endregion Public methods
    }

    /// <summary>
    /// Marker interface for overcoming the covariance problem in single inheritance object orientated programming languages
    /// </summary>
    internal interface IEntityBaseCollection 
    {  }
}
