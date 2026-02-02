using System;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Entity dao event args.
    /// </summary>
    /// <typeparam name="E">The type of entity being processed.</typeparam>
    public class EntityDaoEventArgs :  
        EventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityDaoEventArgs()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityDaoEventArgs(int instanceId)
        {
            this.InstanceId = instanceId;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityDaoEventArgs(EntityBase instance)
        {
            this.Instance = instance;
            if (instance != null)
                this.InstanceId = instance.Id;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityDaoEventArgs(IEnumerable<EntityBase> collection)
        {
            this.Collection = collection;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// The instance being processed.
        /// </summary>
        public EntityBase Instance 
        { get; set; }

        /// <summary>
        /// The id of the instance being processed.
        /// </summary>
        public int InstanceId
        { get; set; }

        /// <summary>
        /// The entity collection being processed.
        /// </summary>
        public IEnumerable<EntityBase> Collection
        { get; set; }

        #endregion Properties
    }
}
