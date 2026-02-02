using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace Keane.CH.Framework.Apps.UI.WPF.Utilities
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
            TextBox textControl,
            object value)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            SetText(textControl, value, string.Empty, null);
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        public static void SetText(
            Label textControl,
            object value)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            SetText(textControl, value, string.Empty, null);
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        /// <param name="nullValue">The null value to use.</param>
        public static void SetText(
            TextBox textControl,
            object value,
            object nullReplacementValue)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            SetText(textControl, value, nullReplacementValue, null);
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        /// <param name="nullValue">The null value to use.</param>
        public static void SetText(
            Label textControl,
            object value,
            object nullReplacementValue)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            SetText(textControl, value, nullReplacementValue, null);
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        /// <param name="nullValue">The null value to use.</param>
        public static void SetText(
            TextBox textControl,
            object value,
            object nullReplacementValue,
            object nullPlaceholder)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            SetText(textControl, value, nullReplacementValue, nullPlaceholder);
        }
        
        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">The value to be assigned to the text control.</param>
        /// <param name="nullValue">The null value to use.</param>
        public static void SetText(
            Label textControl,
            object value,
            object nullReplacementValue,
            object nullPlaceholder)
        {
            // Defensive programming.
            Debug.Assert(textControl != null, "textControl is null");

            SetText(textControl, value, nullReplacementValue, nullPlaceholder);
        }

        /// <summary>
        /// Sets the text value of a label.
        /// </summary>
        /// <param name="textControl">The text control in question.</param>
        /// <param name="value">A value to be assigned to the text control.</param>
        /// <param name="nullReplacementValue">A value replacing a null.</param>
        /// <param name="nullPlaceholder">A value acting as a null.</param>
        private static void SetText(
            Control textControl,
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

            // Filter out null values (but not null in the db).
            if (value.Equals(nullPlaceholder) &&
                nullReplacementValue != null)
            {
                textControlValue = nullReplacementValue.ToString();
            }
            else
            {
                textControlValue = value.ToString().Trim();
            }

            // Assign the string to the text control.            
            if (textControl.GetType() == typeof(TextBox))
            {
                ((TextBox)textControl).Text = textControlValue;
            }
            else if (textControl.GetType() == typeof(Label))
                ((Label)textControl).Content = textControlValue;
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
            TextBox textControl)
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
            TextBox textControl,
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
