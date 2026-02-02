using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Security.Contracts.Message
{
    /// <summary>
    /// Authenticcate credentials initialisation response.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public class AuthenticateInitialisationResponse : AuthenticateResponseBase
    { }
}