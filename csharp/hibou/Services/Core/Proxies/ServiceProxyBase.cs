using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Keane.CH.Framework.Services.Core.Operation;

namespace Keane.CH.Framework.Services.Core
{
    /// <summary>
    /// Base class inherited by all classes that act as clients to services.
    /// </summary>
    public abstract class ServiceProxyBase
    {
        #region Standard service response handler

        /// <summary>
        /// Processes a service response.
        /// </summary>
        /// <param name="response">A service reponse to be processed.</param>
        private void ProcessServiceOperationResponse(OperationResponse response)
        {
            // Defensive coding.
            if (response == null)
                throw new ArgumentNullException();

            // Process failure scenario.            
            if (response.Faulted)
            {
                if (response.Messages == null || response.Messages.Count == 0)
                    throw new ApplicationException();
                else
                    throw new ApplicationException(response.Messages[0].Text);
            }
        }

        #endregion Standard service response handler
    }
}