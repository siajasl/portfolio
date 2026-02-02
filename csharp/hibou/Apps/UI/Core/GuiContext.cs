
namespace Keane.CH.Framework.Apps.UI.Core
{
    /// <summary>
    /// Represents contextual information associated with a gui.
    /// </summary>
    public class GuiContext
    {
        #region Ctor

        /// <summary>
        /// Constructor.
        /// </summary>
        public GuiContext()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation method.
        /// </summary>
        protected void InitialiseMembers()
        {

        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the culture id.
        /// </summary>
        public int CultureId 
        { get; set; }

        /// <summary>
        /// Gets or sets the current user id.
        /// </summary>
        public int UserId 
        { get; set; }

        /// <summary>
        /// Gets or sets the current user role type id list.
        /// </summary>
        public int[] UserRoleTypeIds 
        { get; set; }

        #endregion Properties
    }
}