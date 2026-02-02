using System.Web.UI.WebControls;
using Keane.CH.Framework.Apps.UI.Core.View;
using System;
using Keane.CH.Framework.Apps.UI.Core.View.Search;

namespace Keane.CH.Framework.Apps.UI.Web.Adaptors
{
    /// <summary>
    /// ListControl adaptor.
    /// </summary>
    public class ListControlAdaptor : 
        IListView
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="control">The control being adapted for a tachnology neutral MVP framework.</param>
        public ListControlAdaptor(ListControl control)
        {
            if (control == null)
                throw new ArgumentNullException("control");
            Control = control;
        }

        /// <summary>
        /// Gets or sets the view that is being adapted.
        /// </summary>
        private ListControl Control
        { get; set; }

        #region IListBoxView Members

        /// <summary>
        /// Gets or sets the selected index.
        /// </summary>
        public int SelectedIndex 
        {
            get 
            {
                return Control.SelectedIndex;
            }
            set 
            {
                Control.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the visibility flag.
        /// </summary>
        public bool Visible
        {
            get
            {
                return Control.Visible;
            }
            set
            {
                Control.Visible = value;
            }
        }

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        public void Add(ListItemData itemData)
        {
            if (itemData == null)
                throw new System.ArgumentNullException("itemData");
            ListItem item = new ListItem();
            item.Text = itemData.Text;
            item.Value = itemData.Value;
            item.Selected = itemData.IsSelected;
            Control.Items.Add(item);
        }

        /// <summary>
        /// Adds a collection of items to the list.
        /// </summary>
        /// <param name="itemCollection">The collection of items being added to the list view.</param>
        /// <param name="sortDirection">The sort direction.</param>
        public void Add(ListItemDataCollection itemCollection, SortDirectionType sortDirection)
        {
            if (itemCollection == null)
                throw new System.ArgumentNullException("itemCollection");
            itemCollection.GetSortedList(sortDirection).ForEach(i => Add(i));
        }

        /// <summary>
        /// Adds a collection of items to the list.
        /// </summary>
        /// <param name="itemCollection">The collection of items being added to the list view.</param>
        public void Add(ListItemDataCollection itemCollection)
        {
            Add(itemCollection, SortDirectionType.Ascending);
        }

        /// <summary>
        /// Clears an item from the list.
        /// </summary>
        public void Clear()
        {
            Control.Items.Clear();
        }

        #endregion
    }
}