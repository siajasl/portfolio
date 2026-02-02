using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.DataAccess.ORM
{
    /// <summary>
    /// Enumeration over supported db parameter format types.
    /// </summary>
    /// <remarks>
    /// This is used when automatically generating db paramter names.
    /// </remarks>
    public enum ORMappingAttributeFormatType
    {
        UpperCaseUnderscore,
        UpperCase,
        None,
    }
}