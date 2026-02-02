using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.ExtensionMethods
{
    /// <summary>
    /// Encapsulates string extensions.
    /// </summary>
    public static class StringExtensionMethods
    {
        /// <summary>
        /// Converts the passed string to an upper case with underscore.
        /// </summary>
        /// <param name="instance">The string being converted.</param>
        /// <returns>The string converted to uppercase with underscores.</returns>
        public static string ToUpperCaseUnderscore(this string instance)
        {
            // Defensive programming.
            if (string.IsNullOrEmpty(instance))
                return string.Empty;
            instance = instance.Trim();
            if (string.IsNullOrEmpty(instance))
                return string.Empty;

            // Declare work variables.
            StringBuilder result = new StringBuilder();
            string current = default(string);
            string currentUpper = default(string);
            string next = default(string);
            string nextUpper = default(string);
            string previous = default(string);
            string previousUpper = default(string);

            // Construct new representation.
            for (int i = 0; i < instance.Length; i++)
            {
                current = instance.Substring(i, 1);
                currentUpper = current.ToUpperInvariant();
                if (i > 0 &&
                    current.Equals(currentUpper))
                {
                    previous = instance.Substring(i - 1, 1);
                    previousUpper = previous.ToUpperInvariant();
                    if (!previous.Equals(previousUpper))
                    {
                        result.Append("_");
                    }
                    else if (i < instance.Length - 1)
                    {
                        next = instance.Substring(i + 1, 1);
                        nextUpper = next.ToUpperInvariant();
                        if (!next.Equals(nextUpper))
                        {
                            result.Append("_");
                        }
                    }
                }
                result.Append(currentUpper);
            }
            return result.ToString();
        }

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
