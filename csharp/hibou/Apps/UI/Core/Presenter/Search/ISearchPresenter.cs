using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View.Search;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Search
{
    /// <summary>
    /// Encpasulates a presenter dedicated to search.
    /// </summary>
    /// <typeparam name="CV">The associated search criteria view.</typeparam>
    public interface ISearchPresenter<CV>
    {
        /// <summary>
        /// Searches for a list of entries.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Search(ISearchView<CV> view, GuiContext viewContext);
    }
}