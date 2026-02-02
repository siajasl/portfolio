using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Entity.Contracts;
using Keane.CH.Framework.Services.Entity.Contracts.Message;

namespace Keane.CH.Framework.Services.Notification.Contracts
{
    /// <summary>
    /// Encapsulates service operations for sending entity related notifications.
    /// </summary>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    public interface IProtectedEntityNotificationService
    {
        /// <summary>
        /// Sends an entity modification request notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        OperationResponse
            SendModificationNotification(
                SendModificationNotificationRequest request);

        /// <summary>
        /// Sends an entity modification decision notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        OperationResponse
            SendAdjudicationNotification(
                SendAjudicationNotificationRequest request);
    }
}