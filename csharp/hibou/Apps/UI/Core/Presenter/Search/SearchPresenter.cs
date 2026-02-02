using System;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Services.Search.Contracts;
using Keane.CH.Framework.Services.Search.Contracts.Message;
using Keane.CH.Framework.Apps.UI.Core.View.Search;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Search
{
    /// <summary>
    /// A search dedicated presenter.
    /// </summary>
    /// <typeparam name="SC">The search criteria.</typeparam>
    /// <typeparam name="SCV">The search criteria view interface.</typeparam>
    /// <typeparam name="SI">The search result item.</typeparam>
    public class SearchPresenter<SC, SR, SCV> : 
        PresenterBase,
        ISearchPresenter<SCV>
        where SC : SearchCriteriaBase, new()
        where SR : new() 
    {
        #region Constructor

        /// <summary>
        /// Gets or sets the associated search mapper.
        /// </summary>
        public ISearchPresenterMapper<SC,  SR, SCV> Mapper
        {
            get
            {
                // Just in time instantiation (unless already injected).
                if (this.mapperField == null)
                    this.mapperField = new SearchPresenterMapper<SC, SR, SCV>();
                return this.mapperField;
            }
            set
            {
                this.mapperField = value;
            }
        }
        private ISearchPresenterMapper<SC, SR, SCV> mapperField;

        /// <summary>
        /// Gets or sets the associated search service.
        /// </summary>
        public ISearchService SearchService
        { get; set; }

        #endregion Constructor

        #region ISearchPresenter<SC> Members

        /// <summary>
        /// Searches for a list of entries.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        public void Search(
            ISearchView<SCV> 
            view, GuiContext viewContext)
        {
            // Defensive coding.
            base.AssertParameters(view, viewContext);

            // Derive model from view (i.e. search criteria).
            SC model = new SC();
            this.Mapper.DeserializeModel(model, view, viewContext);

            // Invoke service operation.
            SearchRequest request = new SearchRequest 
            {
                Context = base.GetRequestContext(viewContext),
                Criteria = model
            };
            SearchResponse response =
                this.SearchService.Search(request);

            // Bind view to model (i.e. search results).
            this.Mapper.DeserializeView(view, response.Result, viewContext);
        }

        #endregion      
    }
}