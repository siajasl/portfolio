using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Apps.UI.Web.AjaxResponse
{
    /// <summary>
    /// Represents a name value that will be sent back to the client.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    public class NameValuePair
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name
        { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [DataMember(Name = "value")]
        public string Value
        { get; set; }

        #endregion Properties
    }
}
