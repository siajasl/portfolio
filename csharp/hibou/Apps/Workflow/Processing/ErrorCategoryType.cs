using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// Enumeration over the array of processing error category types.
    /// </summary>
    [Serializable]
    public enum ErrorCategoryType
    {
        Validation = 1,
        Processing,
        Workflow,
    }
}