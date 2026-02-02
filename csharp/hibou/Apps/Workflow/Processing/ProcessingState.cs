using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// Enumeration over the array of possible processing states.
    /// </summary>
    [Serializable]
    public enum ProcessingState
    {
        Fault,
        Success,
        InProgress
    }
}