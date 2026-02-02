
namespace Keane.CH.Framework.Apps.UI.Core.View
{
    /// <summary>
    /// Enumeration over supported edit modes.
    /// </summary>
    /// <remarks>
    /// These are mutually exclusive.
    /// </remarks>
    public enum EditModeType
    {
        /// <summary>
        /// Unspecified (the default).
        /// </summary>
        Unspecified,

        /// <summary>
        /// The user is viewing data.
        /// </summary>
        View,

        /// <summary>
        /// The user is inserting data.
        /// </summary>
        Insert,

        /// <summary>
        /// The user is updating data.
        /// </summary>
        Update
    }
}
