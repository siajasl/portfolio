using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Core.Utilities.Mail
{
    /// <summary>
    /// Encpasulates all information required to send an email.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Core")]
    public class EmailDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        [DataMember()]
        public string From
        { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        [DataMember()]
        public string To
        { get; set; }

        /// <summary>
        /// Gets or sets the BCC.
        /// </summary>
        [DataMember()]
        public string Bcc
        { get; set; }

        /// <summary>
        /// Gets or sets the CC.
        /// </summary>
        [DataMember()]
        public string CC
        { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [DataMember()]
        public string Subject
        { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        [DataMember()]
        public string Body
        { get; set; }

        #endregion Properties
    }
}
