using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Apps.UI.Web.AjaxResponse
{
    /// <summary>
    /// Encapsulates request processing exception details sent back to the client.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    public class ProcessingException
    {
        #region Properties

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message
        { get; set; }

        /// <summary>
        /// Gets or sets the exception severity.
        /// </summary>
        [DataMember(Name = "severity")]
        public ProcessingExceptionSeverityType Severity
        { get; set; }

        #endregion Properties
    }
}
