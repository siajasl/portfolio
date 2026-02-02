using Keane.CH.Framework.Apps.UI.Core;

namespace Keane.CH.Framework.Apps.UI.Core.View.Entity
{
    /// <summary>
    /// Represents the core details of a view over the details of an entity.
    /// </summary>
    public interface IEntityView : 
        IEditableView
    {
        /// <summary>
        /// Gets the entity id.
        /// </summary>
        int EntityId { get; set; }

        /// <summary>
        /// Gets or sets the entity version.
        /// </summary>
        int EntityVersion { get; set; }

        /// <summary>
        /// Gets or sets the entity xml.
        /// </summary>
        string EntityXml
        { get; set; }
    }
}