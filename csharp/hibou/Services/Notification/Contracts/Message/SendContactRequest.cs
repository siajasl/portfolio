using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Core.Operation;

namespace Keane.CH.Framework.Services.Contracts.Notification.Message
{
    /// <summary>
    /// Encapsulates data required for a contact request.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Notification")]
    [Serializable]
    public class SendContactRequest : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the contact first name.
        /// </summary>        
        public string FirstName
        { get; set; }

        /// <summary>
        /// Gets or sets the contact surname.
        /// </summary>        
        public string Surname
        { get; set; }

        /// <summary>
        /// Gets or sets the contact postal address line 1.
        /// </summary>        
        public string PostalAddressLine1
        { get; set; }

        /// <summary>
        /// Gets or sets the contact postal address zip code.
        /// </summary>        
        public string PostalAddressZip
        { get; set; }

        /// <summary>
        /// Gets or sets the contact postal address town.
        /// </summary>        
        public string PostalAddressTown
        { get; set; }

        /// <summary>
        /// Gets or sets the contact email address.
        /// </summary>        
        public string EmailAddress
        { get; set; }

        /// <summary>
        /// Gets or sets the contact email subject.
        /// </summary>        
        public string EmailSubject
        { get; set; }

        /// <summary>
        /// Gets or sets the contact email body.
        /// </summary>        
        public string EmailBody
        { get; set; }

        #endregion Properties
    }
}