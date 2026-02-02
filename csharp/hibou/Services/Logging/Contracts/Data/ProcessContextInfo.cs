using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading;
using Keane.CH.Framework.Core.Utilities.Net;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Encapsulates process context logging information.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public class ProcessContextInfo
    {
        #region Ctor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProcessContextInfo()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected void InitialiseMembers()
        {
            this.MachineName = Environment.MachineName;
            this.MachineIpAddress = DNSUtility.GetMachineIPAddress();
            this.UserDomainName = Environment.UserDomainName;
            this.UserName = Environment.UserName;
            this.ThreadId = Thread.CurrentThread.ManagedThreadId;
            this.ThreadName = Thread.CurrentThread.Name;
            this.MachineOSVersion = Environment.OSVersion.ToString();
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the thread Id upon which the calling process is executing.
        /// </summary>
        [DataMember()]
        public int ThreadId
        { get; set; }

        /// <summary>
        /// Gets or sets the thread name upon which the calling process is executing.
        /// </summary>
        [DataMember()]
        public string ThreadName
        { get; set; }

        /// <summary>
        /// Gets or sets the user name in which the client process is executing.
        /// </summary>
        [DataMember()]
        public string UserName
        { get; set; }

        /// <summary>
        /// Gets or sets the user domain name in which the client process is executing.
        /// </summary>
        [DataMember()]
        public string UserDomainName
        { get; set; }

        /// <summary>
        /// Gets or sets the machine in which the client process is executing.
        /// </summary>
        [DataMember()]
        public string MachineName
        { get; set; }

        /// <summary>
        /// Gets or sets the machine in which the client process is executing.
        /// </summary>
        [DataMember()]
        public string MachineIpAddress
        { get; set; }

        /// <summary>
        /// Gets or sets the operating system in which the client process is executing.
        /// </summary>
        [DataMember()]
        public string MachineOSVersion
        { get; set; }

        #endregion Properties
    }
}