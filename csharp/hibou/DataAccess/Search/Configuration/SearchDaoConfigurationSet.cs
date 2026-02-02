using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.DataAccess.Core.Configuration;
using System.IO;

namespace Keane.CH.Framework.DataAccess.Search.Configuration
{
    /// <summary>
    /// Represents the full set of information required to configure a search dao.
    /// </summary>
    /// <remarks>
    /// This object is useful in dependency injection scenarios.
    /// </remarks>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class SearchDaoConfigurationSet
    {
        #region Properties

        /// <summary>
        /// Gets or sets the dao config data file path.
        /// </summary>
        [DataMember(IsRequired = false, Order = 2)]
        public string DaoFilePath
        { get; set; }

        /// <summary>
        /// Gets the dao config data file.
        /// </summary>
        public FileInfo DaoFile
        { get { return new FileInfo(this.DaoFilePath); } }

        /// <summary>
        /// Gets or sets the search dao config data file path.
        /// </summary>
        [DataMember(IsRequired = false, Order = 3)]
        public string SearchDaoFilePath
        { get; set; }

        /// <summary>
        /// Gets the search config file.
        /// </summary>
        public FileInfo SearchDaoFile
        { get { return new FileInfo(this.SearchDaoFilePath); } }

        /// <summary>
        /// Gets or sets the default search dao config data file path.
        /// </summary>
        [DataMember(IsRequired = false, Order = 4)]
        public string DefaultSearchDaoFilePath
        { get; set; }

        /// <summary>
        /// Gets the default search config file.
        /// </summary>
        public FileInfo DefaultSearchDaoFile
        { 
            get 
            {
                if (string.IsNullOrEmpty(this.DefaultSearchDaoFilePath))
                    return null;
                else
                    return new FileInfo(this.DefaultSearchDaoFilePath); 
            } 
        }

        #endregion Properties
    }
}