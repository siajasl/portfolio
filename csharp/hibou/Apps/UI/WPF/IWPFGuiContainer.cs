using Keane.CH.Framework.Apps.UI.Core;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Represents a WPF gui container that may or may not contain child controls.
    /// </summary>
    public interface IWPFGuiContainer : IGuiContainer
    {
        /// <summary>
        /// Reload event (called upon post backs).
        /// </summary>
        void OnGuiReload();

        /// <summary>
        /// Pre gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        void OnGuiCommandInvoking(WPFGuiCommand command);

        /// <summary>
        /// Gui command invocation handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        void OnGuiCommandInvoke(WPFGuiCommand command);

        /// <summary>
        /// Post gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        void OnGuiCommandInvoked(WPFGuiCommand command);
    }
}
