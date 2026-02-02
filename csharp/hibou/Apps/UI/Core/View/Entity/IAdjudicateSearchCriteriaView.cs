using System;

namespace Keane.CH.Framework.Apps.UI.Core.View.Entity
{
    /// <summary>
    /// A view over the criteria used during a search for entity adjudications.
    /// </summary>
    public interface IAdjudicateSearchCriteriaView
    {
        /// <summary>
        /// Gets the id of the modification type being requested.
        /// </summary>
        int RequestTypeId { get; }

        /// <summary>
        /// Gets the id of the decision being modified.
        /// </summary>
        int DecisionTypeId { get; }

        /// <summary>
        /// Gets the id of the entity being modified.
        /// </summary>
        int EntityTypeId { get; }
    }
}