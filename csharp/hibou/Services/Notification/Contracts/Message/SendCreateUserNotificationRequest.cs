using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Core.Operation;

namespace Keane.CH.Framework.Services.Notification.Contracts
{
    /// <summary>
    /// Encapsulates the information required to send a user initialisation notification.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public class SendCreateUserNotificationRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email address of the new user.
        /// </summary>        
        [DataMember()]
        public string EmailAddress
        { get; set; }

        /// <summary>
        /// Gets or sets the intial password.
        /// </summary>        
        [DataMember()]
        public string InitialPassword
        { get; set; }

        #endregion Properties
    }
}
