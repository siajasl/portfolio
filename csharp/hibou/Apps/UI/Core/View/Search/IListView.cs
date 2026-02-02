using System;
using System.Collections.Generic;
using System.Linq;
using Keane.CH.Framework.Core.ExtensionMethods;

namespace Keane.CH.Framework.Apps.UI.Core.View.Search
{
    /// <summary>
    /// Represents a technology neutral abstraction of a list view.
    /// </summary>
    public interface IListView
    {
        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="itemData">The item data.</param>
        void Add(ListItemData itemData);

        /// <summary>
        /// Adds a collection of items to the list.
        /// </summary>
        /// <param name="itemCollection">The collection of items being added to the list view.</param>
        void Add(ListItemDataCollection itemCollection);

        /// <summary>
        /// Adds a collection of items to the list.
        /// </summary>
        /// <param name="itemCollection">The collection of items being added to the list view.</param>
        /// <param name="sortDirection">The sort direction.</param>
        void Add(ListItemDataCollection itemCollection, SortDirectionType sortDirection);

        /// <summary>
        /// Clears all items from the list.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets or sets the selected index.
        /// </summary>
        int SelectedIndex {get; set;}

        /// <summary>
        /// Gets or sets the visibility flag.
        /// </summary>
        bool Visible { get; set; }
    }

    /// <summary>
    /// Represents a technology neutral abstraction of a item data associated with ListBoxes & the such like.
    /// </summary>
    public class ListItemData
    {
        #region Properties

        /// <summary>
        /// Gets or sets the list item value.
        /// </summary>
        public string Value
        { get; set; }

        /// <summary>
        /// Gets or sets the list item text.
        /// </summary>
        public string Text
        { get; set; }

        /// <summary>
        /// Gets or sets the list item other value.
        /// </summary>
        /// <remarks>This other value can be used as a sort control.</remarks>
        public object OtherValue
        { get; set; }

        /// <summary>
        /// Gets or sets the list item data used for sorting.
        /// </summary>
        public object SortData
        {
            get
            {
                object result = default(object);
                switch (SortSource)
                {
                    case SelectorSortSourceType.Text:
                        result = Text;
                        break;
                    case SelectorSortSourceType.Value:
                        result = Value;
                        break;
                    case SelectorSortSourceType.OtherValue:
                        result = OtherValue;
                        break;
                    default:
                        result = Text;
                        break;
                }
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the sort control.
        /// </summary>
        public SelectorSortSourceType SortSource
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the item is selected.
        /// </summary>
        public bool IsSelected
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the other value to a numeric if possible.
        /// </summary>
        /// <param name="otherValue">The other value to be assigned.</param>
        public void SetOtherValue(string otherValue)
        {
            int otherValueAsInt = default(int);
            if (int.TryParse(otherValue, out otherValueAsInt))
                OtherValue = otherValueAsInt;
            else
                OtherValue = otherValue;
        }

        #endregion Methods
    }

    /// <summary>
    /// Manages a collection of list item data objects.
    /// </summary>
    public class ListItemDataCollection
    {
        #region Constructor

        public ListItemDataCollection()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        private void InitialiseMembers()
        {
            Items = new List<ListItemData>();
            EnforceUniqueItemValueConstraint = true;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the managed collection of list data items.
        /// </summary>
        private List<ListItemData> Items
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the unique item value constrint is to be enfoced or not.
        /// </summary>
        public bool EnforceUniqueItemValueConstraint
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Addsa n item to the managed collection.
        /// </summary>
        /// <param name="item">The item being added to the collection.</param>
        public void Add(ListItemData item)
        {
            // Defensive coding.
            if (item == null)
                throw new ArgumentNullException("item");

            // Enforce unique value constraint is instructed.
            if (EnforceUniqueItemValueConstraint)
            {
                // TODO
            }

            // Add to the collection.
            Items.Add(item);
        }

        /// <summary>
        /// Addsa n item to the managed collection.
        /// </summary>
        /// <param name="item">The item being added to the collection.</param>
        /// <param name="sortSource">The sort control to use.</param>
        public void Add(ListItemData item, SelectorSortSourceType sortSource)
        {
            // Defensive coding.
            if (item == null)
                throw new ArgumentNullException("item");
            item.SortSource = sortSource;
            Add(item);
        }

        /// <summary>
        /// Returns the collection sorted in ascending order.
        /// </summary>
        /// <returns>A sorted collection.</returns>
        public IEnumerable<ListItemData> GetSorted(SortDirectionType sortDirection)
        {
            IEnumerable<ListItemData> sorted;
            if (sortDirection == SortDirectionType.Ascending)
            {
                sorted =
                    from s in Items
                    orderby s.SortData ascending
                    select s;
            }
            else
            {
                sorted =
                    from s in Items
                    orderby s.SortData descending
                    select s;
            }
            return sorted;
        }

        /// <summary>
        /// Returns the collection sorted in ascending order.
        /// </summary>
        /// <returns>A sorted collection.</returns>
        public List<ListItemData> GetSortedList(SortDirectionType sortDirection)
        {
            IEnumerable<ListItemData> sorted = GetSorted(sortDirection);
            return sorted.AsCustomCollection<List<ListItemData>, ListItemData>();
        } 

        #endregion Methods
    }
}