using System;
using System.Windows.Controls;
using System.Windows.Media;
using Keane.CH.Framework.Apps.UI.WPF.ExtensionMethods;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Encapsulates WPF gui management.
    /// </summary>
    public sealed class WPFGuiController
    {
        #region Collaborator instantiation

        /// <summary>
        /// Recusrively instantiates the control's collaborators.
        /// </summary>
        /// <param name="control">A UI control.</param>
        internal static void InstantiateCollaborators(
            Control control)
        {
            if (control != null)
            {
                // Initialise state.
                IWPFGuiContainer container = (control as IWPFGuiContainer);
                if (container != null)
                {
                    container.OnGuiInjectDependencies();
                }

                // Recurse.
                control.GetChildControlList().ForEach(c => InstantiateCollaborators(c));
            }
        }

        #endregion Collaborator instantiation

        #region State reset

        /// <summary>
        /// Resets the state of the control.
        /// </summary>
        /// <param name="control">A UI control.</param>
        public static void Reset(
            Control control)
        {
            if (control != null)
            {
                // Reset state.
                IWPFGuiContainer container = (control as IWPFGuiContainer);
                if (container != null)
                {
                    container.OnGuiReset();
                    // Auto lock (if supported).
                    if (container.SupportsAutoLocking())
                        Lock(control, true);
                }
                else
                {
                    ResetNative(control);
                }

                // Recurse.
                control.GetChildControlList().ForEach(c => Reset(c));
            }
        }

        /// <summary>
        /// Resets the state of the control.
        /// </summary>
        /// <param name="control">A UI control.</param>
        private static void ResetNative(
            Control control)
        {
            TextBox textBoxControl = (control as TextBox);
            if (textBoxControl != null)
            {
                textBoxControl.Text = String.Empty;
                return;
            }
            Button buttonControl = (control as Button);
            if (buttonControl != null)
            {
                buttonControl.IsEnabled = true;
                return;
            }
        }

        #endregion State reset

        #region State loading

        /// <summary>
        /// Recurses all controls & executes the initial load event.
        /// </summary>
        /// <param name="control">A UI control.</param>
        internal static void Load(
            Control control)
        {
            WPFGuiLoader.Load(control);
        }

        /// <summary>
        /// Recurses all controls & executes the re-load event.
        /// </summary>
        /// <param name="control">A UI control.</param>
        internal static void ReLoad(
            Control control)
        {
            WPFGuiLoader.Reload(control);
        }

        #endregion State loading

        #region State lock

        /// <summary>
        /// Lock the state of the control.
        /// </summary>
        /// <param name="control">A UI control.</param>
        /// <param name="locked">Flag indicating whether the control is to be locked or not.</param>
        public static void Lock(
            Control control, Boolean locked)
        {
            if (control != null)
            {
                // Lock state.
                IWPFGuiContainer container = (control as IWPFGuiContainer);
                if (container != null)
                {
                    container.OnGuiLock(locked);
                }
                else
                {
                    LockNative(control, locked);
                }

                // Recurse.
                control.GetChildControlList().ForEach(c => Lock(c, locked));
            }
        }

        /// <summary>
        /// Locks the passed native control.
        /// </summary>
        /// <param name="control">A UI control.</param>
        /// <param name="locked">Flag indicating whether the gui is to be locked or not.</param>
        private static void LockNative(
            Control control, Boolean locked)
        {
            TextBox textBoxControl = (control as TextBox);
            if (textBoxControl != null)
            {
                textBoxControl.IsReadOnly = locked;
                if (locked)
                    textBoxControl.Background = Brushes.Transparent;
                return;
            }
            Button buttonControl = (control as Button);
            if (buttonControl != null)
            {
                buttonControl.IsEnabled = !locked;
                return;
            }
            ComboBox dropDownListControl = (control as ComboBox);
            if (dropDownListControl != null)
            {
                dropDownListControl.IsEnabled = !locked;
                return;
            }
            RadioButton radioButtonControl = (control as RadioButton);
            if (radioButtonControl != null)
            {
                radioButtonControl.IsEnabled = !locked;
                return;
            }
            CheckBox checkBoxControl = (control as CheckBox);
            if (checkBoxControl != null)
            {
                checkBoxControl.IsEnabled = !locked;
                return;
            }
        }

        #endregion State lock

        #region Command state

        /// <summary>
        /// Sets the command state across the control and child controls.
        /// </summary>
        /// <param name="control">A UI control.</param>
        /// <param name="commandId">The command id.</param>
        /// <param name="isEnabled">The command enablement state.</param>
        public static void SetCommandState(Control control, int commandId, bool isEnabled)
        {
            WPFGuiCommandManager.SetState(control, commandId, isEnabled);
        }

        #endregion Command status
    }
}
