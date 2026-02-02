using System;

namespace Keane.CH.Framework.DataAccess.Core.Utilities
{
    /// <summary>
    /// Encapsulates Oracle related utilty functions.
    /// </summary>
    public static class OracleUtility
    {
        /// <summary>
        /// Returns a clob safe string.
        /// </summary>
        /// <param name="value">The string to be parsed for clob safety.</param>
        /// <returns>A clob safe string.</returns>
        public static bool IsClobSafeString(string value)
        {
            // N.B. the number 1973 has been determined via functional testing.
            // Strings longer than this will cause the buffer size to be > 32kb 
            // thus firing an Oracle Exception - see oracle error ORA-01460.
            if (value.Length > 1973)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Returns a clob safe string.
        /// </summary>
        /// <param name="value">The string to be parsed for clob safety.</param>
        /// <returns>A clob safe string.</returns>
        public static string GetClobSafeString(string value)
        {
            if (IsClobSafeString(value))
                return value;
            else
                return String.Empty;
        }
    }
}
