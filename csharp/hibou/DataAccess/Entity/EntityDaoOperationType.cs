
namespace Keane.CH.Framework.DataAccess.Entity
{
    /// <summary>
    /// Enumeration over the standard entity persistence operations.
    /// </summary>
    /// <remarks>
    /// The numbering of the enumeration elements reflects the evolution of this aspect of the framework..
    /// </remarks>
    public enum EntityDaoOperationType
    {
        Delete = 0,
        Get = 1,
        GetAll = 2,
        GetCount = 3,
        GetVersion = 7,
        GetState = 8,
        Insert = 4,
        Update = 5,
        UpdateState = 6
    }
}