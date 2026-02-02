using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.DataAccess.Core.Configuration
{
    /// <summary>
    /// Enumeration over the types of supported database.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public enum DaoDbType
    {
        [EnumMember()]
        Undefined = 0,

        [EnumMember()]
        SqlServer = 1,

        [EnumMember()]
        Oracle = 2,

        [EnumMember()]
        MySql = 3
    }
}