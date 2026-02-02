using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Keane.CH.Framework.DataAccess.Core.Configuration
{
    /// <summary>
    /// Represents the full set of information required to configure a dao.
    /// </summary>
    /// <remarks>
    /// This object is useful in dependency injection scenarios.
    /// </remarks>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class DaoConfigurationSet
    {
        #region Properties

        /// <summary>
        /// Gets or sets the dao config data file path.
        /// </summary>
        [DataMember(IsRequired = false, Order = 2)]
        public string DaoDataFilePath
        { get; set; }

        /// <summary>
        /// Gets the dao config data file.
        /// </summary>
        public FileInfo DaoFile
        { get { return new FileInfo(this.DaoDataFilePath); } }

        #endregion Properties
    }
}
