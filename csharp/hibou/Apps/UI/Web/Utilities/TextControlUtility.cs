using System;
using System.Web;
using System.Web.UI;
using System.Diagnostics;
using System.Web.UI.WebControls;

namespace Keane.CH.Framework.Apps.UI.Web.Utilities
{
    /// <summary>
    /// Encapsulates TextControl utility methods.
    /// </summary>
    public sealed class TextControlUtility
    {
        #region Set text (unformatted)

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        public static void SetText(
            ITextControl textControl, 
            object value)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            if (value != null && !value.Equals(String.Empty))
                SetText(textControl, value, null, null);
            else
            {
                if (textControl is Label)
                    textControl.Text = "&nbsp;";
                else
                    textControl.Text = string.Empty;
            }
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        /// <param name="nullValue">The null value to use.</param>
        public static void SetText(
            ITextControl textControl, 
            object value,
            object nullReplacementValue)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            if ((value == null || value.Equals(String.Empty)) && nullReplacementValue != null)
                textControl.Text = nullReplacementValue.ToString();
            else if (value != null)
                SetText(textControl, value, nullReplacementValue, null);
            else
                textControl.Text = string.Empty;
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">A value to be assigned to the text control.</param>
        /// <param name="nullReplacementValue">A value replacing a null.</param>
        /// <param name="nullPlaceholder">A value acting as a null.</param>
        private static void SetText(
            ITextControl textControl,
            object value,
            object nullReplacementValue,
            object nullPlaceholder)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            if (textControl == null)
                throw new ArgumentNullException("textControl");

            // Assign default value.
            string textControlValue = String.Empty;
            int v = 0;
            if (Int32.TryParse(value.ToString(), out v))
            {
                // Filter out null integer values (but not null in the db).
                if (v == Int32.MinValue && 
                    nullReplacementValue != null)
                {
                    textControlValue = nullReplacementValue.ToString();        
                }
                // Filter out integers with leading zeros.
                else if (value.ToString().Length > 1
                    && value.ToString().Substring(0, 1) == "0")
                {
                    textControlValue = value.ToString().Trim();
                }
                // Otherwise assign the interger value.
                else
                {
                    textControlValue = v.ToString();
                }
            }
            else
            {
                textControlValue = value.ToString().Trim();
            }

            // Assign the string to the text control.            
            textControl.Text = textControlValue;
        }

        #endregion Set text (unformatted)

        #region Get text

        /// <summary>
        /// Gets a safely encoded text value of a text control.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <returns>The passed text control value safely encoded.</returns>
        /// <remarks>This return a null if the string is empty.</remarks>
        public static string GetText(
            ITextControl textControl)
        {
            return GetText(textControl, true);
        }

        /// <summary>
        /// Gets a safely encoded text value of a text control.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="returnNullWhenEmpty">A flag indicating whether a null value wil be returned when the text box is empty.</param>
        /// <returns>The passed text control value safely encoded.</returns>
        public static string GetText(
            ITextControl textControl,
            bool returnNullWhenEmpty)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            // Encode text (protects against hacking).
            string result = string.Empty;
            if (!string.IsNullOrEmpty(textControl.Text))
            {
                result = textControl.Text.Trim();
            }
            else if (returnNullWhenEmpty)
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Gets a safely encoded text value.
        /// </summary>
        /// <param name="textControl">The text in question.</param>
        /// <returns>The passed text safely encoded.</returns>
        /// <remarks>This return a null if the string is empty.</remarks>
        public static string GetText(
            string text)
        {
            return GetText(text, true);
        }

        /// <summary>
        /// Gets a safely encoded text value.
        /// </summary>
        /// <param name="textControl">The text in question.</param>
        /// <param name="returnNullWhenEmpty">A flag indicating whether a null value wil be returned when the text box is empty.</param>
        /// <returns>The passed text safely encoded.</returns>
        public static string GetText(
            string text,
            bool returnNullWhenEmpty)
        {
            // Encode text (protects against hacking).
            string result = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                result = text.Trim();
            }
            else if (returnNullWhenEmpty)
            {
                result = null;
            }
            return result;
        }

        #endregion Get text
    }
}
