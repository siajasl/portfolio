using System;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Entity
{
    /// <summary>
    /// Entity dao event args.
    /// </summary>
    /// <typeparam name="E">The type of entity being processed.</typeparam>
    public class EntityPresenterEventArgs :  
        EventArgs
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityPresenterEventArgs()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityPresenterEventArgs(EntityBase entity)
        {
            this.Entity = entity;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// The instance being processed.
        /// </summary>
        public EntityBase Entity 
        { get; set; }

        #endregion Properties
    }
}
