using System;
using System.Windows.Controls;

namespace Keane.CH.Framework.Apps.UI.WPF.Utilities
{
    /// <summary>
    /// Encapsulates list control utility methods.
    /// </summary>
    public sealed class ListControlUtility
    {
        #region Set value

        /// <summary>
        /// Sets the selected value of a list control.
        /// </summary>
        /// <param name="listControl">The list control in question.</param>
        /// <param name="value">The value that will be selected in the control.</param>
        public static void SetSelectedValue(
            ListBox listControl,
            string value)
        {
            if (listControl == null)
                throw new ArgumentNullException("listControl");
            if (value != null)
            {
                listControl.SelectedValue = value;
            }
        }

        #endregion Set value
    }
}
