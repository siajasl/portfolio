using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Notification.Contracts;
using System.ServiceModel;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Contracts.Notification.Message;

namespace Keane.CH.Framework.Services.Notification.Contracts
{
    /// <summary>
    /// Encapsualtes all notificatiosn to be used across the application.
    /// </summary>
    [ServiceContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    public interface INotificationService :
        IProtectedEntityNotificationService, 
        ISecurityNotificationService
    {
        /// <summary>
        /// Sends a contact notification.
        /// </summary>
        /// <param name="request">The request parameters.</param>
        /// <returns>A single instance.</returns>
        [OperationContract()]
        OperationResponse SendContactNotification(SendContactRequest request);
    }
}
