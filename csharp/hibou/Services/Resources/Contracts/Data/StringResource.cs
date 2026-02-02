using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Resources.Contracts
{
    /// <summary>
    /// Represents a string resource within a system.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Resources")]
    [Serializable]
    public class StringResource : ResourceBase<string>
    {
        /// <summary>
        /// Gets the resource type.
        /// </summary>
        public override ResourceType ResourceType
        {
            get { return ResourceType.String; }
        }
    }
}
