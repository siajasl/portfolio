using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Entity
{
    /// <summary>
    /// Presenter encapsulating ajudication operations.
    /// </summary>
    public interface IAdjudicationPresenter
    {
        /// <summary>
        /// Loads a delete modification for adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Load(
            IAdjudicateDeleteView view,
            GuiContext viewContext);

        /// <summary>
        /// Accepts a delete adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Accept(
            IAdjudicateDeleteView view,
            GuiContext viewContext);

        /// <summary>
        /// Rejects a delete adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Reject(
            IAdjudicateDeleteView view,
            GuiContext viewContext);

        /// <summary>
        /// Loads an update modification for adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Load(
            IAdjudicateUpdateView view,
            GuiContext viewContext);

        /// <summary>
        /// Accepts a update adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Accept(
            IAdjudicateUpdateView view,
            GuiContext viewContext);

        /// <summary>
        /// Rejects a update adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Reject(
            IAdjudicateUpdateView view,
            GuiContext viewContext);
    }
}
