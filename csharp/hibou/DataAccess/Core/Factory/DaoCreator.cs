using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.DataAccess.Search.Configuration;
using System.IO;

namespace Keane.CH.Framework.DataAccess.Core.Factory
{
    /// <summary>
    /// Manages instantiation of dao abstract pointers.
    /// </summary>
    public sealed class DaoCreator
    {
        #region Constructors

        private DaoCreator() { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Instantiates & returns a dao interface.
        /// </summary>
        /// <param name="daoFile">The dao file.</param>
        /// <returns>A configured dao interface.</returns>
        public static IDao Create(
            FileInfo daoFile)
        {
            // Instantiate concrete instance.
            Dao concreteInstance = new Dao();

            // Inject dependecies.
            concreteInstance.Config =
                DaoConfigurationCache.Get(daoFile, true);

            // Return abstract pointer.
            return (IDao)concreteInstance;
        }

        #endregion Methods
    }
}