using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Enumeration over the different aspects of a system.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jul-2008" />
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public enum SystemLayerType 
    {
        /// <summary>
        /// Application layer.
        /// </summary>
        [EnumMember()]
        Application = 1,

        /// <summary>
        /// Servcies layer.
        /// </summary>
        [EnumMember()]
        Services = 2,

        /// <summary>
        /// Data Access layer.
        /// </summary>
        [EnumMember()]
        DataAccess = 3,

        /// <summary>
        /// External layer (e.g. an api or web-service call).
        /// </summary>
        [EnumMember()]
        External = 4,
    }
}