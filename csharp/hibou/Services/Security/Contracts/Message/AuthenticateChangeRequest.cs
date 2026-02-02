using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Security.Contracts.Message
{
    /// <summary>
    /// Defines credentials submitted by a user in order to change their password.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public class AuthenticateChangeRequest : AuthenticateRequestBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current password.
        /// </summary>        
        [DataMember()]
        public string Password
        { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>        
        [DataMember()]
        public string NewPassword
        { get; set; }

        #endregion Properties
    }
}