using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View.Search;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Search
{
    /// <summary>
    /// UI presenter encapsulating selectors.
    /// </summary>
    public interface ISelectorPresenter
    {
        /// <summary>
        /// Initialises the selector.
        /// </summary>
        /// <param name="view">The view to be initialised.</param>
        /// <param name="viewContext">The view context.</param>
        void InitialiseSelector(
            ISelectorView view, 
            GuiContext viewContext);
    }
}
