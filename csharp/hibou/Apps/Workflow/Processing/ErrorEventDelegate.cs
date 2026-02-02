using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// A function pointer to a processing error event handler.
    /// </summary>
    /// <param name="sender">The event control.</param>
    /// <param name="args">The event arguments.</param>
    public delegate void ErrorEventDelegate(object sender, ErrorEventArgs args);
}