using System;
using System.Windows;
using System.Windows.Controls;
using Keane.CH.Framework.Apps.UI.Core;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Represents a base window from which all WPF windows will inherit.
    /// </summary>
    /// <remarks>
    /// This exposes common functionality & services to all windows/user controls within an application.
    /// </remarks>
    public class WPFWindowBase :
        Window,
        IWPFGuiContainer
    {
        #region Ctor

        public WPFWindowBase() 
            : base()
        {
            Settings = new WPFWindowBaseSettings(this);
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the standard window settings.
        /// </summary>
        public WPFWindowBaseSettings Settings
        { get; private set; }

        #endregion Properties

        #region System.Windows.Window overrides

        /// <summary>
        /// Window on-initialized event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            PreInitialise();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            WPFGuiController.InstantiateCollaborators(this);
            WPFGuiController.Reset(this);
            WPFGuiController.Load(this);
        }

        #endregion System.Windows.Window overrides

        #region Private methods

        /// <summary>
        /// Performs standard window pre-initialisation routines.
        /// </summary>
        private void PreInitialise()
        {
            // Assign the window title.
            if (!string.IsNullOrEmpty(Settings.ClientName))
                this.Title = Settings.ClientName;
        }

        #endregion Private methods

        #region IWPFGuiContainer Members

        #region Standard methods

        /// <summary>
        /// Collaborator instantiation event.
        /// </summary>
        public virtual void OnGuiInjectDependencies()
        { }

        /// <summary>
        /// Pre initial load event (fired the first time the gui is loaded).
        /// </summary>
        /// <remarks>
        /// Occurs the first time the view is rendered.
        /// </remarks>
        public virtual void OnGuiLoading()
        { }

        /// <summary>
        /// Initial load event.
        /// </summary>
        /// <remarks>
        /// Occurs after state initialisation.
        /// </remarks>
        public virtual void OnGuiLoad()
        { }

        /// <summary>
        /// Post initial load event (fired the first time the gui is loaded).
        /// </summary>
        public virtual void OnGuiLoaded()
        { }

        /// <summary>
        /// Resets the container.
        /// </summary>
        public virtual void OnGuiReset()
        { }

        /// <summary>
        /// Sets the container's user input state so as to prevent/allow user interaction.
        /// </summary>
        /// <param name="locked">Flag indicating whether the view will be locked for user input.</param>
        public virtual void OnGuiLock(
            bool locked)
        { }

        /// <summary>
        /// Sets the command state across the container.
        /// </summary>
        /// <param name="command">The command.</param>
        public virtual void OnGuiSetCommandState(GuiCommand command)
        { }

        /// <summary>
        /// Reload event (called upon post backs).
        /// </summary>
        public virtual void OnGuiReload()
        { }

        #endregion Standard methods

        #region Events

        /// <summary>
        /// Pre gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        public virtual void OnGuiCommandInvoking(WPFGuiCommand command)
        { }

        /// <summary>
        /// Gui command invocation handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        public virtual void OnGuiCommandInvoke(WPFGuiCommand command)
        { }

        /// <summary>
        /// Post gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        public virtual void OnGuiCommandInvoked(WPFGuiCommand command)
        { }

        #endregion Events

        #endregion IWPFGuiContainer Members

        #region Static methods

        /// <summary>
        /// Derives the WPF window view base from an object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The window.</returns>
        internal static WPFWindowBase DeriveWindow(
            object source)
        {
            WPFWindowBase result = null;
            Control control = source as Control;
            if (control != null)
                result = control.Parent as WPFWindowBase;
            if (result == null)
                throw new WPFGuiContainerInheritanceException();
            return result;
        }

        /// <summary>
        /// Derives the root window control from the passed control object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The root window control.</returns>
        internal static Control DeriveControl(
            object source)
        {
            Control result = source as Control;
            if (result != null)
                result = result.Parent as Control;
            return result;
        }

        #endregion Static methods
    }
}
