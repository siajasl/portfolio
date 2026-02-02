using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.Services.Search.Contracts.Data
{
    /// <summary>
    /// Abstract class inherited by search criteria classes.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Search")]
    [Serializable]
    public abstract class SearchCriteriaBase
    {
        #region Constants

        private const int MAXIMUM_RESULTS = 10000;

        #endregion Constants

        #region Constructor

        public SearchCriteriaBase()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            MaximumResults = MAXIMUM_RESULTS;
            CultureId = 1;
            FreeText = string.Empty;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the maximum number of results.
        /// </summary>
        [DataMember()]
        public int MaximumResults
        { get; set; }

        /// <summary>
        /// Gets or sets the id of the culture that the user is running under.
        /// </summary>
        [DataMember()]
        public int CultureId
        { get; set; }

        /// <summary>
        /// Gets the free text search.
        /// </summary>
        [DataMember()]
        public string FreeText { get; set; }

        /// <summary>
        /// Gets the free text search start point type.
        /// </summary>
        [DataMember()]
        public TextSearchStartPointType FreeTextStartPoint { get; set; }

        #endregion Properties

        #region Virtual methods

        /// <summary>
        /// Returns whether the search criteria may return a result.
        /// </summary>
        public virtual bool CanReturnResult()
        {
            return true;
        }

        /// <summary>
        /// Returns whether the search criteria are valid.
        /// </summary>
        public virtual bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Resets the search criteria.
        /// </summary>
        public virtual void Reset()
        {
            InitialiseMembers();
        }

        #endregion Virtual methods
    }
}