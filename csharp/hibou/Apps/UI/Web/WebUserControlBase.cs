using System.Web.UI;
using System.Web.UI.WebControls;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Represents a base user control from which all user controls will inherit.
    /// </summary>
    /// <remarks>
    /// This exposes common functionality & services to all user controls within an application.
    /// </remarks>
    public abstract class WebUserControlBase : 
        UserControl,
        IWebGuiContainer
    {
        #region Properties

        /// <summary>
        /// Gets a pointer to the base page upon which the control is hosted.
        /// </summary>
        public WebPageBase BasePage
        {
            get { return WebPageBase.DerivePage(this); }
        }

        /// <summary>
        /// Gets a pointer to the base master page upon which the control is hosted.
        /// </summary>
        public WebMasterPageBase BaseMasterPage
        {
            get { return WebPageBase.DeriveMasterPage(this); }
        }
        
        /// <summary>
        /// Gets the associated gui services.
        /// </summary>
        public WebPageBaseSettings Settings
        {
            get { return BasePage.Settings; }
        }

        /// <summary>
        /// Gets or sets the edit mode under which the user control will be used.
        /// </summary>
        public EditModeType EditMode
        { get; set; }

        /// <summary>
        /// Gets a placeholder control for caching hidden values.
        /// </summary>
        public PlaceHolder PlaceHolderControl
        { get { return BaseMasterPage.PlaceHolderControl; } }

        #endregion Properties

        #region IWebGuiContainer Members

        #region Standard methods

        /// <summary>
        /// Placeholder for the poitn at which collaborator injection takes place.
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
        public virtual void OnGuiLock(bool locked)
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