using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Utilities.ExtensionMethods
{
    /// <summary>
    /// Encapsulates string extensions.
    /// </summary>
    public static class StringExtensionMethods
    {
        /// <summary>
        /// Extension method to perform a parse over a file path.
        /// </summary>
        public static string ParseFilePath(
            this string filePath)
        {
            const string SEPARATOR = "\\"; ;
            return filePath.Replace("\\\\", SEPARATOR);
        }

        /// <summary>
        /// Performs a string equailty test that also returns true if both are null
        /// </summary>
        /// <param name="stringA">A string for comparason.</param>
        /// <param name="stringB">A string for comparason.</param>
        public static bool IsNullablyEqual(
            this string stringA, string stringB)
        {
            bool result = false;
            if (stringA == null && stringB == null)
                result = true;
            else if (stringA != null && stringB != null)
                result = stringA.Equals(stringB);
            return result;
        }
    }
}
