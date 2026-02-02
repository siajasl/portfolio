using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Apps.UI.Web.AjaxResponse
{
    /// <summary>
    /// Enumeration over the type of dom element to be updated.
    /// </summary>
    public enum DomUpdateTargetType
    {
        Unspecified,
        AspNetTextBox,
        AspNetLabel,
        HtmlInput,
    }
}
