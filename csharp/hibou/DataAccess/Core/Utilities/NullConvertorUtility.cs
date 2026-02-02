using System;

namespace Keane.CH.Framework.DataAccess.Core.Utilities
{
    /// <summary>
    /// Converts types to their null representations.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class NullConvertorUtility
    {
        private NullConvertorUtility() { }

        /// <summary>
        /// Replaces null value with DBNull.Value.
        /// </summary>
        public static object GetNullDbValue(string value)
        {
            if (String.IsNullOrEmpty(value))
                return DBNull.Value;
            else
                return value;
        }

        /// <summary>
        /// Replaces null value with DBNull.Value.
        /// </summary>
        public static object GetNullDbValue(double value)
        {
            if (value == 0)
                return DBNull.Value;
            else
                return value;
        }

        /// <summary>
        /// Replaces null value with DBNull.Value.
        /// </summary>
        public static object GetNullDbValue(int value)
        {
            if (value == 0)
                return DBNull.Value;
            else
                return value;
        }
        
        /// <summary>
        /// Replaces null value with DBNull.Value.
        /// </summary>
        public static object GetNullDbValue(DateTime value)
        {
            if (value == DateTime.MinValue)
                return DBNull.Value;
            else
                return value;
        }
    }
}
