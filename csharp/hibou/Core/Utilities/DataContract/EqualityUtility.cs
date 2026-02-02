using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace Keane.CH.Framework.Core.Utilities.DataContract
{
    /// <summary>
    /// Encapsulates data contract equality utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class EqualityUtility
    {
        #region Ctor.

        private EqualityUtility() { }

        #endregion Ctor.

        #region Methods

        /// <summary>
        /// Overrides the equals method using a serialization mechanism to perform the comparason.
        /// </summary>
        /// <param name="instanceA">An object.</param>
        /// <param name="instanceB">Another object.</param>
        /// <returns>True if the entites are deemed equal.</returns>
        public static bool AreEqual(
            object instanceA, object instanceB)
        {
            if (instanceA == null)
                throw new ArgumentNullException("instanceA");
            if (instanceB == null)
                throw new ArgumentNullException("instanceB");
            string instanceAAsString = 
                SerializationUtility.SerializeToString(instanceA);
            string instanceBAsString = 
                SerializationUtility.SerializeToString(instanceB);
            return instanceAAsString.Equals(instanceBAsString);
        }

        #endregion Equality
    }
}
