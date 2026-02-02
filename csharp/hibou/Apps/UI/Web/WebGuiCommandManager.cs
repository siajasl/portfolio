using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Keane.CH.Framework.Apps.UI.Web.ExtensionMethods;
using Keane.CH.Framework.Apps.UI.Web.AjaxResponse;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Encapsulates web gui event publishing.
    /// </summary>
    internal sealed class WebGuiCommandManager
    {
        #region Command invocation

        /// <summary>
        /// Invokes a command across a gui.
        /// </summary>
        /// <param name="control">The invocation control.</param>
        /// <param name="commandId">The command id.</param>
        /// <param name="commandData">The command data.</param>
        internal static AjaxResponseData Invoke(
            Control control, int commandId, object commandData)
        {
            // Defensive programming.
            if (control == null)
                throw new ArgumentNullException("An command can only be invoked by an identifiable source.");
            WebPageBase page = WebPageBase.DerivePage(control);
            if (page == null)
                throw new ArgumentNullException("Command invocation is only supported for pages inheriting from WebPageBase.");

            // Instantiate command.
            WebGuiCommand command = new WebGuiCommand();
            command.Id = commandId;
            command.Data = commandData;
            command.Context = page.Settings.CreateContext();

            // 3 phase invocation (each phase permitted to cancel).
            DoPreInvoke(page, command);
            if (!command.IsCancelled)
            {
                DoInvoke(page, command);
                if (!command.IsCancelled)
                    DoPostInvoke(page, command);
            }

            // Return event response.
            return command.AjaxResponseData;
        }

        /// <summary>
        /// Performs the pre-invocation.
        /// </summary>
        /// <param name="target">The command target.</param>
        /// <param name="command">The command being invoked.</param>
        private static void DoPreInvoke(
            Control target, WebGuiCommand command)
        {
            if ((target != null) &&
                (!command.IsCancelled))
            {
                // Invoke container method.
                IWebGuiContainer container = (target as IWebGuiContainer);
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
            Control target, WebGuiCommand command)
        {
            if ((target != null) &&
                (!command.IsCancelled))
            {
                // Invoke container method.
                IWebGuiContainer container = (target as IWebGuiContainer);
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
            Control target, WebGuiCommand command)
        {
            if ((target != null) &&
                (!command.IsCancelled))
            {
                // Invoke container method.
                IWebGuiContainer container = (target as IWebGuiContainer);
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
            WebPageBase page = WebPageBase.DerivePage(control);
            if (page == null)
                throw new ArgumentNullException("Command invocation is only supported for pages inheriting from WebPageBase.");

            // Instantiate command.
            WebGuiCommand command = new WebGuiCommand();
            command.Id = commandId;
            command.IsEnabled = isEnabled;
            command.Context = page.Settings.CreateContext();

            // Set the state of the command across the whole gui.
            DoSetState(page, command);
        }

        /// <summary>
        /// Performs the state assignement.
        /// </summary>
        /// <param name="target">The command target.</param>
        /// <param name="command">The command being invoked.</param>
        private static void DoSetState(
            Control target, WebGuiCommand command)
        {
            if (target != null) 
            {
                // Set state.
                IWebGuiContainer container = (target as IWebGuiContainer);
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