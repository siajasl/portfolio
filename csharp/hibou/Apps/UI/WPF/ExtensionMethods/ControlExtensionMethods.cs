using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Keane.CH.Framework.Apps.UI.WPF.ExtensionMethods
{
    /// <summary>
    /// Extension methods over System.Windows.Controls.Control.
    /// </summary>
    public static class ControlExtensionMethods
    {
        /// <summary>
        /// Returns a generic list of the control's child controls.
        /// </summary>
        /// <param name="control">The control in question.</param>
        /// <typeparam name="T">The type of child controls to be filtered.</typeparam>
        public static List<Control> GetChildControlList<T>(
            this Control control)
        {
            return control.GetChildControlList<T>(false);
        }

        /// <summary>
        /// Returns a generic list of the control's child controls.
        /// </summary>
        /// <param name="control">The control in question.</param>
        /// <param name="recurse">Flag indicating whether recursion will occur or not.</param>
        /// <typeparam name="T">The type of child controls to be filtered.</typeparam>
        public static List<Control> GetChildControlList<T>(
            this Control control, bool recurse)
        {
            List<Control> result = new List<Control>();
            List<Control> unfiltered = new List<Control>();
            control.BuildChildControlList(unfiltered, recurse);
            foreach (Control item in unfiltered)
            {
                if (item is T)
                    result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Returns a generic list of the control's child controls.
        /// </summary>
        /// <param name="control">The control in question.</param>
        public static List<Control> GetChildControlList(
            this Control control)
        {
            return control.GetChildControlList(false);
        }

        /// <summary>
        /// Returns a generic list of the control's child controls.
        /// </summary>
        /// <param name="control">The control in question.</param>
        /// <param name="recurse">Flag indicating whether recursion will occur or not.</param>
        public static List<Control> GetChildControlList(
            this Control control, bool recurse)
        {
            List<Control> result = new List<Control>();
            control.BuildChildControlList(result, recurse);
            return result;
        }

        /// <summary>
        /// Recursively derives a flattened list of child controls hosted upon a control.
        /// </summary>
        /// <param name="control">The control in question.</param>
        /// <param name="controlList">The list of controls.</param>
        /// <param name="recurse">Flag indicating whether recursion will occur or not.</param>
        public static void BuildChildControlList(
            this Control control, List<Control> controlList, bool recurse)
        {
            // Initialise the control list.
            if (controlList == null)
                controlList = new List<Control>();

            // Iterate child controls & add each child to the list.
            if (control != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(control); i++)
                {
                    Visual childControl = (Visual)VisualTreeHelper.GetChild(control, i);
                    if (childControl is Control)
                    {
                        controlList.Add((Control)childControl);
                        // Recurse if instructed.
                        if (recurse)
                        {
                            ((Control)childControl).BuildChildControlList(controlList, recurse);
                        }
                    }
                }
            }
        }

    }
}
