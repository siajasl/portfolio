using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// Enumeration over the array of processing error severity types.
    /// </summary>
    [Serializable]
    public enum ErrorSeverityType
    {
        Low = 1,
        Medium = 10,
        High = 100,
        Critical = 1000,
    }
}
