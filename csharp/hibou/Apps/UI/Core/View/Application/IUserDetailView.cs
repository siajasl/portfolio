using Keane.CH.Framework.Apps.UI.Core.View;
using Keane.CH.Framework.Apps.UI.Core.View.Entity;

namespace Keane.CH.Framework.Apps.UI.Core.View.Application
{
    /// <summary>
    /// Represents a view over the details of an entity.
    /// </summary>
    public interface IUserDetailView :
        IEntityView
    {
        /// <summary>
        /// Gets or sets the user's application name.
        /// </summary>        
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user's first name.
        /// </summary>        
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's surname.
        /// </summary>        
        string Surname
        { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>        
        string EmailAddress
        { get; set; }

        /// <summary>
        /// Gets or sets the user's canton.
        /// </summary>        
        int RefIdCanton
        { get; set; }

        /// <summary>
        /// Gets or sets the user's culture.
        /// </summary>        
        int RefIdSupportedCulture
        { get; set; }

        /// <summary>
        /// Gets or sets the user role id.
        /// </summary>        
        int RefIdUserRoleType
        { get; set; }

        /// <summary>
        /// Gets or sets the user's password question.
        /// </summary>        
        string PasswordQuestion
        { get; set; }

        /// <summary>
        /// Gets or sets the user's password question answer.
        /// </summary>        
        string PasswordQuestionAnswer
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether the user is deletable or not.
        /// </summary>        
        bool IsDeletable { get; set; }

        /// <summary>
        /// Gets a flag indicating whether the user is new to the system.
        /// </summary>        
        bool IsNew { get; }
    }
}