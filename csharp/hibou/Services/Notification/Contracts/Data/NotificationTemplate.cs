using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Notification.Contracts.Data
{
    /// <summary>
    /// Encapsulates the data required for a notification template.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public class NotificationTemplate
    {
        #region Properties

        /// <summary>
        /// Gets or sets the notification template type.
        /// </summary>
        [DataMember()]
        public NotificationTemplateType TemplateType
        { get; set; }

        /// <summary>
        /// Gets or sets the notification body.
        /// </summary>
        [DataMember()]
        public string Body
        { get; set; }

        /// <summary>
        /// Gets or sets the notification subject.
        /// </summary>
        [DataMember()]
        public string Subject
        { get; set; }

        #endregion Properties
    }
}
