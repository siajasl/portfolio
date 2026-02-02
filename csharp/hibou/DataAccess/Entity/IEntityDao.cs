using Keane.CH.Framework.Services.Core.Operation;
using System;
using System.Collections.Generic;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Entiy specific data access object interface.
    /// </summary>
    /// <typeparam name="E">A sub-class inheriting from Entity.</typeparam>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public interface IEntityDao
    {
        #region Event publisher

        /// <summary>
        /// Gets the associated events (permits event listening).
        /// </summary>
        IEntityDaoEvents Events
        { get; }

        #endregion Event publisher

        #region Delete

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entityType">The type of the entity being deleted.</param>
        /// <param name="entityId">The id of the entity being deleted.</param>
        void Delete(
            Type entityType, 
            int entityId);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">An entity to be deleted.</param>
        void Delete(
            EntityBase entity);

        /// <summary>
        /// Deletes a collection.
        /// </summary>
        /// <param name="collection">A collection of entities to be deleted.</param>
        void Delete(
            IEnumerable<EntityBase> collection);

        #endregion Delete

        #region Get

        /// <summary>
        /// Returns an entity.
        /// </summary>
        /// <param name="entityType">The type of the entity being retrieved.</param>
        /// <param name="entityId">The id of the entity being retrieved.</param>
        EntityBase Retrieve(
            Type entityType, 
            int entityId);

        /// <summary>
        /// Returns a collection of all entities.
        /// </summary>
        /// <param name="entityType">The type of the entity being retrieved.</param>
        /// <remarks>
        /// Used during unit tests & for caching purposes.
        /// </remarks>
        IEnumerable<EntityBase> RetrieveAll(
            Type entityType);

        /// <summary>
        /// Returns a collection of all entities filtered by the passed entity state.
        /// </summary>
        /// <param name="entityType">The type of the entity being retrieved.</param>
        /// <param name="entityState">The state used for performing the filter.</param>
        IEnumerable<EntityBase> RetrieveAllByState(
            Type entityType, 
            EntityState entityState);

        /// <summary>
        /// Returns the count of entities.
        /// </summary>
        /// <param name="entityType">The type of the entity whose count is being retrieved.</param>
        /// <remarks>
        /// Used during unit tests & search operations.
        /// </remarks>
        int RetrieveCount(
            Type entityType);

        /// <summary>
        /// Returns the entity version of an entity.
        /// </summary>
        /// <param name="entityType">The type of the entity whose version is being retrieved.</param>
        /// <param name="entityId">The id of the entity.</param>
        /// <remarks>
        /// Used during concurrency validation & unit tests.
        /// </remarks>
        int RetrieveVersion(
            Type entityType,
            int entityId);

        /// <summary>
        /// Returns the entity state of an entity.
        /// </summary>
        /// <param name="entityType">The type of the entity whose state is being retrieved.</param>
        /// <param name="entityId">The id of the entity.</param>
        /// <remarks>
        /// Used during unit tests.
        /// </remarks>
        EntityState RetrieveState(
            Type entityType,
            int entityId);

        #endregion Get

        #region Save

        /// <summary>
        /// Saves an entity.
        /// </summary>
        /// <param name="entity">An entity being saved.</param>
        void Save(
            EntityBase entity);

        /// <summary>
        /// Save a collection.
        /// </summary>
        /// <param name="collection">A collection of entities being saved.</param>
        void Save(
            IEnumerable<EntityBase> collection);

        #endregion Save

        #region Update state

        /// <summary>
        /// Updates the entity state.
        /// </summary>
        /// <param name="entityType">The type of the entity whose state is being updated.</param>
        /// <param name="entityId">The id of the entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        void UpdateState(
            Type entityType,
            int entityId,
            EntityState entityState);

        /// <summary>
        /// Updates the entity state.
        /// </summary>
        /// <param name="entityTypeId">The type id of the entity.</param>
        /// <param name="entityId">The id of the entity.</param>
        /// <param name="entityState">The new state of the entity.</param>
        void UpdateState(
            int entityTypeId,
            int entityId,
            EntityState entityState);

        #endregion Update state
    }
}
