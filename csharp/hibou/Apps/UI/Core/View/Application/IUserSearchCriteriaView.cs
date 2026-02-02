namespace Keane.CH.Framework.Apps.UI.Core.View.Application
{
    /// <summary>
    /// A view over the criteria used during a search.
    /// </summary>
    public interface IUserSearchCriteriaView
    {
        /// <summary>
        /// Gets the free text search.
        /// </summary>
        string FreeText { get; }

        /// <summary>
        /// Gets the user role type id.
        /// </summary>
        int UserRoleTypeId { get; }

        /// <summary>
        /// Gets the user role canton id.
        /// </summary>
        int CantonId { get; }
    }
}