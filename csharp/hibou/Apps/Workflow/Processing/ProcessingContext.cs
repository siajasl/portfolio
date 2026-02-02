using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Services.Entity.Contracts.Data;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// Encapsualtes information relating to a workflow processing context.
    /// </summary>
    /// <remarks>
    /// This is often passed between activities during the processing of sequential workflows.
    /// </remarks>
    [Serializable]
    public class ProcessingContext
    {
        #region Constructor

        protected ProcessingContext()
        {
            this.InitialiseMembers();
        }

        #endregion Constructor

        #region Events

        /// <summary>
        /// The event handler invoked when a processing fault has occurred.
        /// </summary>
        public event ErrorEventDelegate OnErrorEventHandler;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the associated timer.
        /// </summary>
        public ProcessingTimer Timer
        { get ; set ; }

        /// <summary>
        /// Gets the associated fault.
        /// </summary>
        public Exception Fault
        { get; set; }

        /// <summary>
        /// Gets the current processing state.
        /// </summary>
        public ProcessingState State
        {
            get 
            {
                return
                    (this.IsInError ? ProcessingState.Fault : ProcessingState.Success);
            }
        }

        /// <summary>
        /// Gets whether a processing error has occurrred.
        /// </summary>
        public bool IsInError
        {
            get { return (this.ErrorCollection.Count > 0); }
        }

        /// <summary>
        /// Gets the collection of errors.
        /// </summary>
        private EntityBaseCollection<Error> ErrorCollection
        { get; set; }

        /// <summary>
        /// Gets a read only collection of errors.
        /// </summary>
        public IEnumerable<Error> Errors
        { 
            get 
            {
                return this.ErrorCollection.AsEnumerable<Error>();
            } 
        }

        /// <summary>
        /// Gets a single string representatoin of the error messages.
        /// </summary>
        public string GetErrorMessage()
        {
            string result = string.Empty;
            if (this.IsInError && 
                this.ErrorCollection != null)
            {                
                StringBuilder errorMessage = new StringBuilder();
                this.ErrorCollection.ForEach(
                    e => errorMessage.AppendLine(
                        string.Format(e.Message, e.MessageArguments)));
                result = errorMessage.ToString();
            }
            return result;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected virtual void InitialiseMembers()
        {
            this.ErrorCollection = new EntityBaseCollection<Error>();
            this.Timer = new ProcessingTimer(true);
        }

        /// <summary>
        /// Adds a processing error to the context.
        /// </summary>
        /// <param name="error"></param>
        public void AddError(Error error)
        {
            this.ErrorCollection.Add(error);
            if (this.OnErrorEventHandler != null)
                this.OnErrorEventHandler(this, new ErrorEventArgs(error));
        }

        /// <summary>
        /// Adds a processing error to the context.
        /// </summary>
        public void AddError(
            string code,
            params string[] messageArguments)
        {
            this.AddError(Error.Create<Error>(code, messageArguments));
        }

        /// <summary>
        /// Adds a processing error to the context.
        /// </summary>
        public void AddError(
            int code,
            params string[] messageArguments)
        {
            this.AddError(code.ToString(), messageArguments);
        }

        /// <summary>
        /// Adds a processing fault to the context.
        /// </summary>
        /// <remarks>The assigned error code is 9999.</remarks>
        public void AddFault(Exception fault)
        {
            this.Fault = fault;
            this.AddError(Error.Create<Error>(fault));
        }

        #endregion Methods
    }
}
