using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Web.AjaxResponse;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Represents a command that is to be executed across a web UI.
    /// </summary>
    /// <remarks>
    /// These allow web UI containers to process commands.
    /// </remarks>
    public class WebGuiCommand : GuiCommand
    {
        #region Constructors

        public WebGuiCommand()
            : base()
        { }

        protected override void InitialiseMembers()
        {
            base.InitialiseMembers();
            AjaxResponseData = new AjaxResponseData();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the ajax response data to be sent back to the client.
        /// </summary>
        public AjaxResponseData AjaxResponseData
        { get; set; }

        #endregion Properties
    }
}
