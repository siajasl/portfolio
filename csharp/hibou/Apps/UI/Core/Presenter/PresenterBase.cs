using System;
using System.Diagnostics;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Core;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter
{
    /// <summary>
    /// Base class inherited by all presenters.
    /// </summary>
    public abstract class PresenterBase : 
        ServiceProxyBase
    {
        /// <summary>
        /// Performs a null test against the passed view/view context parameters.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context information.</param>
        protected void AssertParameters(
            object view, 
            object viewContext)
        {
            Debug.Assert(view != null, "view is null");
            Debug.Assert(view != null, "viewContext is null");
        }

        /// <summary>
        /// Derives an operation request context from the passed view context.
        /// </summary>
        /// <param name="viewContext">The view context.</param>
        /// <returns>An operation request context.</returns>
        protected OperationRequestContext GetRequestContext(
            GuiContext viewContext)
        {
            OperationRequestContext result = new OperationRequestContext();
            result.UserName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            result.CultureId = viewContext.CultureId;
            result.UserId = viewContext.UserId;
            return result;
        }
    }
}