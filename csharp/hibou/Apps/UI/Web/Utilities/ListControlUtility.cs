using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Keane.CH.Framework.Apps.UI.Web.Utilities
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
            ListControl listControl,
            string value)
        {
            if (listControl == null)
                throw new ArgumentNullException("listControl");
            if (value != null)
            {
                foreach (ListItem listItem in listControl.Items)
                {
                    if (listItem.Value.Trim().Equals(value.Trim()))
                    {
                        listItem.Selected = true;
                        break;
                    }
                }
            }
        }

        #endregion Set value
    }
}
