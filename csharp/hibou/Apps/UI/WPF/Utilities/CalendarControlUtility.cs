using System;
using System.Windows.Controls;

namespace Keane.CH.Framework.Apps.UI.WPF.Utilities
{
    /// <summary>
    /// Encapsulates calendarControl utility methods.
    /// </summary>
    public sealed class CalendarControlUtility
    {
        #region Set text (unformatted)

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="calendarControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        public static void SetText(
            TextBox calendarControl,
            object value)
        {
            SetText(calendarControl, value, null, null);
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="calendarControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        /// <param name="nullValue">The null value to use.</param>
        public static void SetText(
            TextBox calendarControl,
            object value,
            object nullReplacementValue)
        {
            SetText(calendarControl, value, nullReplacementValue, null);
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="calendarControl">The text control in question.</param>
        /// <param name="value">A value to be assigned to the text control.</param>
        /// <param name="nullReplacementValue">A value replacing a null.</param>
        /// <param name="nullPlaceholder">A value acting as a null.</param>
        public static void SetText(
            TextBox calendarControl,
            object value,
            object nullReplacementValue,
            object nullPlaceholder)
        {
            if (calendarControl == null)
                throw new ArgumentNullException("calendarControl");

            // Assign default value.
            DateTime datetimeControlValue = DateTime.Now;

            // If either null or the null placeholder then use the null replacement value.
            if (value == null ||
               (nullPlaceholder != null && value.Equals(nullPlaceholder)))
            {
                if (nullReplacementValue != null)
                {
                    DateTime result;
                    if (!DateTime.TryParse(nullReplacementValue.ToString(), out result))
                    {
                        result = DateTime.Now;
                    }
                    datetimeControlValue = result;
                }
            }
            // Otherwise use the passed value.
            else
            {
                datetimeControlValue = DateTime.Parse(value.ToString());
            }

            // Assign the string ot the text control.
            calendarControl.Text = datetimeControlValue.ToString();
        }

        #endregion Set text (unformatted)
    }
}
