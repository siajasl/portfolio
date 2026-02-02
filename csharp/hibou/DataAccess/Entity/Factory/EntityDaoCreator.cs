using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.DataAccess.Core.Factory;
using Keane.CH.Framework.Services.Entity.Contracts.Data;
using System.IO;
using System.Diagnostics;

namespace Keane.CH.Framework.DataAccess.Entity.Factory
{
    /// <summary>
    /// Manages instantiation & configuration of entity dao abstract pointers.
    /// </summary>
    public sealed class EntityDaoCreator
    {
        #region Factory methods

        /// <summary>
        /// Instantiates & returns an entity dao interface.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        /// <param name="defaultEntityDaoFile">The default entity dao file.</param>
        /// <returns>A configured entity dao interface.</returns>
        public static IEntityDao Create<E>(
            FileInfo daoFile,
            FileInfo entityDaoFile,
            FileInfo defaultEntityDaoFile)
            where E : EntityBase, new()
        {
            // Defensive programming.
            Debug.Assert(daoFile != null, "daoFile parameter is null");
            Debug.Assert(entityDaoFile != null, "entityDaoFile parameter is null");
            Debug.Assert(daoFile.Exists, "daoFile does not exist");
            Debug.Assert(entityDaoFile.Exists, "entityDaoFile does not exist");            
            if (defaultEntityDaoFile != null)
                Debug.Assert(defaultEntityDaoFile.Exists, "defaultEntityDaoFile does not exist");

            // Instantiate concrete instance.
            EntityDao<E> concreteInstance = new EntityDao<E>();

            // Configure.
            concreteInstance.Config =
                EntityDaoConfigurationCache.Get(daoFile, entityDaoFile, defaultEntityDaoFile);
            concreteInstance.Dao =
                DaoCreator.Create(daoFile);

            // Return abstract pointer.
            return (IEntityDao)concreteInstance;
        }

        /// <summary>
        /// Instantiates & returns an entity dao interface.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        /// <returns>A configured entity dao interface.</returns>
        public static IEntityDao Create<E>(
            FileInfo daoFile,
            FileInfo entityDaoFile)
            where E : EntityBase, new()
        {
            return Create<E>(daoFile, entityDaoFile, null);
        }

        /// <summary>
        /// Instantiates & returns an entity dao interface.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        /// <param name="defaultEntityDaoFile">The default entity dao file.</param>
        /// <returns>A configured entity dao interface.</returns>
        public static D Create<D, E>(
            FileInfo daoFile,
            FileInfo entityDaoFile,
            FileInfo defaultEntityDaoFile)
            where D : EntityDao<E>, new()
            where E : EntityBase, new()
        {
            // Defensive programming.
            Debug.Assert(daoFile != null, "daoFile parameter is null");
            Debug.Assert(entityDaoFile != null, "entityDaoFile parameter is null");
            Debug.Assert(daoFile.Exists, "daoFile does not exist");
            Debug.Assert(entityDaoFile.Exists, "entityDaoFile does not exist");
            if (defaultEntityDaoFile != null)
                Debug.Assert(defaultEntityDaoFile.Exists, "defaultEntityDaoFile does not exist");

            // Instantiate concrete instance.
            D concreteInstance = new D();

            // Configure.
            concreteInstance.Config =
                EntityDaoConfigurationCache.Get(daoFile, entityDaoFile, defaultEntityDaoFile);
            concreteInstance.Dao =
                DaoCreator.Create(daoFile);

            // Return abstract pointer.
            return concreteInstance;
        }

        /// <summary>
        /// Instantiates & returns an entity dao interface.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <param name="entityDaoFile">The entity dao file.</param>
        /// <returns>A configured entity dao interface.</returns>
        public static D Create<D, E>(
            FileInfo daoFile,
            FileInfo entityDaoFile)
            where D : EntityDao<E>, new()
            where E : EntityBase, new()
        {
            return Create<D, E>(daoFile, entityDaoFile, null);
        }

        #endregion Factory methods
    }
}