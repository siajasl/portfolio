using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Notification.Contracts.Data
{
    /// <summary>
    /// Enumeration over the types of notification templates in use within this system.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public enum NotificationTemplateType
    {
        [EnumMember()]
        UserInitialisation = 1,
        [EnumMember()]
        UserCredentialsForgotten = 2,
        [EnumMember()]
        ModificationRequest = 3,
        [EnumMember()]
        ModificationDecisionAccept = 4,
        [EnumMember()]
        ModificationDecisionReject = 5,
        [EnumMember()]
        Contact = 6
    }
}