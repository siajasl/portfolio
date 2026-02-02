using Keane.CH.Framework.DataAccess.Entity;
using Keane.CH.Framework.Services.Security.Contracts.Message;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Services.Security.Implementation.DataAccess
{
    /// <summary>
    /// Encapsualtes all data access operations for a CodeItem domain object.
    /// </summary>
    public interface IUserDao :
        IEntityDao
    {
        /// <summary>
        /// Gets an instance by searching across the repository by username.
        /// </summary>
        /// <param name="userName">The system unique username.</param>
        /// <returns>A user object.</returns>
        User Get(string userName);

        /// <summary>
        /// Gets an instance by searching for the user that originated the protected entity modification.
        /// </summary>
        /// <param name="modificationId">The protected entity modification id.</param>
        /// <returns>A user object.</returns>
        User GetByModificationId(int modificationId);

        /// <summary>
        /// Gets a list of filtered by the passed role type.
        /// </summary>
        /// <param name="refIdUserRoleType">The type of role.</param>
        /// <returns>A list of users filtered by role type.</returns>
        EntityBaseCollection<User> GetByRoleType(int refIdUserRoleType);
    }
}