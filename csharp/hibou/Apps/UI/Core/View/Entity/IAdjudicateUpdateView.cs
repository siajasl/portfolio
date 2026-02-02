using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Apps.UI.Core.View.Entity
{
    /// <summary>
    /// Encapsulates the data required for a modicifation update adjudication.
    /// </summary>
    public interface IAdjudicateUpdateView
    {
        /// <summary>
        /// Gets or the id of the modification being adjudicated.
        /// </summary>
        int ModificationId { get; }

        /// <summary>
        /// Instructs the view to render as per the passed entity type.
        /// </summary>
        /// <param name="entityTypeId">The id of the type of entity being adjudicated.</param>
        /// <param name="entityId">The id of the entity being adjudicated.</param>
        /// <param name="entityXml">The modified xml.</param>
        /// <param name="requiresDecision">Indicates whether a decision is required or not.</param>
        void RenderForTargetEntity(
            int entityTypeId, int entityId, string entityXml, bool requiresDecision);
    }
}