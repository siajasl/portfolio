using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Web;
using System.Web.UI.WebControls;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;

namespace Keane.CH.Framework.Apps.UI.Web.Controls
{
    /// <summary>
    /// Base class for all user controls acting as entity detail controls.
    /// </summary>
    public abstract class EntityDetailUserControlBase : 
        WebUserControlBase, 
        IEntityView
    {
        #region Properties

        /// <summary>
        /// Gets or sets a flag indicating whether the entity will be automatically loaded when the page loads for the first time.
        /// </summary>
        public bool DisableAutoEntityLoad
        { get; set; }

        #region Redirects

        /// <summary>
        /// Gets the default url to redirect to when exiting the page.
        /// </summary>
        protected abstract string DefaultRedirectUrlOnExit
        { get; }

        /// <summary>
        /// Gets the default url to redirect to when an insert has occurred.
        /// </summary>
        protected abstract string DefaultRedirectUrlOnInsert
        { get; }

        /// <summary>
        /// Gets the url to redirect to when exiting the page.
        /// </summary>
        public string RedirectUrlOnExit
        { get; set; }

        /// <summary>
        /// Gets or sets the url to redirect to when an insert has occurred.
        /// </summary>
        public string RedirectUrlOnInsert
        { get; set; }

        #endregion Redirects

        #endregion Properties

        #region WebUserControlBase overrides

        /// <summary>
        /// Initial load event.
        /// </summary>
        /// <remarks>
        /// Occurs after state initialisation.
        /// </remarks>
        public override void OnGuiLoad()
        {
            // Load page data.
            if (!DisableAutoEntityLoad)
                ExecuteLoad();
        }

        /// <summary>
        /// Pre initial load event (fired the first time the gui is loaded).
        /// </summary>
        /// <remarks>
        /// Occurs the first time the view is rendered.
        /// </remarks>
        public override void OnGuiLoading()
        {
            // Derive entity id from url.
            if (Request.QueryString["Id"] != null)
            {
                int entityId = 0;
                Int32.TryParse(Request.QueryString["Id"], out entityId);
                EntityId = entityId;
            }
        }
        
        /// <summary>
        /// Pre gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        /// <remarks>
        /// This allows the event to be cancelled.
        /// </remarks>
        public override void OnGuiCommandInvoking(WebGuiCommand command)
        {
            switch (command.Id)
            {
                case (int)GuiCommandType.Save:
                    ExecuteValidate(command);
                    break;
                case (int)GuiCommandType.SaveProtected:
                    ExecuteValidate(command);
                    break;
                case (int)GuiCommandType.SaveClose:
                    ExecuteValidate(command);
                    break;
                case (int)GuiCommandType.SaveCloseProtected:
                    ExecuteValidate(command);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Event handler for gui related events.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        public override void OnGuiCommandInvoke(WebGuiCommand command)
        {
            switch (command.Id)
            {
                case (int)GuiCommandType.Cancel:
                    ExecuteCancel(command);
                    break;
                case (int)GuiCommandType.CancelImmediate:
                    ExecuteCancelImmediate(command);
                    break;
                case (int)GuiCommandType.Delete:
                    ExecuteDelete(command);
                    break;
                case (int)GuiCommandType.DeleteProtected:
                    ExecuteDeleteProtected(command);
                    break;
                case (int)GuiCommandType.Save:
                    ExecuteSave(command);
                    break;
                case (int)GuiCommandType.SaveClose:
                    ExecuteSave(command);
                    ExecuteClose(command);
                    break;
                case (int)GuiCommandType.SaveCloseProtected:
                    ExecuteSaveProtected(command);
                    ExecuteClose(command);
                    break;
                case (int)GuiCommandType.SaveProtected:
                    ExecuteSaveProtected(command);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Post gui command execution handler.
        /// </summary>
        /// <param name="command">The command being invoked.</param>
        public override void OnGuiCommandInvoked(WebGuiCommand command)
        {
            switch (command.Id)
            {
                case (int)GuiCommandType.Delete:
                    SetRedirectUrlOnExit(command);
                    break;
                case (int)GuiCommandType.DeleteProtected:
                    SetRedirectUrlOnExit(command);
                    break;
                case (int)GuiCommandType.Save:
                    if (EditMode == EditModeType.Insert)
                        SetRedirectUrlOnInsert(command);
                    break;
                case (int)GuiCommandType.SaveProtected:
                    if (EditMode == EditModeType.Insert)
                        SetRedirectUrlOnInsert(command);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// User control initialisation event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            // Inject hidden input controls in order to cache entity information.
            EntityVersionControl = new HtmlInputHidden();
            EntityIdControl = new HtmlInputHidden();            
        }

        #endregion WebUserControlBase overrides

        #region IEntityView Members

        /// <summary>
        /// Gets the entity Id.
        /// </summary>
        public int EntityId
        {
            get
            {
                int result = 0;
                if (EntityIdControl.Value != null)
                    Int32.TryParse(EntityIdControl.Value, out result);
                return result;
            }
            set
            {
                if (EntityIdControl.Value != null)
                    EntityIdControl.Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the entity version.
        /// </summary>
        public int EntityVersion
        {
            get
            {
                int result = 0;
                if (EntityVersionControl.Value != null)
                    Int32.TryParse(EntityVersionControl.Value, out result);
                return result;
            }
            set
            {
                if (EntityVersionControl.Value != null)
                    EntityVersionControl.Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the entity version control.
        /// </summary>
        private HtmlInputHidden EntityVersionControl
        {
            get
            {
                return PlaceHolderControl.FindControl(this.ID + "_entityVersionControl") as HtmlInputHidden;
            }
            set
            {
                value.ID = this.ID + "_entityVersionControl";                
                value.Value = "0";
                PlaceHolderControl.Controls.Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the entity id control.
        /// </summary>
        private HtmlInputHidden EntityIdControl
        {
            get
            {
                return PlaceHolderControl.FindControl(this.ID + "_entityIdControl") as HtmlInputHidden;
            }
            set
            {
                value.ID = this.ID + "_entityIdControl";
                value.Value = "0";
                PlaceHolderControl.Controls.Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the entity xml.
        /// </summary>
        public string EntityXml
        { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the save command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        private void ExecuteSave(WebGuiCommand command)
        {
            if (EditMode == EditModeType.Insert)
                ExecuteInsert(command);
            else
                ExecuteUpdate(command);
        }

        /// <summary>
        /// Executes the save command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        private void ExecuteSaveProtected(WebGuiCommand command)
        {
            if (EditMode == EditModeType.Insert)
                ExecuteInsert(command);
            else
                ExecuteUpdateProtected(command);
        }

        /// <summary>
        /// Executes the load command.
        /// </summary>
        public virtual void ExecuteLoad()
        { }

        /// <summary>
        /// Executes the load from xml command.
        /// </summary>
        public virtual void ExecuteLoadFromXml()
        { }

        /// <summary>
        /// Executes the update command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteUpdate(WebGuiCommand command) 
        { }

        /// <summary>
        /// Executes the update protected command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteUpdateProtected(WebGuiCommand command) 
        { }

        /// <summary>
        /// Executes the insert command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteInsert(WebGuiCommand command)
        { }
        
        /// <summary>
        /// Executes the close command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteClose(WebGuiCommand command)
        {
            SetRedirectUrlOnExit(command);
        }

        /// <summary>
        /// Executes the cancel command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteCancel(WebGuiCommand command)
        {
            SetRedirectUrlOnExit(command);
        }

        /// <summary>
        /// Executes the cancel command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteCancelImmediate(WebGuiCommand command)
        {
            SetRedirectUrlOnExit(command);
        }

        /// <summary>
        /// Executes the delete command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteDelete(WebGuiCommand command)
        { }

        /// <summary>
        /// Executes the delete protected command.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteDeleteProtected(WebGuiCommand command)
        { }

        /// <summary>
        /// Executes page validation prior to save operation.
        /// </summary>
        /// <param name="args">The associated event arguments.</param>
        public virtual void ExecuteValidate(WebGuiCommand command)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                command.AjaxResponseData.FormWasValidated = false;
                command.IsCancelled = true;            
            }
        }

        #region Private methods

        /// <summary>
        /// Sets the redirect url upon an exit.
        /// </summary>
        /// <param name="args">Underlying event arguments.</param>
        private void SetRedirectUrlOnExit(WebGuiCommand command)
        {
            // Use the assigned redirect url or default as appropriate.
            string redirectUrl = string.Empty;
            if (!string.IsNullOrEmpty(this.RedirectUrlOnExit))
                redirectUrl = this.RedirectUrlOnExit;
            else if (!string.IsNullOrEmpty(this.DefaultRedirectUrlOnExit))
                redirectUrl = this.DefaultRedirectUrlOnExit;

            // If a redirect url was specified then redirect.
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                // Resolve the redirect url & inject into response (suppress exception handling).
                try
                {
                    command.AjaxResponseData.RedirectUrl = ResolveUrl(redirectUrl);
                }
                catch { }
            }
        }

        /// <summary>
        /// Sets the redirect url upon an insert.
        /// </summary>
        /// <param name="args">Underlying event arguments.</param>
        private void SetRedirectUrlOnInsert(WebGuiCommand command)
        {
            // Use the assigned redirect url or default as appropriate.
            string redirectUrl = string.Empty;
            if (!string.IsNullOrEmpty(this.RedirectUrlOnInsert))
                redirectUrl = this.RedirectUrlOnInsert;
            else if (!string.IsNullOrEmpty(this.DefaultRedirectUrlOnInsert))
                redirectUrl = this.DefaultRedirectUrlOnInsert;

            // If a redirect url was specified then redirect.
            if (!string.IsNullOrEmpty(redirectUrl))
            {
                // Inject the new entity id into the url (suppress exception handling).
                try
                {
                    redirectUrl = string.Format(redirectUrl, this.EntityId);
                }
                catch { }

                // Resolve the redirect url & inject into response (suppress exception handling).
                try
                {
                    command.AjaxResponseData.RedirectUrl = base.ResolveUrl(redirectUrl);
                }
                catch { }
            }
        }

        #endregion Private methods

        #endregion Methods
    }
}