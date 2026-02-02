using System;
using Keane.CH.Framework.Core.Resources.Exceptions;

namespace Keane.CH.Framework.Apps.UI.WPF
{
    /// <summary>
    /// Exception thrown when a container does not inherit from one of the framework base classes.
    /// </summary>
    /// <remarks>
    /// This ensures that developers are hooking into the UI framework correctly.
    /// </remarks>
    public class WPFGuiContainerInheritanceException : Exception
    {
        public WPFGuiContainerInheritanceException()
            : base(String.Format(ExceptionMessages.WPFGuiContainerInheritanceExceptionMessage, 
                                 typeof(WPFWindowBase).FullName, 
                                 typeof(WPFUserControlBase).FullName))
        { }
    }
}
