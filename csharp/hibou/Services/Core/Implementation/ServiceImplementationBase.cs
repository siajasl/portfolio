using System;
using Keane.CH.Framework.Services.Core.Operation;
using Keane.CH.Framework.Services.Core;

namespace Keane.CH.Framework.Services.Core
{
    /// <summary>
    /// Base class inherited by all service implementation classes.
    /// </summary>
    public abstract class ServiceImplementationBase : 
        ServiceProxyBase
    {
        /// <summary>
        /// Standard service exception handler.
        /// </summary>
        /// <param name="ex">The exception that has been thrown within the service.</param>
        protected virtual OperationResponse HandleServiceException(Exception ex)
        {
            // Process the service exception.
            ProcessException(ex);

            // Return response to be sent back to client.
            return OperationResponse.GetException(ex);
        }

        /// <summary>
        /// Standard service excpetion handler.
        /// </summary>
        /// <param name="ex">The exception that has been thrown within the service.</param>
        protected virtual OperationResponse<T> HandleServiceException<T>(Exception ex)
        {
            // Process the service exception.
            ProcessException(ex);

            // Return response to be sent back to client.
            return OperationResponse.GetException<T>(ex);
        }

        /// <summary>
        /// Handles a service exception.
        /// </summary>
        /// <param name="ex">The exception that has been thrown within the service.</param>
        private void ProcessException(Exception ex)
        { 
            // TODO log ...etc.
        }
    }
}
