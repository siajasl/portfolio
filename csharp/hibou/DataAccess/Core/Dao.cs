using System;
using System.Data.Common;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using Keane.CH.Framework.Core.Utilities.Exceptions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;

namespace Keane.CH.Framework.DataAccess.Core
{
    /// <summary>
    /// Database access object base class.
    /// </summary>
    /// <remarks>
    /// 1.  Encapsulates core database access functionality.
    /// 2.  Microsoft Enterprise Library acts as a mediator to the ADO.NET library.
    /// </remarks>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    [Serializable]
    public class Dao : IDao
    {
        #region Constructors

        /// <summary>
        /// Internal constructor ensures that clients must go through the factory.
        /// </summary>
        public Dao() 
        { }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the associated configuration data.
        /// </summary>
        internal DaoConfiguration Config
        { get; set; }

        #endregion Properties

        #region IDao members

        /// <summary>
        /// Gets or sets the assoicated exception delegate.
        /// </summary>
        public ExceptionHandlerDelegate ExceptionHandlerDelegate
        { get; set; }

        /// <summary>
        /// Returns the EntLib database wrapper instance mapping to the default database as specified in the configuration connection string keys.
        /// </summary>
        /// <returns>A database wrapper instance.</returns>
        public Database GetDatabase()
        {
            // Return the default connection string.
            return GetDatabase(Config.DbConnectionKey);
        }

        /// <summary>
        /// Attempts to connect to and return the EntLib database wrapper.
        /// </summary>
        /// <param name="connection">The connection to use.</param>
        /// <returns>A database wrapper instance.</returns>
        public Database GetDatabase(string connection)
        {
            // Return the appropriate database wrapper type.
            switch (Config.DatabaseType)
            {
                case DaoDbType.Oracle:
                    return DatabaseFactory.CreateDatabase(connection) as OracleDatabase;
                case DaoDbType.SqlServer:
                    return DatabaseFactory.CreateDatabase(connection);
                default:
                    throw new NotSupportedException("Database type is unsupported.");
            }
        }

        /// <summary>
        /// Returns a database command object corresponding to a stored procedures.
        /// </summary>
        /// <param name="db">The database against which a command is to be instantiated.</param>
        /// <param name="storedProcedureName">The name of the stored procedure to be executed.</param>
        /// <param name="parseStoredProcedureName">Flag indicating whether the stored proc name will be passed or not.</param>
        /// <returns>A database command wrapper instance.</returns>
        public DbCommand GetCommand(
            Database db,
            string storedProcedureName,
            bool parseStoredProcedureName)
        {
            // Initialise result.
            DbCommand result = null;

            // Derive command.
            if (parseStoredProcedureName)
            {
                result = 
                    db.GetStoredProcCommand(Config.ParseStoredProcedureName(storedProcedureName));
            }
            else
            {
                result = 
                    db.GetStoredProcCommand(storedProcedureName);
            }
            
            // Assign command specific configuration attributes.
            if (result != null && 
                Config.CommandTimeout > 0)
                result.CommandTimeout = Config.CommandTimeout;
            
            // Return result.
            return result;
        }

        /// <summary>
        /// Returns a database command object corresponding to a stored procedures.
        /// </summary>
        /// <param name="db">The database against which a command is to be instantiated.</param>
        /// <param name="storedProcedureName">The name of the stored procedure to be executed.</param>
        /// <returns>A database command wrapper instance.</returns>
        public DbCommand GetCommand(
            Database db,
            string storedProcedureName)
        {
            return GetCommand(db, storedProcedureName, true);
        }

        #endregion IDao members
    }
}