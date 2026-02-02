using System;
using System.Web.UI;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Web;
using System.Web.UI.WebControls;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Represents a base master page from which all master pages will inherit.
    /// </summary>
    /// <remarks>
    /// This exposes common functionality & services to all pages/user controls within an application.
    /// </remarks>
    public abstract class WebMasterPageBase : 
        MasterPage,
        IWebGuiContainer
    {
        #region Properties

        /// <summary>
        /// Gets a pointer to the base page.
        /// </summary>
        public WebPageBase BasePage
        {
            get { return WebPageBase.DerivePage(this); }
        }

        /// <summary>
        /// Gets or sets the user control services.
        /// </summary>
        public WebPageBaseSettings Settings
        {
            get { return BasePage.Settings; }
        }

        /// <summary>
        /// Gets a placeholder control for caching hidden values.
        /// </summary>
        public abstract PlaceHolder PlaceHolderControl
        { get; }

        #endregion Properties

        #region IWebGuiContainer Members

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
        /// Container reset event.
        /// </summary>
        public virtual void OnGuiReset()
        { }

        /// <summary>
        /// Sets the container's user input state so as to prevent/allow user interaction.
        /// </summary>
        /// <param name="locked">Flag indicating whether the gui will be locked for user input.</param>
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
        public virtual void OnGuiCommandInvoking(WebGuiCommand command)
        { }

        /// <summary>
        /// Gui command invocation handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        public virtual void OnGuiCommandInvoke(WebGuiCommand command)
        { }

        /// <summary>
        /// Post gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        public virtual void OnGuiCommandInvoked(WebGuiCommand command)
        { }

        #endregion Events

        #endregion IWebGuiContainer Members
    }
}