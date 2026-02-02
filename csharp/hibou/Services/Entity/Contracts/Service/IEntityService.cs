using System;
using Keane.CH.Framework.Services.Core.Operation;
using System.ServiceModel;
using Keane.CH.Framework.Services.Entity.Contracts.Message;

namespace Keane.CH.Framework.Services.Entity.Contracts
{
    /// <summary>
    /// Entity specific service interface.
    /// </summary>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01")]
    public interface IEntityService
    {
        /// <summary>
        /// Deletes a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        [OperationContract()]
        DeleteResponse Delete(DeleteRequest request);

        /// <summary>
        /// Deletes a single protected instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        [OperationContract()]
        DeleteProtectedResponse DeleteProtected(DeleteProtectedRequest request);

        /// <summary>
        /// Retrieves a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        RetrieveResponse Retrieve(RetrieveRequest request);

        /// <summary>
        /// Inserts a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>The id of the newly inserted instance.</returns>
        [OperationContract()]
        InsertResponse Insert(InsertRequest request);

        /// <summary>
        /// Updates a single instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        [OperationContract()]
        UpdateResponse Update(UpdateRequest request);

        /// <summary>
        /// Updates a single protected instance.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        [OperationContract()]
        UpdateProtectedResponse UpdateProtected(UpdateProtectedRequest request);

        /// <summary>
        /// Processes a protected entity decision.
        /// </summary>
        /// <param name="request">The request data.</param>
        /// <returns>Search results.</returns>
        [OperationContract()]
        AdjudicateResponse
            Adjudicate(AdjudicateRequest request);
    }
}