using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;
using Keane.CH.Framework.Services.Entity.Contracts.Data;
using Keane.CH.Framework.Core.Utility.Reflection;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Entity
{
    /// <summary>
    /// An entity dedicated mapper.
    /// </summary>
    /// <remarks>
    /// This uses reflections to perform mapping.
    /// </remarks>
    /// <typeparam name="E">A domain entity implemented as a sub-class of EntityBase.</typeparam>
    /// <typeparam name="EV">An entity view being processed.</typeparam>
    public class EntityPresenterMapper<E, EV> :
        IEntityPresenterMapper<E, EV>
        where E : EntityBase, new()
        where EV : class, IEntityView
    {
        #region IEntityPresenterMapper<E,EV> Members

        /// <summary>
        /// Deserializes the view from the model.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="model">The model from which the view state is to be deserialzed.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        public virtual void DeserializeView(EV view, E model, GuiContext viewContext)
        {
            // Entity binding.
            view.EntityId = model.EntityInfo.EntityId;
            view.EntityVersion = model.EntityInfo.EntityVersion;

            // Reflection binding.
            ReflectionMappingUtility.Map(model, view, typeof(EV), true);
        }

        /// <summary>
        /// Deserializes the model from the view.
        /// </summary>
        /// <param name="model">The model being processed.</param>
        /// <param name="view">The view from which the model state is to deserialzed.</param>
        /// <param name="viewContext">The view context passed from the application.</param>
        public virtual void DeserializeModel(E model, EV view, GuiContext viewContext)
        {
            // Reflection binding.
            ReflectionMappingUtility.Map(view, typeof(EV), model, true);
        }

        #endregion
    }
}
