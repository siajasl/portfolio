using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Core.Utilities.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace Keane.CH.Framework.DataAccess.Core
{
    /// <summary>
    /// Implemented by classes acting as data access object.
    /// </summary>
    /// <remarks>
    /// This encapsulates methods required to 
    /// </remarks>
    public interface IDao
    {
        /// <summary>
        /// Gets or sets an exception handler delegate.
        /// </summary>
        ExceptionHandlerDelegate ExceptionHandlerDelegate
        { get; set; }

        /// <summary>
        /// Returns a EntLib database wrapper instance.
        /// </summary>
        /// <remarks>
        /// This maps to the default database as specified in configuration.
        /// </remarks>
        /// <returns>A database wrapper instance.</returns>
        Database GetDatabase();

        /// <summary>
        /// Returns a EntLib database wrapper instance corresponding to the passed db connection reference.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <returns>A database wrapper instance.</returns>
        Database GetDatabase(
            string connection);

        /// <summary>
        /// Returns a database command object corresponding to a stored procedures.
        /// </summary>
        /// <param name="db">The database against which a command is to be instantiated.</param>
        /// <param name="storedProcedureName">The name of the stored procedure to be executed.</param>
        /// <param name="parseStoredProcedureName">Flag indicating whether the stored proc name will be passed or not.</param>
        /// <returns>A database command wrapper instance.</returns>
        DbCommand GetCommand(
            Database db,
            string storedProcedureName,
            bool parseStoredProcedureName);

        /// <summary>
        /// Returns a database command object corresponding to a stored procedures.
        /// </summary>
        /// <param name="db">The database against which a command is to be instantiated.</param>
        /// <param name="storedProcedureName">The name of the stored procedure to be executed.</param>
        /// <returns>A database command wrapper instance.</returns>
        DbCommand GetCommand(
            Database db,
            string storedProcedureName);
    }
}