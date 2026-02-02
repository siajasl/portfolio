using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Keane.CH.Framework.Core.Utilities.Net
{
    /// <summary>
    /// Encapsulates dns related functions.
    /// </summary>
    public sealed class DNSUtility
    {
        #region Ctor.

        private DNSUtility() { }

        #endregion Ctor.

        #region Methods

        /// <summary>
        /// Returns the machines IP address.
        /// </summary>
        /// <returns>The machines IP address.</returns>
        public static string GetMachineIPAddress()
        {
            string result = string.Empty;
            string hostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            foreach (IPAddress ipAddress in ipEntry.AddressList)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    result = ipAddress.ToString();
                    break;                
                }
            }
            return result;
        }

        #endregion Methods
    }
}
