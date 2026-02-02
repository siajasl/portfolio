using System;
using System.Web;
using System.Web.UI;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Web;
using Keane.CH.Framework.Apps.UI.Web.State;
using Keane.CH.Framework.Apps.UI.Web.Specialized;
using System.Web.UI.WebControls;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Represents a base page from which all web pages will inherit.
    /// </summary>
    /// <remarks>
    /// This exposes common functionality & services to all pages/user controls within an application.
    /// </remarks>
    public abstract class WebPageBase :
        System.Web.UI.Page,
        IWebGuiContainer
    {
        #region Ctor

        public WebPageBase() 
            : base()
        {
            Settings = new WebPageBaseSettings(this);
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets a pointer to the base master page upon which the control is hosted.
        /// </summary>
        public WebMasterPageBase BaseMasterPage
        {
            get { return WebPageBase.DeriveMasterPage(this); }
        }

        /// <summary>
        /// Gets or sets the standard page settings.
        /// </summary>
        public WebPageBaseSettings Settings
        { get; private set; }

        /// <summary>
        /// Gets whether the page is simply a navigation placeholder page.
        /// </summary>
        private bool IsNavigationPlaceholderPage
        {
            get
            {
                WebNavigationPlaceholderPage containerPage =
                    this as WebNavigationPlaceholderPage;
                return (containerPage != null);
            }
        }

        /// <summary>
        /// Gets the page specific site map provider.
        /// </summary>
        public virtual string PageSiteMapProvider
        { get { return String.Empty; } }

        /// <summary>
        /// Gets a placeholder control for caching hidden values.
        /// </summary>
        public PlaceHolder PlaceHolderControl
        { get { return BaseMasterPage.PlaceHolderControl; } }

        #endregion Properties

        #region System.Web.UI.Page overrides

        /// <summary>
        /// Page pre-initialisation event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            // Pre-initialise (unless processing navigation placeholders).
            if (!IsNavigationPlaceholderPage)
                PreInitialise();
        }

        /// <summary>
        /// Page initialisation event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // Security precaution in order to thwart view state replay attacks.
            if (Request.IsAuthenticated)
            {
                ViewStateUserKey = 
                    Context.User.Identity.Name + Settings.UserId.ToString();
            }
        }

        /// <summary>
        /// Page load event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);

            // First page load tasks.
            if (!Page.IsPostBack)
            {                
                WebGuiController.InstantiateCollaborators(this);
                WebGuiController.Reset(this);
                WebGuiController.Load(this);
            }

            // Reload page tasks.
            else
            {
                WebGuiController.InstantiateCollaborators(this);
                WebGuiController.ReLoad(this);
            }
        }

        /// <summary>
        /// Sets the System.Web.UI.Page.Culture and System.Web.UI.Page.UICulture for
        /// the current thread of the page. 
        /// </summary>
        protected override void InitializeCulture()
        {
            base.InitializeCulture();
            base.Culture = Settings.UserCulture;
            base.UICulture = Settings.UserUiCulture;
        }

        #endregion System.Web.UI.Page overrides

        #region Private methods

        /// <summary>
        /// Performs standard page pre-initialisation routines.
        /// </summary>
        private void PreInitialise()
        {
            // Assign the page theme.
            this.Theme = Settings.UserTheme;

            // Assign the page title.
            if (!string.IsNullOrEmpty(Settings.ClientName))
                Title = Settings.ClientName;
            if (SiteMap.CurrentNode != null)
            {
                if (!String.IsNullOrEmpty(Title))
                    Title += @" - ";
                Title += SiteMap.CurrentNode.Title.Trim();
            }
        }

        #endregion Private methods

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

        #region Static methods

        /// <summary>
        /// Derives the web page view base from an object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The page.</returns>
        internal static WebPageBase DerivePage(
            object source)
        {
            WebPageBase result = null;
            Control control = source as Control;
            if (control != null)
                result = control.Page as WebPageBase;
            if (result ==  null)
                throw new WebGuiContainerInheritanceException();
            return result;
        }

        /// <summary>
        /// Derives the web master page view base from an object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The page.</returns>
        internal static WebMasterPageBase DeriveMasterPage(
            object source)
        {
            WebPageBase page = DerivePage(source);
            return page.Master as WebMasterPageBase;
        }

        /// <summary>
        /// Derives the root page control from the passed control object.
        /// </summary>
        /// <param name="source">The source object.</param>
        /// <returns>The root page control.</returns>
        internal static Control DeriveControl(
            object source)
        {
            Control result = source as Control;
            if (result != null)
                result = result.Page as Control;
            return result;
        }

        #endregion Static methods
    }
}