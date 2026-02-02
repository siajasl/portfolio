using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Web.ExtensionMethods;
using System.Drawing;
using Keane.CH.Framework.Apps.UI.Web.AjaxResponse;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Encapsulates web gui management.
    /// </summary>
    public sealed class WebGuiController
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
                IWebGuiContainer container = (control as IWebGuiContainer);
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
                IWebGuiContainer container = (control as IWebGuiContainer);
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
                buttonControl.Enabled = true;
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
            WebGuiLoader.Load(control);
        }

        /// <summary>
        /// Recurses all controls & executes the re-load event.
        /// </summary>
        /// <param name="control">A UI control.</param>
        internal static void ReLoad(
            Control control)
        {
            WebGuiLoader.Reload(control);
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
                IWebGuiContainer container = (control as IWebGuiContainer);
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
                textBoxControl.ReadOnly = locked;
                if (locked)
                    textBoxControl.BackColor = Color.Transparent;
                return;
            }
            Button buttonControl = (control as Button);
            if (buttonControl != null)
            {
                buttonControl.Enabled = !locked;
                return;
            }
            DropDownList dropDownListControl = (control as DropDownList);
            if (dropDownListControl != null)
            {
                dropDownListControl.Enabled = !locked;
                return;
            }
            RadioButton radioButtonControl = (control as RadioButton);
            if (radioButtonControl != null)
            {
                radioButtonControl.Enabled = !locked;
                return;
            }
            RadioButtonList radioButtonListControl = (control as RadioButtonList);
            if (radioButtonListControl != null)
            {
                radioButtonListControl.Enabled = !locked;
                return;
            }
            CheckBoxList checkBoxListControl = (control as CheckBoxList);
            if (checkBoxListControl != null)
            {
                checkBoxListControl.Enabled = !locked;
                return;
            }
            CheckBox checkBoxControl = (control as CheckBox);
            if (checkBoxControl != null)
            {
                checkBoxControl.Enabled = !locked;
                return;
            }            
        }

        #endregion State lock

        #region Command invocation

        /// <summary>
        /// Invokes a command across the gui.
        /// </summary>
        /// <param name="control">The control from which the command is to be invoked.</param>
        /// <param name="commandType">The command type.</param>
        public static AjaxResponseData Invoke(
            Control control, GuiCommandType commandType)
        {
            return WebGuiCommandManager.Invoke(control, (int)commandType, null);
        }

        /// <summary>
        /// Invokes a command across the gui.
        /// </summary>
        /// <param name="control">The control from which the command is to be invoked.</param>
        /// <param name="commandType">The command type.</param>
        /// <param name="commandData">The command data.</param>
        public static AjaxResponseData Invoke(
            Control control, GuiCommandType commandType, object commandData)
        {
            return WebGuiCommandManager.Invoke(control, (int)commandType, commandData);
        }

        /// <summary>
        /// Invokes a command across the gui.
        /// </summary>
        /// <param name="control">The control from which the command is to be invoked.</param>
        /// <param name="commandId">The command id.</param>
        public static AjaxResponseData Invoke(
            Control control, int commandId)
        {
            return WebGuiCommandManager.Invoke(control, commandId, null);
        }

        /// <summary>
        /// Invokes a command across the gui.
        /// </summary>
        /// <param name="control">The control from which the command is to be invoked.</param>
        /// <param name="commandId">The command id.</param>
        /// <param name="commandData">The command data.</param>
        public static AjaxResponseData Invoke(
            Control control, int commandId, object commandData)
        {
            return WebGuiCommandManager.Invoke(control, commandId, commandData);
        }

        #endregion Command invocation

        #region Command state

        /// <summary>
        /// Sets the command state across the control and child controls.
        /// </summary>
        /// <param name="control">A UI control.</param>
        /// <param name="commandId">The command id.</param>
        /// <param name="isEnabled">The command enablement state.</param>
        public static void SetCommandState(Control control, int commandId, bool isEnabled)
        {
            WebGuiCommandManager.SetState(control, commandId, isEnabled);
        }

        #endregion Command status
    }
}