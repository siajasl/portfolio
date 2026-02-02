using Keane.CH.Framework.Apps.UI.Core;
using Keane.CH.Framework.Apps.UI.Core.Presenter;
using Keane.CH.Framework.Apps.UI.Core.Presenter.Entity;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;
using Keane.CH.Framework.Services.Entity.Contracts;
using Keane.CH.Framework.Services.Entity.Contracts.Data;
using Keane.CH.Framework.Services.Entity.Contracts.Message;

namespace Keane.CH.Framework.Apps.UI.Core.Presenter.Entity
{
    /// <summary>
    /// UI presenter encapsulating modification ajudication operations.
    /// </summary>
    public class AdjudicationPresenter :
        PresenterBase, 
        IAdjudicationPresenter
    {
        #region Collaborators

        /// <summary>
        /// Gets or sets an associated service.
        /// </summary>
        public IEntityService EntityModificationService
        { get; set; }

        #endregion Collaborators

        #region IModificationAdjudicationPresenter Members

        /// <summary>
        /// Loads a delete modification for adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAdjudicationPresenter.Load(
            IAdjudicateDeleteView view, GuiContext viewContext)
        {
            // Retrieve modification instance.
            RetrieveRequest request = new RetrieveRequest() 
            {
                Context = base.GetRequestContext(viewContext),
                EntityId = view.ModificationId,
                EntityType = typeof(ProtectedEntityModification)
            };
            RetrieveResponse response = this.EntityModificationService.Retrieve(request);

            // Render the display according to the enttiy type.
            ProtectedEntityModification instance = response.Entity as ProtectedEntityModification;
            view.RenderForTargetEntity(
                instance.TargetEntityTypeId, 
                instance.TargetEntityId, 
                instance.RequiresDecision);
        }

        /// <summary>
        /// Accepts a delete adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAdjudicationPresenter.Accept(
            IAdjudicateDeleteView view, GuiContext viewContext)
        {
            // Send the decision to the service layer for processing.
            SendDecision(view.ModificationId, AjudicationDecisionType.Accept, viewContext);
        }

        /// <summary>
        /// Rejects a delete adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAdjudicationPresenter.Reject(
            IAdjudicateDeleteView view, GuiContext viewContext)
        {
            // Send the decision to the service layer for processing.
            SendDecision(view.ModificationId, AjudicationDecisionType.Reject, viewContext);
        }

        /// <summary>
        /// Loads an update modification for adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAdjudicationPresenter.Load(IAdjudicateUpdateView view, GuiContext viewContext)
        {
            // Retrieve modification instance.
            RetrieveRequest request = new RetrieveRequest() 
            {
                Context = base.GetRequestContext(viewContext),
                EntityId = view.ModificationId,
                EntityType = typeof(ProtectedEntityModification)
            };
            RetrieveResponse response = this.EntityModificationService.Retrieve(request);

            // Render the display according to the entity type.
            ProtectedEntityModification instance = (ProtectedEntityModification)response.Entity;
            view.RenderForTargetEntity(
                instance.TargetEntityTypeId, 
                instance.TargetEntityId,
                instance.TargetEntitySerialized,
                instance.RequiresDecision);
        }

        /// <summary>
        /// Accepts a update adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAdjudicationPresenter.Accept(IAdjudicateUpdateView view, GuiContext viewContext)
        {
            // Send the decision to the service layer for processing.
            SendDecision(view.ModificationId, AjudicationDecisionType.Accept, viewContext);
        }

        /// <summary>
        /// Rejects a update adjudication.
        /// </summary>
        /// <param name="view">The view being processed.</param>
        /// <param name="viewContext">The view context.</param>
        void IAdjudicationPresenter.Reject(IAdjudicateUpdateView view, GuiContext viewContext)
        {
            // Send the decision to the service layer for processing.
            SendDecision(view.ModificationId, AjudicationDecisionType.Reject, viewContext);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sends the details of a decision to the service layer.
        /// </summary>
        /// <param name="modificationId">The id of the modification being processed.</param>
        /// <param name="decisionType">The decision type.</param>
        /// <param name="viewContext">The view context.</param>
        private void SendDecision(
            int modificationId, 
            AjudicationDecisionType decisionType, 
            GuiContext viewContext)
        {
            // Invoke service operation.
            AdjudicateRequest request = new AdjudicateRequest() 
            {
                Context = GetRequestContext(viewContext),
                DecisionType = decisionType,
                ModificationId = modificationId
            };
            this.EntityModificationService.Adjudicate(request);
        }

        #endregion Private methods
    }
}
