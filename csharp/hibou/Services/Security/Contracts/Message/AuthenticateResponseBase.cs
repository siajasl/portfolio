using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Core.Operation;

namespace Keane.CH.Framework.Services.Security.Contracts.Message
{
    /// <summary>
    /// Defines a base class for authentication responses.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public abstract class AuthenticateResponseBase : 
        OperationResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets the authenticated user profile (minue credentials information).
        /// </summary>        
        [DataMember()]
        public User User
        { get; set; }

        #endregion Properties
    }
}
