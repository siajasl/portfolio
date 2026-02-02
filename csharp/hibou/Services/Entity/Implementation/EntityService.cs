using System;
using System.Diagnostics;
using Keane.CH.Framework.Core.ExtensionMethods;
using Keane.CH.Framework.Core.Utilities.Caching;
using Keane.CH.Framework.Core.Utilities.DataContract;
using Keane.CH.Framework.DataAccess.Entity;
using Keane.CH.Framework.Services.Core;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Entity.Contracts;
using Keane.CH.Framework.Services.Entity.Contracts.Data;
using Keane.CH.Framework.Services.Entity.Contracts.Message;
using Keane.CH.Framework.Services.Notification.Contracts;

namespace Keane.CH.Framework.Services.Entity.Implementation
{
    /// <summary>
    /// Entity specific service base class for encapsulating common functionality.
    /// </summary>
    /// <typeparam name="E">A sub-class inheriting from Entity.</typeparam>
    public class EntityService<E> :
        ServiceImplementationBase,
        IEntityService
        where E : EntityBase, new()
    {
        #region Properties

        #region Collaborators

        /// <summary>
        /// Gets or sets the associated entity dao.
        /// </summary>
        public IEntityDao EntityDao
        { get; set; }

        /// <summary>
        /// Gets or sets the associated entity cache accessor.
        /// </summary>
        public IEntityCacheAccessor EntityCache
        { get; set; }

        /// <summary>
        /// Gets or sets the associated protected entity modification dao.
        /// </summary>
        /// <remarks>
        /// This is used when updating/deleting protected entities.
        /// </remarks>
        public IEntityDao ProtectedEntityModificationDao
        { get; set; }

        /// <summary>
        /// Gets or sets the associated protected entity notification service.
        /// </summary>
        public IProtectedEntityNotificationService ProtectedEntityNotificationService
        { get; set; }

        #endregion Collaborators

        /// <summary>
        /// Gets or sets the protected entity type id.
        /// </summary>
        /// <remarks>
        /// This is used when updating/deleting protected entities.
        /// </remarks>
        public int ProtectedEntityTypeId
        { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Retrieves a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        public virtual RetrieveResponse Retrieve(
            RetrieveRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null);
            Debug.Assert(request.Context != null);
            Debug.Assert(request.EntityType != null);

            // Execute operation.
            try
            {
                // Retrieve from cache (if caching).
                EntityBase instance = null;
                bool retrievedFromCache = false;
                if (this.EntityCache != null)
                {
                    instance = this.EntityCache.Get(request.EntityType, request.EntityId);
                    retrievedFromCache = (instance != null);                
                }

                // Retrieve instance from repository (if ncessary).
                if (instance == null)
                {
                    instance = this.EntityDao.Retrieve(request.EntityType, request.EntityId);                
                }

                // If caching & not retrieved from cache then add to cache.
                if (this.EntityCache != null && 
                    instance != null && 
                    !retrievedFromCache)
                {
                    this.EntityCache.Add(instance);
                }

                // Generate service response.
                RetrieveResponse response = new RetrieveResponse();
                response.Entity = instance;
                response.Status = OperationResponseStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                RetrieveResponse response = new RetrieveResponse();                
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        /// <summary>
        /// Deletes a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        public virtual DeleteResponse Delete(
            DeleteRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null);
            Debug.Assert(request.Context != null);
            Debug.Assert(request.EntityType != null);

            // Execute operation.
            try
            {
                try
                {
                    // Attempt a hard delete.
                    this.EntityDao.Delete(request.EntityType, request.EntityId);

                    // Synchronize cache (if necessary).
                    if (this.EntityCache != null)
                        this.EntityCache.Remove(request.EntityType, request.EntityId);
                }
                catch 
                {
                    // On failure set state to inactive.
                    EntityDao.UpdateState(request.EntityType, request.EntityId, EntityState.InActive);
                }

                // Generate service response.
                DeleteResponse response = new DeleteResponse();
                response.Status = OperationResponseStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                DeleteResponse response = new DeleteResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        /// <summary>
        /// Deletes a single protected instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        public virtual DeleteProtectedResponse DeleteProtected(
            DeleteProtectedRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null);
            Debug.Assert(request.Context != null);
            Debug.Assert(request.EntityType != null);

            // Execute operation.
            try
            {
                // Update state.
                this.EntityDao.UpdateState(
                    request.EntityType,
                    request.EntityId, 
                    EntityState.InActivePending);

                // Derive modification details.
                ProtectedEntityModification modification = 
                    this.GetEntityModification(
                        request.EntityId, 
                        ProtectedEntityModificationType.Delete, 
                        request.Context);
                
                // Persist modication details.
                this.ProtectedEntityModificationDao.Save(modification);

                // Send notification.
                if (this.ProtectedEntityNotificationService != null)
                {
                    SendNotification(
                        request.EntityId,
                        ProtectedEntityModificationType.Delete,
                        request.Context);                
                }

                // Synchronize cache (if necessary).
                if (this.EntityCache != null)
                {
                    this.EntityCache.UpdateState(
                        request.EntityType,
                        request.EntityId, 
                        EntityState.InActivePending);
                }

                // Generate service response.
                DeleteProtectedResponse response = new DeleteProtectedResponse();
                response.Status = OperationResponseStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                DeleteProtectedResponse response = new DeleteProtectedResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        /// <summary>
        /// Updates a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        public virtual UpdateResponse Update(
            UpdateRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null);
            Debug.Assert(request.Context != null);
            Debug.Assert(request.Entity != null);

            // Execute operation.
            try
            {
                // Persist the changes.
                this.EntityDao.Save((E)request.Entity);

                // Synchronize cache (if necessary).
                if (this.EntityCache != null)
                    this.EntityCache.Update(request.Entity);

                // Generate service response.
                UpdateResponse response = new UpdateResponse();
                response.Status = OperationResponseStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                UpdateResponse response = new UpdateResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        /// <summary>
        /// Updates a single protected instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        public virtual UpdateProtectedResponse UpdateProtected(
            UpdateProtectedRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null);
            Debug.Assert(request.Context != null);
            Debug.Assert(request.Entity != null);

            // Execute operation.
            try
            {
                // Update state.
                this.EntityDao.UpdateState(
                    request.Entity.GetType(),
                    request.Entity.Id, 
                    EntityState.ActivePending);

                // Derive modification details.
                ProtectedEntityModification modification =
                    this.GetEntityModification(
                        request.Entity.Id, 
                        ProtectedEntityModificationType.Update, 
                        request.Context);
                modification.TargetEntitySerialized =
                    (string)request.Entity.Serialize(SerializationType.XmlString);

                // Persist modification details.
                this.ProtectedEntityModificationDao.Save(modification);

                // Send notification.
                if (this.ProtectedEntityNotificationService != null)
                {
                    this.SendNotification(
                        request.Entity.Id,
                        ProtectedEntityModificationType.Update,
                        request.Context);
                }

                // Synchronize cache (if necessary).
                if (this.EntityCache != null)
                {
                    this.EntityCache.UpdateState(
                        typeof(E),
                        request.Entity.Id, 
                        EntityState.ActivePending);
                }

                // Generate service response.
                UpdateProtectedResponse response = new UpdateProtectedResponse();
                response.Status = OperationResponseStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                UpdateProtectedResponse response = new UpdateProtectedResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        /// <summary>
        /// Inserts a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>The id of the newly inserted instance.</returns>
        public virtual InsertResponse Insert(
            InsertRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null);
            Debug.Assert(request.Context != null);
            Debug.Assert(request.Entity != null);

            // Execute operation.
            try
            {
                // Persist the instance.
                this.EntityDao.Save((E)request.Entity);

                // Synchronize cache (if necessary).
                if (this.EntityCache != null)
                    this.EntityCache.Add(request.Entity);

                // Generate service response.
                InsertResponse response = new InsertResponse();
                response.EntityId = request.Entity.Id;
                response.Status = OperationResponseStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                InsertResponse response = new InsertResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        /// <summary>
        /// Processes a protected entity decision.
        /// </summary>
        /// <param name="request">The request data.</param>
        /// <returns>Search results.</returns>
        public virtual AdjudicateResponse Adjudicate(
            AdjudicateRequest request)
        {
            // Defensive programming.
            Debug.Assert(request != null);
            Debug.Assert(request.Context != null);

            // Execute operation.
            try
            {
                // Update modification decision details.
                ProtectedEntityModification modification =
                    (ProtectedEntityModification)this.ProtectedEntityModificationDao.Retrieve(typeof(ProtectedEntityModification), request.ModificationId);
                modification.DecisionType = request.DecisionType;
                modification.DecisionUserName = request.Context.UserName;
                modification.DecisionDate = DateTime.Now;
                ProtectedEntityModificationDao.Save(modification);

                // Derive post decision entity state.
                EntityState entityState = EntityState.Active;
                if (modification.RequestType == ProtectedEntityModificationType.Delete &&
                    request.DecisionType == AjudicationDecisionType.Accept)
                {
                    entityState = EntityState.InActive;
                }

                // Update entity state.
                this.ProtectedEntityModificationDao.UpdateState(
                    modification.TargetEntityTypeId, modification.TargetEntityId, entityState);

                // Send notification.
                if (this.ProtectedEntityNotificationService != null)
                {
                    SendAjudicationNotificationRequest notificationRequest = new SendAjudicationNotificationRequest() 
                    { 
                        Context = request.Context,
                        DecisionType = request.DecisionType,
                        ModificationId = request.ModificationId
                    };
                    this.ProtectedEntityNotificationService.SendAdjudicationNotification(notificationRequest);
                }

                // Generate service response.
                AdjudicateResponse response = new AdjudicateResponse();
                response.Status = OperationResponseStatus.Success;
                return response;
            }
            catch (Exception ex)
            {
                AdjudicateResponse response = new AdjudicateResponse();
                response.Status = OperationResponseStatus.Exception;
                return response;
            }
        }

        #endregion Public Methods

        #region Private methods

        /// <summary>
        /// Gets the requested modification details.
        /// </summary>
        /// <param name="instanceId">The id of the instance.</param>
        /// <param name="modificationType">The type of modification.</param>
        /// <returns>The modification details.</returns>
        private ProtectedEntityModification GetEntityModification(
            int instanceId, 
            ProtectedEntityModificationType modificationType, 
            OperationRequestContext requestContext)
        {
            ProtectedEntityModification result = new ProtectedEntityModification()
            {
                DecisionType = AjudicationDecisionType.Undecided,
                RequestDate = DateTime.Now.PrecisionSafe(),
                RequestType = modificationType,
                RequestUserId = (requestContext.UserId),
                TargetEntityId = instanceId,
                TargetEntityTypeId = ProtectedEntityTypeId,
                TargetEntityTypeName = typeof(E).AssemblyQualifiedName
            };
            return result;
        }

        /// <summary>
        /// Sends a protected entity modification notification.
        /// </summary>
        /// <param name="entityId">The id of the entity being modified.</param>
        /// <param name="modificationType">The modification type.</param>
        /// <param name="operationRequestContext">The operation request type.</param>
        private void SendNotification(
            int entityId,
            ProtectedEntityModificationType modificationType,
            OperationRequestContext operationRequestContext)
        {
            // Invoke service operation.
            SendModificationNotificationRequest request = new SendModificationNotificationRequest()
            {
                Context = operationRequestContext,
                EntityId = entityId,
                EntityTypeId = ProtectedEntityTypeId,
                ModificationType = modificationType
            };
            this.ProtectedEntityNotificationService.SendModificationNotification(request);
        }

        #endregion Private methods
    }
}