using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Apps.UI.Web.AjaxResponse
{
    /// <summary>
    /// Represents the update details to be applied to a client side dom element.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    public class DomUpdate
    {
        #region Properties

        /// <summary>
        /// Gets or sets the dom element id.
        /// </summary>
        [DataMember(Name = "clientId")]
        public string ClientId
        { get; set; }

        /// <summary>
        /// Gets or sets the dom element value.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value
        { get; set; }

        /// <summary>
        /// Gets or sets the dom element type.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type
        { get; set; }

        #endregion Properties
    }
}