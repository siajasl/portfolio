using Keane.CH.Framework.Core.Entity;
using System;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Core.Entity
{
    /// <summary>
    /// Entity dao event args.
    /// </summary>
    /// <typeparam name="E">The type of entity being processed.</typeparam>
    public class EntityEventArgs :  
        EventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityEventArgs()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityEventArgs(int instanceId)
        {
            this.InstanceId = instanceId;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityEventArgs(EntityBase instance)
        {
            this.Instance = instance;
            if (instance != null)
                this.InstanceId = instance.Id;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityEventArgs(IEnumerable<EntityBase> collection)
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
