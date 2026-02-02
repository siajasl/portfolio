using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.Core.Entity;

namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Manages instantiation & configuration of entity dao abstract pointers.
    /// </summary>
    public sealed class IEntityDaoFactory
    {
        #region Factory methods

        /// <summary>
        /// Instantiates & returns an entity dao interface.
        /// </summary>
        /// <param name="configSet">An entity dao configuration set.</param>
        /// <returns>A configured entity dao interface.</returns>
        public static IEntityDao<E> Create<E>(
            EntityDaoConfigurationSet configSet)
            where E : EntityBase, new()
        {
            // Defensive programming.
            if (configSet == null)
                throw new ArgumentNullException("configSet");

            // Instantiate concrete instance.
            EntityDao<E> concreteInstance = new EntityDao<E>();

            // Configure.
            concreteInstance.Configure(configSet);

            // Return abstract pointer.
            return (IEntityDao<E>)concreteInstance;
        }

        #endregion Factory methods
    }
}