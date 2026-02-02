using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Services.Search.Contracts.Data;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Entity
{
    /// <summary>
    /// Interface implementted by those classes acting as mapper interfaces.
    /// </summary>
    /// <typeparam name="E">A domain entity implemented as a sub-class of EntityBase.</typeparam>
    /// <typeparam name="EV">An entity view being processed.</typeparam>
    public interface IEntityPresenterMapper<E, EV>
        where E : EntityBase, new()
        where EV : class, IEntityView
    {
        /// <summary>
        /// Deserializes the view from the model.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="model">The model from which the view state is to be deserialzed.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        void DeserializeView(
            EV view,
            E model,
            GuiContext viewContext);

        /// <summary>
        /// Deserializes the model from the view.
        /// </summary>
        /// <param name="model">The model being processed.</param>
        /// <param name="view">The view from which the model state is to deserialzed.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        void DeserializeModel(
            E model,
            EV view,
            GuiContext viewContext);
    }
}