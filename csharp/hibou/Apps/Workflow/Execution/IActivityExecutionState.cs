using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Workflow.Execution
{
    /// <summary>
    /// Implemented by workflows and activities in order to determine executability status.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public interface IActivityExecutionState
    {
        /// <summary>
        /// Is the workflow/activity executable or not?
        /// </summary>
        /// <returns>True if executable, false otherwise.</returns>
        bool IsExecutable();
    }
}
