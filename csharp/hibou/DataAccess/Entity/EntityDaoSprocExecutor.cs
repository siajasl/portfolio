using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Keane.CH.Framework.DataAccess.Core;
using Keane.CH.Framework.DataAccess.Entity.Configuration;
using Keane.CH.Framework.Core.Utilities.Exceptions;
using Keane.CH.Framework.Core.ExtensionMethods;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Keane.CH.Framework.DataAccess.ORM;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Manages all execution of entity related stored procedures.
    /// </summary>
    internal sealed class EntityDaoSprocExecutor
    {
        #region Constants

        private const string STD_PARAM_OPERATION_STATE = "";

        #endregion Constants

        #region Delete

        /// <summary>
        /// Deletes an instance.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="entity">The entity being updated.</param>
        internal static void Delete(
            IDao dao,
            EntityDaoConfiguration config,
            EntityBase entity)
        {
            // Defensive programming.
            AssertParameters(dao, config, entity, true);

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.Delete, entity.Id, db, cmd);

                    // Execute operation.
                    db.ExecuteNonQuery(cmd);
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        #endregion Delete

        #region Get

        /// <summary>
        /// Gets an entity instance.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="entityId">The Id of the entity to be retrieved.</param>
        internal static EntityBase Get<E>(
            IDao dao,
            EntityDaoConfiguration config,
            int entityId)
            where E : EntityBase, new()
        {
            // Defensive programming.
            AssertParameters(dao, config, entityId);

            // Default result.
            EntityBase result = null;

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.Get, entityId, db, cmd);

                    // Execute operation.
                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        // Insantiate & map accordingly.
                        if (dr.Read())
                        {
                            result = new E();
                            ORMapper.Map(config.Mappings, dr, result);
                        }
                    }
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }

            // Return result.
            return result;
        }

        /// <summary>
        /// Gets a collection of all entities.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The entity dao configuration data.</param>
        internal static IEnumerable<EntityBase> GetAll<E>(
            IDao dao,
            EntityDaoConfiguration config)
            where E : EntityBase, new()
        {
            // Defensive programming.
            AssertParameters(dao, config);

            // Default result.
            List<EntityBase> result = new List<EntityBase>();

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.GetAll, db, cmd);

                    // Execute operation.
                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        // Iterate datareader.
                        while (dr.Read())
                        {
                            // Insantiate, map & add to the result.
                            E item = new E();
                            ORMapper.Map(config.Mappings, dr, item);
                            result.Add(item);
                        }
                    }
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }

            // Return result.
            return result.AsEnumerable();
        }

        /// <summary>
        /// Returns the count of instances.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The entity dao configuration data.</param>
        internal static int GetCount(
            IDao dao,
            EntityDaoConfiguration config)
        {
            // Defensive programming.
            AssertParameters(dao, config);

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.GetCount, db, cmd);

                    // Execute operation.
                    db.ExecuteScalar(cmd);

                    // Return ouput result.
                    return (int)db.GetParameterValue(cmd, "@OPERATION_RESULT");
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        /// <summary>
        /// Gets the current version of an instance.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="entityId">The Id of the entity whose current version is to be retrieved.</param>
        internal static int GetVersion(
            IDao dao,
            EntityDaoConfiguration config,
            int entityId)
        {
            // Defensive programming.
            AssertParameters(dao, config, entityId);

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.GetVersion, entityId, db, cmd);

                    // Execute operation.
                    db.ExecuteScalar(cmd);

                    // Return ouput result.
                    return (int)db.GetParameterValue(cmd, "@OPERATION_RESULT");
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        /// <summary>
        /// Gets the current state of an instance.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="entityId">The Id of the entity whose current state is to be retrieved.</param>
        internal static int GetState(
            IDao dao,
            EntityDaoConfiguration config,
            int entityId)
        {
            // Defensive programming.
            AssertParameters(dao, config, entityId);

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.GetState, entityId, db, cmd);

                    // Execute operation.
                    db.ExecuteScalar(cmd);

                    // Return ouput result.
                    return (int)db.GetParameterValue(cmd, "@OPERATION_RESULT");
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        #endregion Get

        #region Update

        /// <summary>
        /// Updates an entity instance.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="dao">A dao helper.</param>
        /// <param name="entity">The entity being updated.</param>
        /// <exception cref="ConcurrencyException"></exception>
        internal static void Update(
            IDao dao,
            EntityDaoConfiguration config,
            EntityBase entity)
        {
            // Defensive programming.
            AssertParameters(dao, config, entity, true);

            // Execute operation.
            try
            {
                // Perform concurrency test (may throw an exception).
                PerformConcurrencyTest(dao, config, entity);

                // Increment the entity version.
                entity.EntityInfo.EntityVersion++;
                
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.Update, entity, db, cmd);

                    // Add mapped parameters.
                    ORMapper.AddMappedParameterList(
                        config.Mappings, db, cmd, entity);

                    // Execute operation.
                    db.ExecuteNonQuery(cmd);
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        #endregion Update

        #region Update state

        /// <summary>
        /// Updates the state of an instance.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="entityId">The Id of the entity to be deleted.</param>
        /// <param name="entityState">The state of the entity against which the operation may or may not be executed against.</param>
        internal static void UpdateState(
            IDao dao,
            EntityDaoConfiguration config,
            int entityId,
            EntityState entityState)
        {
            // Defensive programming.
            AssertParameters(dao, config, entityId);

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.UpdateState, entityId, entityState, db, cmd);

                    // Execute operation.
                    db.ExecuteNonQuery(cmd);
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        /// <summary>
        /// Updates the state of an instance.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="entityTypeId">The entity type id.</param>
        /// <param name="entityId">The Id of the entity to be deleted.</param>
        /// <param name="entityState">The state of the entity against which the operation may or may not be executed against.</param>
        internal static void UpdateState(
            IDao dao,
            int entityTypeId,
            int entityId,
            EntityState entityState)
        {
            // Defensive programming.
            AssertParameters(dao, entityTypeId, entityId, entityState);

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, "[Application].[uspProtectedEntityUpdateStatus]", false))
                {
                    // Entity Type Id.
                    db.AddInParameter(
                        cmd, @"@ENTITY_TYPE_ID", DbType.Int32, entityTypeId);

                    // Entity Id.
                    db.AddInParameter(
                        cmd, @"@ENTITY_ID", DbType.Int32, entityId);

                    // Entity State.
                    db.AddInParameter(
                        cmd, @"@ENTITY_STATE", DbType.Int32, (int)entityState);

                    // Operation user.
                    db.AddInParameter(
                        cmd, @"@OPERATION_USER", DbType.String, Environment.UserName);

                    // Execute operation.
                    db.ExecuteNonQuery(cmd);
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        #endregion Update state

        #region Insert

        /// <summary>
        /// Inserts an entity instance.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="dao">A dao helper.</param>
        /// <param name="entity">The entity being inserted.</param>
        internal static void Insert(
            IDao dao,
            EntityDaoConfiguration config,
            EntityBase entity)
        {
            // Defensive programming.
            AssertParameters(dao, config, entity, false);

            // Execute operation.
            try
            {
                // Prepare operation.
                Database db = dao.GetDatabase();
                using (DbCommand cmd = dao.GetCommand(db, config.DbCommand, false))
                {
                    // Add standard parameters.
                    AddStandardParameters(
                        config, EntityDaoOperationType.Insert, entity, db, cmd);

                    // Add mapped parameters.
                    ORMapper.AddMappedParameterList(
                        config.Mappings, db, cmd, entity);

                    // Execute operation.
                    db.ExecuteNonQuery(cmd);

                    // Derive inserted entity id from result.
                    entity.Id = (int)db.GetParameterValue(cmd, "@OPERATION_RESULT");
                }
            }

            // Handle operation faults.
            catch (Exception fault)
            {
                if (dao.ExceptionHandlerDelegate != null)
                    dao.ExceptionHandlerDelegate(fault, MethodBase.GetCurrentMethod());
                throw fault;
            }
        }

        #endregion Insert

        #region Private methods

        /// <summary>
        /// Performs a concurrency test just prior to the update operation.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="dao">A dao helper.</param>
        /// <param name="entity">The entity being updated.</param>
        private static void PerformConcurrencyTest(
            IDao dao,
            EntityDaoConfiguration config,
            EntityBase entity)
        {
            // Compare in memory entity version with the persisted entity version.
            int persistedVersion = GetVersion(dao, config, entity.Id);
            if (entity.EntityInfo.EntityVersion != persistedVersion)
                throw new ConcurrencyException();
        }

        #region Defensive programming

        /// <summary>
        /// Performs parameter assertions.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="dao">A dao helper.</param>
        private static void AssertParameters(
            IDao dao,
            EntityDaoConfiguration config)
        {
            AssertParameters(dao);
            if (config == null)
                throw new ArgumentNullException("config");
            if (config.StandardParameters == null)
                throw new ArgumentNullException("config.StandardParameters");
        }

        /// <summary>
        /// Performs parameter assertions.
        /// </summary>
        /// <param name="dao">The dao.</param>
        /// <param name="entityTypeId">The entity type id.</param>
        /// <param name="entityId">The Id of the entity to be deleted.</param>
        /// <param name="entityState">The state of the entity against which the operation may or may not be executed against.</param>
        private static void AssertParameters(
            IDao dao,
            int entityTypeId,
            int entityId,
            EntityState entityState)
        {
            AssertParameters(dao);
            if (entityId <= 0)
                throw new ArgumentException("Entity Id is less than zero: stored procedure can only be executed against existing entities.");
            if (entityState == EntityState.Unspecified)
                throw new ArgumentException("Entity state must be specified.");
        }

        /// <summary>
        /// Performs parameter assertions.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="dao">A dao helper.</param>
        /// <param name="entityId">The id of the entity being processed.</param>
        private static void AssertParameters(
            IDao dao,
            EntityDaoConfiguration config,
            int entityId)
        {
            AssertParameters(dao, config);
            if (entityId <= 0)
                throw new ArgumentException("Entity Id is less than zero: stored procedure can only be executed against existing entities.");
        }

        /// <summary>
        /// Performs parameter assertions.
        /// </summary>
        /// <param name="dao">A dao helper.</param>
        private static void AssertParameters(
            IDao dao)
        {
            if (dao == null)
                throw new ArgumentNullException("dao");
        }

        /// <summary>
        /// Performs parameter assertions.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="dao">A dao helper.</param>
        /// <param name="entity">The entity being processed.</param>
        /// <param name="entityIdMustExist">Flag indicating whether the entity id must be that of an existing entity.</param>
        private static void AssertParameters(
            IDao dao,
            EntityDaoConfiguration config,
            EntityBase entity, 
            bool entityIdMustExist)
        {
            AssertParameters(dao, config);
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (entityIdMustExist)
            {
                if (entity.Id <= 0)
                    throw new ArgumentException("Operation can only be performed against existing entities.");            
            }
            else 
            {
                if (entity.Id > 0)
                    throw new ArgumentException("Operation can only be performed against new entities.");
            }
        }

        #endregion Defensive programming

        #region Standard parameters

        /// <summary>
        /// Adds the parameters to the database command parameter collection.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="operationType">The type of operation being executed.</param>
        /// <param name="db">The db against which the db command will be executed.</param>
        /// <param name="cmd">The db command to be executed.</param>
        private static void AddStandardParameters(
            EntityDaoConfiguration config,
            EntityDaoOperationType operationType,
            Database db,
            DbCommand cmd)
        {
            // Operation type.
            db.AddInParameter(
                cmd, 
                config.StandardParameters.OperationType, 
                DbType.Int32, 
                (int)operationType);

            // Operation result (out).
            db.AddInParameter(
                cmd, 
                config.StandardParameters.OperationResult, 
                DbType.Int32, 
                0);
            cmd.Parameters[config.StandardParameters.OperationResult].Direction = ParameterDirection.InputOutput;

            // Operation date.
            db.AddInParameter(
                cmd, 
                config.StandardParameters.OperationDate, 
                DbType.DateTime, 
                DateTime.Now.PrecisionSafe());

            // Operation user.
            db.AddInParameter(
                cmd, 
                config.StandardParameters.OperationUser, 
                DbType.String, 
                Environment.UserName);

            // Operation entity id (out).
            db.AddInParameter(
                cmd, 
                config.StandardParameters.OperationEntityId, 
                DbType.Int32, 
                0);
            cmd.Parameters[config.StandardParameters.OperationEntityId].Direction = ParameterDirection.InputOutput;

            // Operation entity state.
            db.AddInParameter(
                cmd, 
                config.StandardParameters.OperationEntityState, 
                DbType.Int32, 
                0);

            // Operation entity version.
            db.AddInParameter(
                cmd, 
                config.StandardParameters.OperationEntityVersion, 
                DbType.Int32, 
                0);
        }

        /// <summary>
        /// Adds the parameters to the database command parameter collection.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="operationType">The type of operation being executed.</param>
        /// <param name="entityId">The id of the entity against which the operation may or may not be executed against.</param>
        /// <param name="entityState">The state of the entity against which the operation may or may not be executed against.</param>
        /// <param name="db">The db against which the db command will be executed.</param>
        /// <param name="cmd">The db command to be executed.</param>
        private static void AddStandardParameters(
            EntityDaoConfiguration config,
            EntityDaoOperationType operationType,
            int entityId,
            EntityState entityState,
            Database db,
            DbCommand cmd)
        {
            AddStandardParameters(config, operationType, entityId, db, cmd);
            cmd.Parameters[config.StandardParameters.OperationEntityState].Value = (int)entityState;
        }

        /// <summary>
        /// Adds the parameters to the database command parameter collection.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="operationType">The type of operation being executed.</param>
        /// <param name="entityId">The id of the entity against which the operation may or may not be executed against.</param>
        /// <param name="db">The db against which the db command will be executed.</param>
        /// <param name="cmd">The db command to be executed.</param>
        private static void AddStandardParameters(
            EntityDaoConfiguration config,
            EntityDaoOperationType operationType,
            int entityId,
            Database db,
            DbCommand cmd)
        {
            AddStandardParameters(config, operationType, db, cmd);
            cmd.Parameters[config.StandardParameters.OperationEntityId].Value = entityId;
        }

        /// <summary>
        /// Adds the parameters to the database command parameter collection.
        /// </summary>
        /// <param name="config">The entity dao configuration data.</param>
        /// <param name="operationType">The type of operation being executed.</param>
        /// <param name="entityId">The id of the entity against which the operation may or may not be executed against.</param>
        /// <param name="db">The db against which the db command will be executed.</param>
        /// <param name="cmd">The db command to be executed.</param>
        private static void AddStandardParameters(
            EntityDaoConfiguration config,
            EntityDaoOperationType operationType,
            EntityBase entity,
            Database db,
            DbCommand cmd)
        {
            AddStandardParameters(config, operationType, entity.Id, db, cmd);
            DateTime operationDate = (DateTime)cmd.Parameters[config.StandardParameters.OperationDate].Value;
            if (entity.EntityInfo.IsNew)
            {
                entity.EntityInfo.EntityCreateDate = operationDate;
            }
            else
            {
                entity.EntityInfo.EntityUpdateDate = operationDate;
            }
            cmd.Parameters[config.StandardParameters.OperationEntityState].Value = (int)entity.EntityInfo.EntityState;
            cmd.Parameters[config.StandardParameters.OperationEntityVersion].Value = (int)entity.EntityInfo.EntityVersion;
        }

        #endregion Standard parameters

        #endregion Private methods
    }
}
