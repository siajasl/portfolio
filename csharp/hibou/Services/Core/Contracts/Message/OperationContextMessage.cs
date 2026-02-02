using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Core.Operation
{
    /// <summary>
    /// A context message that may be sent along with an operation response.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class OperationContextMessage : 
        EntityBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        [DataMember()]
        public string Text
        { get; set; }

        /// <summary>
        /// Gets or sets the message key for filtering message lists/dictionaries.
        /// </summary>
        [DataMember()]
        public string Key
        { get; set; }

        /// <summary>
        /// Gets or sets the message type, i.e. wanring, informational, critical ... etc.
        /// </summary>
        [DataMember()]
        public OperationContextMessageType Type
        { get; set; }

        /// <summary>
        /// Gets or sets the culture for which the message is targeted.
        /// </summary>
        [DataMember()]
        public string Culture
        { get; set; }

        #endregion Properties
    }
}