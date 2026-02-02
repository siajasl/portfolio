using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Core.Operation;

namespace Keane.CH.Framework.Services.Notification.Contracts
{
    /// <summary>
    /// Encapsulates the information required to send a user credentials forgoteen notification.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public class SendForgottenCredentialsNotificationRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email address of the user who has forgotten their credentials.
        /// </summary>        
        [DataMember()]
        public string EmailAddress
        { get; set; }

        /// <summary>
        /// Gets or sets the temporary password.
        /// </summary>        
        [DataMember()]
        public string TemporaryPassword
        { get; set; }

        #endregion Properties
    }
}