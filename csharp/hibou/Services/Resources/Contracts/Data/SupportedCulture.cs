using System.Runtime.Serialization;
using System;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Resources.Contracts.Data
{
    /// <summary>
    /// Repersents a culture supported by an application.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Resources")]
    [Serializable]
    public class SupportedCulture : 
        EntityBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the descritpion.
        /// </summary>        
        [DataMember()]
        public string Description
        { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [DataMember()]
        public string Code
        { get; set; }

        #endregion Properties
    }
}