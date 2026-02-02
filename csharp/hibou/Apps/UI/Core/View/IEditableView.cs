using Keane.CH.Framework.Apps.UI.Core;

namespace Keane.CH.Framework.Apps.UI.Core.View
{
    /// <summary>
    /// Encapsulates core attributes of an editable views.
    /// </summary>
    public interface IEditableView
    {
        /// <summary>
        /// Gets the edit mode type.
        /// </summary>
        EditModeType EditMode { get; }
    }
}
