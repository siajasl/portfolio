using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View.Search;
using Keane.CH.Framework.Core.Utility.Reflection;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Search
{
    /// <summary>
    /// A search dedicated mapper.
    /// </summary>
    /// <remarks>
    /// This uses reflections to perform mapping.
    /// </remarks>
    /// <typeparam name="SC">A subclass of SearchCriteriaBase.</typeparam>
    /// <typeparam name="SCV">A search criteria view.</typeparam>
    /// <typeparam name="SR">The type of search result item returned by the search.</typeparam>
    public class SearchPresenterMapper<SC, SR, SCV> :
        ISearchPresenterMapper<SC, SR, SCV>
        where SC : SearchCriteriaBase, new()
        where SR : new()
    {
        #region ISearchPresenterMapper<SC, SI, SCV> Members

        /// <summary>
        /// Deserializes the view from the model.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="model">The model from which the view state is to be deserialzed.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        void ISearchPresenterMapper<SC, SR, SCV>.DeserializeView(
            ISearchView<SCV> view, 
            SearchResult model, 
            GuiContext viewContext)
        {
            view.Results.DataSource = model.Data;
            view.Results.DataBind();
        }

        /// <summary>
        /// Deserializes the model from the view.
        /// </summary>
        /// <param name="model">The model being processed.</param>
        /// <param name="view">The view from which the model state is to deserialzed.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        void ISearchPresenterMapper<SC, SR, SCV>.DeserializeModel(
            SC model, 
            ISearchView<SCV> view, 
            GuiContext viewContext)
        {
            // Perform reflection binding.
            ReflectionMappingUtility.Map(view.Criteria, typeof(SCV), model, true);

            // Apply default criteria from view context.
            model.CultureId = viewContext.CultureId;
        }

        #endregion
    }
}
