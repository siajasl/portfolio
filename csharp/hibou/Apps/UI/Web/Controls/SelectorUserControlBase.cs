using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Web;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core.Presenter;
using Keane.CH.Framework.Apps.UI.Core.View.Search;
using Keane.CH.Framework.Apps.UI.Core.Presenter.Search;

namespace Keane.CH.Framework.Apps.UI.Web.Controls
{
    /// <summary>
    /// Encapsulates common selector related functionality.
    /// </summary>
    public abstract class SelectorUserControlBase :
        WebUserControlBase, 
        ISelectorView
    {
        #region Properties

        /// <summary>
        /// Gets or sets associated presenter.
        /// </summary>
        public ISelectorPresenter SelectorPresenter
        { get; set; }

        /// <summary>
        /// Gets or sets the selector adaptor managing the collection of items.
        /// </summary>
        public IListView SelectorAdaptor
        { get; set; }

        /// <summary>
        /// Gets or sets the selector type.
        /// </summary>
        public string SelectorType
        { get; set; }

        /// <summary>
        /// Gets or sets the selector sub-type.
        /// </summary>
        public string SelectorSubType
        { get; set; }

        /// <summary>
        /// Gets or sets the selector display type.
        /// </summary>
        public SelectorDisplayType DisplayType
        { get; set; }

        /// <summary>
        /// Gets the selector default value.
        /// </summary>
        public string DefaultValue
        { get; set; }

        /// <summary>
        /// Gets the selector sort direction type.
        /// </summary>
        public SortDirectionType SortDirectionType
        { get; set; }

        /// <summary>
        /// Gets the selector sort control type.
        /// </summary>
        public SelectorSortSourceType SortSourceType
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the null value will be suppressed.
        /// </summary>
        public bool SuppressNullListItem
        { get; set; }

        /// <summary>
        /// Gets or sets the selected index.
        /// </summary>
        public int SelectedIndex
        {
            get { return SelectorAdaptor.SelectedIndex; }
            set { SelectorAdaptor.SelectedIndex = value; }
        }

        /// <summary>
        /// Gets or sets the event id to fire when a code selector 
        /// item has been selected and an autopostback is being handled.
        /// </summary>
        public int OnSelectedEventId
        {
            get
            {
                object o = ViewState["OnSelectedEventId"];
                return (o == null) ? 0 : (int)o;
            }
            set
            {
                ViewState["OnSelectedEventId"] = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Presenter creation function.
        /// </summary>
        /// <returns>A selector presenter.</returns>
        protected virtual ISelectorPresenter CreateSelectorPresenter()
        {
            return null;
        }

        /// <summary>
        /// Adaptor creation function.
        /// </summary>
        /// <returns>A selector adaptor.</returns>
        protected abstract IListView CreateSelectorAdaptor();

        #endregion Methods

        #region WebUserControlBase overrides

        /// <summary>
        /// Container reset event.
        /// </summary>
        public override void OnGuiReset()
        {
            base.OnGuiReset();
            SelectorAdaptor.SelectedIndex = -1;
        }

        /// <summary>
        /// Sets the container's user input state so as to prevent/allow user interaction.
        /// </summary>
        /// <param name="locked">Flag indicating whether the view will be locked for user input.</param>
        public override void OnGuiLock(bool locked)
        {
            base.OnGuiLock(locked);
            SelectorAdaptor.Visible = !locked;
        }

        /// <summary>
        /// Pre initial load event (fired the first time the gui is loaded).
        /// </summary>
        /// <remarks>
        /// Occurs the first time the view is rendered.
        /// </remarks>
        public override void OnGuiLoading()
        {
            SelectorAdaptor.Clear();            
            SelectorPresenter.InitialiseSelector(this, Settings.CreateContext());
        }

        /// <summary>
        /// Placeholder for the poitn at which collaborator injection takes place.
        /// </summary>
        public override void OnGuiInjectDependencies()
        {
            base.OnGuiInjectDependencies();
            ISelectorPresenter presenter = CreateSelectorPresenter();
            if (presenter != null)
                SelectorPresenter = presenter;
            SelectorAdaptor = CreateSelectorAdaptor();
        }
        #endregion WebUserControlBase overrides

        #region ISelectorView Members

        string ISelectorView.SelectorType
        {
            get { return this.SelectorType; }
        }

        string ISelectorView.SelectorSubType
        {
            get { return SelectorSubType; }
        }

        SelectorDisplayType ISelectorView.DisplayType
        {
            get { return this.DisplayType; }
        }

        SortDirectionType ISelectorView.SortDirection
        {
            get { return this.SortDirectionType; }
        }

        SelectorSortSourceType ISelectorView.SortSourceType
        {
            get { return this.SortSourceType; }
        }

        string ISelectorView.DefaultValue
        {
            get { return this.DefaultValue; }
        }

        bool ISelectorView.SuppressNullListItem
        {
            get { return this.SuppressNullListItem; }
        }

        IListView ISelectorView.List
        {
            get { return this.SelectorAdaptor; }
        }

        #endregion
    }
}
