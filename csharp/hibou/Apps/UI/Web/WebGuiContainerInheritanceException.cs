using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Core.Resources.Exceptions;

namespace Keane.CH.Framework.Apps.UI.Web
{
    /// <summary>
    /// Exception thrown when a container does not inherit from one of the framework base classes.
    /// </summary>
    /// <remarks>
    /// This ensures that developers are hooking into the UI framework correctly.
    /// </remarks>
    public class WebGuiContainerInheritanceException : Exception
    {
        public WebGuiContainerInheritanceException()
            : base(String.Format(ExceptionMessages.WebGuiContainerInheritanceExceptionMessage, 
                                 typeof(WebPageBase).FullName, 
                                 typeof(WebUserControlBase).FullName, 
                                 typeof(WebMasterPageBase).FullName))
        { }
    }
}