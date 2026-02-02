using System;

namespace Keane.CH.Framework.Core.ExtensionMethods
{
    /// <summary>
    /// Encapsulates date time extensions.
    /// </summary>
    public static class DateTimeExtensionMethods
    {
        /// <summary>
        /// Performs a reduced precision equality test between two dates.
        /// This is necessary because of precision dropping when saving to databases.
        /// </summary>
        /// <param name="dateTimeA">A date time for comparason.</param>
        /// <param name="dateTimeB">A date time for comparason.</param>
        /// <returns>True if the 2 dates are deemed equal according to a reduced level of precision.</returns>
        public static bool IsEquals(
            this DateTime dateTimeA, 
            DateTime dateTimeB)
        {
            bool result = false;
            if (dateTimeA.Equals(dateTimeB))
                result = true;
            else
            {
                if (dateTimeA.Date.Equals(dateTimeB.Date) &&
                    dateTimeA.Hour.Equals(dateTimeB.Hour) &&
                    dateTimeA.Minute.Equals(dateTimeB.Minute) &&
                    dateTimeA.Second.Equals(dateTimeB.Second))
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// Returns a reduced precision value.
        /// This is necessary because of precision dropping when saving to databases.
        /// </summary>
        /// <param name="dateTimeA">A date time for precision dropping.</param>
        /// <returns>A database safe precision datetime.</returns>
        public static DateTime PrecisionSafe(
            this DateTime dateTimeA)
        {
            return new DateTime(
                dateTimeA.Year, 
                dateTimeA.Month, 
                dateTimeA.Day, 
                dateTimeA.Hour, 
                dateTimeA.Minute, 
                dateTimeA.Second, 
                0);
        }
    }
}
