using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Represents a web gui container that may or may not contain child controls.
    /// </summary>
    public interface IWebGuiContainer : IGuiContainer
    {
        /// <summary>
        /// Reload event (called upon post backs).
        /// </summary>
        void OnGuiReload();

        /// <summary>
        /// Pre gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        void OnGuiCommandInvoking(WebGuiCommand command);

        /// <summary>
        /// Gui command invocation handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        void OnGuiCommandInvoke(WebGuiCommand command);

        /// <summary>
        /// Post gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        void OnGuiCommandInvoked(WebGuiCommand command);
    }
}