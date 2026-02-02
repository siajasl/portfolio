using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Notification
{
    /// <summary>
    /// Represents a view enabling a user to contact the administrator.
    /// </summary>
    public interface IContactView
    {
        #region Properties

        /// <summary>
        /// Gets the contact first name.
        /// </summary>        
        string FirstName 
        { get; }

        /// <summary>
        /// Gets the contact surname.
        /// </summary>        
        string Surname
        { get; }

        /// <summary>
        /// Gets the contact postal address line 1.
        /// </summary>        
        string PostalAddressLine1
        { get; }

        /// <summary>
        /// Gets the contact postal address zip code.
        /// </summary>        
        string PostalAddressZip
        { get; }

        /// <summary>
        /// Gets the contact postal address town.
        /// </summary>        
        string PostalAddressTown
        { get; }
        
        /// <summary>
        /// Gets the contact email address.
        /// </summary>        
        string EmailAddress
        { get; }

        /// <summary>
        /// Gets the contact email subject.
        /// </summary>        
        string EmailSubject
        { get; }

        /// <summary>
        /// Gets the contact email body.
        /// </summary>        
        string EmailBody
        { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Executed when the contact email has been sent.
        /// </summary>
        void OnEmailDelivery();

        /// <summary>
        /// Executed when the contact email could not be sent.
        /// </summary>
        void OnEmailDeliveryFailure();

        #endregion Methods
    }
}