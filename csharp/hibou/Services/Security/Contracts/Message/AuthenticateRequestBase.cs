using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Core.Operation;

namespace Keane.CH.Framework.Services.Security.Contracts.Message
{
    /// <summary>
    /// Defines credentials submitted by a user in order to logon to a system.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public abstract class AuthenticateRequestBase : 
        OperationRequest
    {
        #region Properties

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>        
        [DataMember()]
        public string UserName
        { get; set; }

        #endregion Properties
    }
}
