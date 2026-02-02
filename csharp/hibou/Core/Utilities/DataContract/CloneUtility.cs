using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Json;
using Keane.CH.Framework.Core.Utilities.DataContract;

namespace Keane.CH.Framework.Core.Utilities.DataContract
{
    /// <summary>
    /// Encapsulates cloning utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class CloneUtility
    {
        #region Ctor.

        private CloneUtility() { }

        #endregion Ctor.

        #region Cloning

        /// <summary>
        /// Clones the via text.
        /// </summary>
        /// <typeparam name="E">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be cloned.</param>
        /// <returns>A cloned instance of the data contract type.</returns>
        public static object CloneViaText(object dataContract)
        {
            string serialized = SerializationUtility.SerializeToString(dataContract);
            return DeserializationUtility.DeserializeFromString(serialized, dataContract.GetType());
        }

        /// <summary>
        /// Clones the via text.
        /// </summary>
        /// <typeparam name="E">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be cloned.</param>
        /// <returns>A cloned instance of the data contract type.</returns>
        public static object CloneViaByteArray(object dataContract)
        {
            byte[] serialized = SerializationUtility.SerializeToByteArray(dataContract, dataContract.GetType());
            return DeserializationUtility.DeserializeFromByteArray(serialized, dataContract.GetType());
        }

        #endregion Cloning
    }
}
