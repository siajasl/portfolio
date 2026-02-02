using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.DataAccess.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using Keane.CH.Framework.DataAccess.Core;
using System.Reflection;
using Keane.CH.Framework.DataAccess.ORM;
using Keane.CH.Framework.Services.Security.Contracts.Message;
using Keane.CH.Framework.Services.Security.Implementation;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Security.Implementation.DataAccess
{
    /// <summary>
    /// Encapsualtes all data access operations for a CodeItem domain object.
    /// </summary>
    public class UserDao<U> :
        EntityDao<U>,
        IUserDao
        where U : User, new()
    {
        #region IUserDao Members

        /// <summary>
        /// Gets an instance by searching across the repository by username.
        /// </summary>
        /// <param name="userName">The system unique username.</param>
        /// <returns>A user object.</returns>
        User IUserDao.Get(string userName)
        {
            User result = null;
            try
            {
                // Prepare operation.
                Database db = base.Dao.GetDatabase();
                using (DbCommand cmd =
                    base.Dao.GetCommand(db, "[Application].[uspUserSearchByUserName]", false))
                {
                    // Prepare operation parameters.
                    db.AddInParameter(cmd, "@USER_NAME", DbType.String, userName);

                    // Execute operation.
                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        // Insantiate, map and add to the result.
                        if (dr.Read())
                        {
                            result = new User();
                            ORMapper.Map(base.Mappings, dr, result);
                        }
                    }
                }
            }
            // Handle exceptions.
            catch (Exception fault)
            {
                if (base.Dao.ExceptionHandlerDelegate != null)
                    base.Dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }

            // Return result.
            return result;
        }

        /// <summary>
        /// Gets an instance by searching for the user that originated the protected entity modification.
        /// </summary>
        /// <param name="modificationId">The protected entity modification id.</param>
        /// <returns>A user object.</returns>
        User IUserDao.GetByModificationId(int modificationId)
        {
            User result = null;
            try
            {
                // Prepare operation.
                Database db = base.Dao.GetDatabase();
                using (DbCommand cmd =
                    base.Dao.GetCommand(db, "[Application].[uspUserSearchByModificationId]", false))
                {
                    // Prepare operation parameters.
                    db.AddInParameter(cmd, "@MODIFICATION_ID", DbType.Int32, modificationId);

                    // Execute operation.
                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        // Insantiate, map and add to the result.
                        if (dr.Read())
                        {
                            result = new User();
                            ORMapper.Map(base.Mappings, dr, result);
                        }
                    }
                }
            }
            // Handle exceptions.
            catch (Exception fault)
            {
                if (base.Dao.ExceptionHandlerDelegate != null)
                    base.Dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }

            // Return result.
            return result;
        }

        /// <summary>
        /// Gets a list of filtered by the passed role type.
        /// </summary>
        /// <param name="refIdUserRoleType">The type of role.</param>
        /// <returns>A list of users filtered by role type.</returns>
        EntityBaseCollection<User> IUserDao.GetByRoleType(int refIdUserRoleType)
        {
            EntityBaseCollection<User> result = new EntityBaseCollection<User>();
            try
            {
                // Prepare operation.
                Database db = base.Dao.GetDatabase();
                using (DbCommand cmd =
                    base.Dao.GetCommand(db, "[Application].[uspUserSearchByUserRoleTypeId]", false))
                {
                    // Prepare operation parameters.
                    db.AddInParameter(cmd, "@USER_ROLE_TYPE_ID", DbType.Int32, refIdUserRoleType);

                    // Execute operation.
                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        // Iterate datareader.
                        while (dr.Read())
                        {
                            // Insantiate, map & add to the result.
                            User item = new User();
                            ORMapper.Map(base.Mappings, dr, item);
                            result.Add(item);
                        }
                    }
                }
            }
            // Handle exceptions.
            catch (Exception fault)
            {
                if (base.Dao.ExceptionHandlerDelegate != null)
                    base.Dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }

            // Return result.
            return result;
        }

        #endregion
    }
}
