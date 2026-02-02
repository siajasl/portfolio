using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Search.Contracts.Data
{
    /// <summary>
    /// Enumeration over text search starting points.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Search")]
    [Serializable]
    public enum TextSearchStartPointType
    {
        /// <summary>
        /// The search target is anywhere within the target text.
        /// </summary>
        [EnumMember()]
        Anywhere = 1,
        /// <summary>
        /// The search target is from the beginning of the target text.
        /// </summary>
        [EnumMember()]
        Begin = 2,
    }
}