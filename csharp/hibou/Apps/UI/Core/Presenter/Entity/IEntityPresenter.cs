using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Entity
{
    /// <summary>
    /// Represents a simple entity presenter.
    /// </summary>
    /// <typeparam name="EV">The associated entity detail view.</typeparam>
    public interface IEntityPresenter<EV>
        where EV : class, IEntityView
    {
        /// <summary>
        /// Loads an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Load(
            EV view,
            GuiContext viewContext);

        /// <summary>
        /// Loads an entity directly from previously deserialized xml.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void LoadProtected(
            EV view,
            GuiContext viewContext);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Delete(
            EV view,
            GuiContext viewContext);

        /// <summary>
        /// Deletes a protected entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void DeleteProtected(
            EV view,
            GuiContext viewContext);

        /// <summary>
        /// Inserts an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Insert(
            EV view,
            GuiContext viewContext);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void Update(
            EV view,
            GuiContext viewContext);

        /// <summary>
        /// Updates a protected entity.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void UpdateProtected(
            EV view,
            GuiContext viewContext);
    }
}
