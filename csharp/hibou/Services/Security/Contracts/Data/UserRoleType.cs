using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Security.Contracts.Message
{
    /// <summary>
    /// An user role type - admin, user ..etc.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Security")]
    public class UserRoleType : 
        EntityBase
    {
        #region Properties

        /// <summary>
        /// The user role name.
        /// </summary>        
        [DataMember()]
        public string Name
        { get; set; }

        /// <summary>
        /// The user role access level.
        /// </summary>        
        [DataMember()]
        public int AccessLevel
        { get; set; }

        #endregion Properties
    }
}
