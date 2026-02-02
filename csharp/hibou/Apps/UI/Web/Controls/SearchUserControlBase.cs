using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Web.Controls
{
    /// <summary>
    /// Base class for all user controls acting as search controls.
    /// </summary>
    public class SearchUserControlBase : 
        WebUserControlBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a flag indicating whether a search has already been executed or not.
        /// </summary>
        public bool SearchExecuted
        { get; set; }

        #endregion Properties
    }
}
