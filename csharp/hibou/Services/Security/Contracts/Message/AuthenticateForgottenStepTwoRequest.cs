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
    public class AuthenticateForgottenStepTwoRequest : 
        AuthenticateForgottenStepOneRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the password answer.
        /// </summary>        
        [DataMember()]
        public string PasswordQuestionAnswer
        { get; set; }

        #endregion Properties
    }
}
