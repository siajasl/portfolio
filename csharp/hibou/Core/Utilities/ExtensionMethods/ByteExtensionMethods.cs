using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Utilities.ExtensionMethods
{
    /// <summary>
    /// Encapuslate byte related extension methods.
    /// </summary>
    public static class ByteExtensionMethods
    {
        /// <summary>
        /// Determines whether the 2 byte arrays are equivalent.
        /// </summary>
        /// <param name="left">The left array.</param>
        /// <param name="right">The right array.</param>
        /// <returns>True if equivalent.</returns>
        public static bool AreEqualArrays(this byte[] left, byte[] right)
        {
            // True if both are null.
            if ((left ==  null) && (right == null))
                return true;

            // Falise is either is null and the other is not.
            if ((left == null) || (right == null))
                return false;

            // False if there is a length mismatch.
            if (left.Length != right.Length)
                return false;

            // False if there is an array element mismatch.
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                    return false;
            }
            return true;
        }
    }
}
