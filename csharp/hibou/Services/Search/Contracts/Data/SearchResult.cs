using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.Linq;

namespace Keane.CH.Framework.Services.Search.Contracts.Data
{
    /// <summary>
    /// Repersents the results of a search.
    /// </summary>
    /// <typeparam name="I">A search result item.</typeparam>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Search")]
    [Serializable]
    public class SearchResult : 
        IDeserializationCallback
    {
        #region Constructor

        public SearchResult()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Collaborator instantiation event.
        /// </summary>
        protected void InitialiseMembers()
        {
            this.DataList = new List<object>();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the privately managed list.
        /// </summary>
        private List<object> DataList
        { get; set; }

        /// <summary>
        /// Gets the returned search data.
        /// </summary>
        [DataMember()]
        public object[] Data
        {
            get
            {
                if (this.DataList != null)
                    return this.DataList.ToArray();
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.DataList = value.ToList();
            }
        }

        /// <summary>
        /// Returns the count of items in the search result.
        /// </summary>
        public int Count
        {
            get
            {
                if (this.DataList != null)
                    return this.DataList.Count;
                else
                    return default(int);
            }
        }

        /// <summary>
        /// Gets or sets the total in the underlying repository.
        /// </summary>
        /// <remarks>This support paging scenarios.</remarks>
        [DataMember()]
        public int Total
        { get; set; }

        /// <summary>
        /// Gets or sets the time in milliseconds that the serach took.
        /// </summary>
        [DataMember()]
        public int TimeInMs
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item">An item being added to the list.</param>
        public void Add(object item)
        {
            if (item !=  null)
                this.DataList.Add(item);
        }

        /// <summary>
        /// Adds a list of items to the list.
        /// </summary>
        /// <param name="items">The list of items being added to the list.</param>
        public void Add(object[] items)
        {
            if (items != null) 
            {
                foreach (object item in items)
                {
                    this.Add(item);
                }
            }
        }

        /// <summary>
        /// Sets the maximum size of the search result.
        /// </summary>
        /// <param name="maximumSize">The maximum size of the search result.</param>
        public void SetMaximumSize(int maximumSize)
        {
            if (maximumSize > 0 &&
                this.Count > maximumSize)
            {
                int index = maximumSize;
                int count = this.Count - maximumSize;
                this.DataList.RemoveRange(index, count);
            }
        }

        #endregion Methods

        #region IDeserializationCallback Members

        void IDeserializationCallback.OnDeserialization(object sender)
        {
            if (this.Data != null)
                this.DataList = this.Data.ToList();
            else
                this.DataList = new List<object>();
        }

        #endregion
    }
}