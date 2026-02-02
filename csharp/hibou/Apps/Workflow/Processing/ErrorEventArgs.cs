using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// Processing error event arguments.
    /// </summary>
    [Serializable]
    public class ErrorEventArgs : EventArgs
    {
        #region Constructors

        /// <summary>
        /// Ctor.
        /// </summary>
        public ErrorEventArgs(Error error)
        {
            ProcessingError = error;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the processing error.
        /// </summary>
        public Error ProcessingError
        { get; private set; }

        #endregion Properties
    }
}
