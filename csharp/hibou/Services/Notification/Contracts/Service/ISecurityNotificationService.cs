using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Entity.Contracts;

namespace Keane.CH.Framework.Services.Notification.Contracts
{
    /// <summary>
    /// Encapsulates service operations for sending security related notifications.
    /// </summary>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    public interface ISecurityNotificationService
    {
        /// <summary>
        /// Sends a credentials initialisation notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        OperationResponse
            SendCreateUserNotification(
                SendCreateUserNotificationRequest request);

        /// <summary>
        /// Sends a credentials forgotten notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        OperationResponse
            SendForgottenCredentialsNotification(
                SendForgottenCredentialsNotificationRequest request);
    }
}