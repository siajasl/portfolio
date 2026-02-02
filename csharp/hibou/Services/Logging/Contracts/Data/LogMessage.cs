using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Base class for all log message types.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public class LogMessage
    {
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public LogMessage()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public LogMessage(Exception fault) 
            : this()
        {
            this.SetFault(fault);
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            this.Id = 0;
            this.ExecutionContext = new ExecutionContextInfo();
            this.EventType = LogEventType.Information;
            this.MessageData = new List<NameValuePair<string>>();
            this.TimeStamp = DateTime.Now;
            this.WriterType = LogMessageWriterType.Information;
            this.Priority = LogMessagePriorityType.Low;
            this.ProcessContext = new ProcessContextInfo();
            this.Verbosity = LogMessageVerbosityType.Medium;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the log message id.
        /// </summary>
        [DataMember()]
        public int Id
        { get; set; }

        /// <summary>
        /// Gets or sets the log message timestamp.
        /// </summary>
        [DataMember()]
        public DateTime TimeStamp
        { get; set; }

        /// <summary>
        /// Gets or sets the log message text.
        /// </summary>
        [DataMember()]
        public string Message
        { get; set; }

        /// <summary>
        /// Gets or sets the log message event type.
        /// </summary>
        [DataMember()]
        public LogEventType EventType
        { get; set; }

        /// <summary>
        /// Gets or sets the log message writer.
        /// </summary>
        [DataMember()]
        public LogMessageWriterType WriterType
        { get; set; }

        /// <summary>
        /// Gets or sets the log message priority.
        /// </summary>
        [DataMember()]
        public LogMessagePriorityType Priority
        { get; set; }

        /// <summary>
        /// Gets or sets the log message verbosity.
        /// </summary>
        [DataMember()]
        public LogMessageVerbosityType Verbosity
        { get; set; }

        /// <summary>
        /// Gets or sets the execution context details.
        /// </summary>
        [DataMember()]
        public ExecutionContextInfo ExecutionContext
        { get; set; }

        /// <summary>
        /// Gets or sets the process context details.
        /// </summary>
        [DataMember()]
        public ProcessContextInfo ProcessContext
        { get; set; }

        /// <summary>
        /// Gets or sets the context message data.
        /// </summary>
        [DataMember()]
        public List<NameValuePair<string>> MessageData
        { get; set; }

        /// <summary>
        /// Gets or sets the fault information that may have occurred during execution.
        /// </summary>
        [DataMember()]
        public FaultInfo FaultInfo
        { get; set; }

        /// <summary>
        /// Gets whether the execution faulted.
        /// </summary>
        public bool WasFaulted
        {
            get
            {
                return (FaultInfo != null);
            }
        }       

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Sets the message value.
        /// </summary>
        /// <param name="message">The message being assigned.</param>
        /// <param name="messageArgs">The message arguments.</param>
        public virtual void SetMessage(
            string message, params object[] messageArgs)
        {
            try
            {
                Message = String.Format(message, messageArgs);
            }
            catch
            {
                Message = message;
            }
        }

        /// <summary>
        /// Sets the fault.
        /// </summary>
        /// <param name="fault">The fualt being assigned.</param>
        public void SetFault(Exception fault)
        {
            this.FaultInfo = new FaultInfo();
            this.FaultInfo.SetFault(fault);
            this.Verbosity = LogMessageVerbosityType.High;
            this.EventType = LogEventType.Error;
            this.Priority = LogMessagePriorityType.High;
            // Ensure that faults are mapped to the error writer unless otherwise specified.
            if (this.WriterType != LogMessageWriterType.ErrorCritical &&
                this.WriterType != LogMessageWriterType.Security &&
                this.WriterType != LogMessageWriterType.SecurityCritical)
                this.WriterType = LogMessageWriterType.Error;
        }


        #endregion Public Methods
    }
}