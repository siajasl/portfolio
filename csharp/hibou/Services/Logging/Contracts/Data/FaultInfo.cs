using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Encapsualtes information pertaining to a fault.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public class FaultInfo
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FaultInfo()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            this.StackTrace = Environment.StackTrace;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        [DataMember()]
        public string Message
        { get; set; }

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        [DataMember()]
        public string Source
        { get; set; }

        /// <summary>
        /// Gets or sets the stack trace.
        /// </summary>
        [DataMember()]
        public string StackTrace
        { get; set; }

        /// <summary>
        /// Gets or sets the inner fault.
        /// </summary>
        [DataMember()]
        public FaultInfo InnerFault
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the fault.
        /// </summary>
        /// <param name="fault">The fualt being assigned.</param>
        internal void SetFault(Exception fault)
        {
            this.Message = fault.Message;
            this.StackTrace = fault.StackTrace;
            this.Source = fault.Source;
            if (fault.InnerException != null)
            {
                InnerFault = new FaultInfo();
                InnerFault.SetFault(fault.InnerException);
            }
        }

        #endregion Methods
    }
}
