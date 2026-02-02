using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Security.Contracts.Message
{
    /// <summary>
    /// Defines forgotten credentials submitted by a user in order to retrieve a password.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public class AuthenticateForgottenStepOneRequest : AuthenticateRequestBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>        
        [DataMember()]
        public string EmailAddress
        { get; set; }

        #endregion Properties
    }
}
