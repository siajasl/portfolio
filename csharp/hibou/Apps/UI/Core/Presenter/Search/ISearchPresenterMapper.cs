using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Apps.UI.Core.View.Search;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Search
{
    /// <summary>
    /// Search criteria mapper interface.
    /// </summary>
    /// <typeparam name="SC">The search criteria.</typeparam>
    /// <typeparam name="SCV">The search criteria view interface.</typeparam>
    /// <typeparam name="SR">The search result item.</typeparam>
    public interface ISearchPresenterMapper<SC, SR, SCV>
        where SC : new()
        where SR : new()
    {
        /// <summary>
        /// Binds the view to the model.
        /// </summary>
        /// <param name="view">A search view being processed.</param>
        /// <param name="model">A list of search results to be bound to the view.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        void DeserializeView(
            ISearchView<SCV> view,
            SearchResult model,
            GuiContext viewContext);

        /// <summary>
        /// Binds the model to the view.
        /// </summary>
        /// <param name="model">A search criteria entered by the user.</param>
        /// <param name="view">A search view being processed.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        void DeserializeModel(
            SC model,
            ISearchView<SCV> view,
            GuiContext viewContext);
    }
}
