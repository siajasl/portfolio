using System;
using System.Windows.Controls;
using Keane.CH.Framework.Apps.UI.WPF.ExtensionMethods;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Encapsulates WPF gui event publishing.
    /// </summary>
    internal sealed class WPFGuiCommandManager
    {
        #region Command invocation

        /// <summary>
        /// Performs the pre-invocation.
        /// </summary>
        /// <param name="target">The command target.</param>
        /// <param name="command">The command being invoked.</param>
        private static void DoPreInvoke(
            Control target, WPFGuiCommand command)
        {
            if ((target != null) &&
                (!command.IsCancelled))
            {
                // Invoke container method.
                IWPFGuiContainer container = (target as IWPFGuiContainer);
                if (container != null)
                {
                    container.OnGuiCommandInvoking(command);
                }

                // Recurse.
                if (!command.IsCancelled)
                    target.GetChildControlList().ForEach(c => DoPreInvoke(c, command));
            }
        }

        /// <summary>
        /// Performs the invocation.
        /// </summary>
        /// <param name="target">The command target.</param>
        /// <param name="command">The command being invoked.</param>
        private static void DoInvoke(
            Control target, WPFGuiCommand command)
        {
            if ((target != null) &&
                (!command.IsCancelled))
            {
                // Invoke container method.
                IWPFGuiContainer container = (target as IWPFGuiContainer);
                if (container != null)
                {
                    container.OnGuiCommandInvoke(command);
                }

                // Recurse.
                target.GetChildControlList().ForEach(c => DoInvoke(c, command));
            }
        }

        /// <summary>
        /// Performs the post-invocation.
        /// </summary>
        /// <param name="target">The command target.</param>
        /// <param name="command">The command being invoked.</param>
        private static void DoPostInvoke(
            Control target, WPFGuiCommand command)
        {
            if ((target != null) &&
                (!command.IsCancelled))
            {
                // Invoke container method.
                IWPFGuiContainer container = (target as IWPFGuiContainer);
                if (container != null)
                {
                    container.OnGuiCommandInvoked(command);
                }

                // Recurse.
                target.GetChildControlList().ForEach(c => DoPostInvoke(c, command));
            }
        }

        #endregion Command invocation

        #region State assignment

        /// <summary>
        /// Invokes a command across a gui.
        /// </summary>
        /// <param name="control">The invocation control.</param>
        /// <param name="commandId">The command id.</param>
        /// <param name="commandData">The command data.</param>
        internal static void SetState(
            Control control, int commandId, bool isEnabled)
        {
            // Defensive programming.
            if (control == null)
                throw new ArgumentNullException("An command can only be invoked by an identifiable source.");
            WPFWindowBase window = WPFWindowBase.DeriveWindow(control);
            if (window == null)
                throw new ArgumentNullException("Command invocation is only supported for windows inheriting from WPFWindowBase.");

            // Instantiate command.
            WPFGuiCommand command = new WPFGuiCommand();
            command.Id = commandId;
            command.IsEnabled = isEnabled;
            command.Context = window.Settings.CreateContext();

            // Set the state of the command across the whole gui.
            DoSetState(window, command);
        }

        /// <summary>
        /// Performs the state assignement.
        /// </summary>
        /// <param name="target">The command target.</param>
        /// <param name="command">The command being invoked.</param>
        private static void DoSetState(
            Control target, WPFGuiCommand command)
        {
            if (target != null)
            {
                // Set state.
                IWPFGuiContainer container = (target as IWPFGuiContainer);
                if (container != null)
                {
                    container.OnGuiSetCommandState(command);
                }

                // Recurse.
                target.GetChildControlList().ForEach(c => DoSetState(c, command));
            }
        }

        #endregion State assignment
    }
}
