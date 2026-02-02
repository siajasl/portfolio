using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.Exceptions;
using Keane.CH.Framework.Services.Entity.Contracts.Data;
using Keane.CH.Framework.Services.Logging.Contracts.Data;

namespace Keane.CH.Framework.Core.Workflow.Processing
{
    /// <summary>
    /// A processing error that has occurred during processing.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class Error : 
        EntityBase
    {
        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Error()
        {
            this.InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected void InitialiseMembers()
        {
            // Create the default error type.
            this.ErrorType = new ErrorType()
            {
                Category = ErrorCategoryType.Processing,
                LogMessageCategory = LogMessageWriterType.Error.ToString(),
                Message = "Application exception : {0}",
                Severity = ErrorSeverityType.High,
                CodeAsInt = (int)ApplicationExceptionType.UnknownException
            };            
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the associated error type.
        /// </summary>
        [DataMember()]
        public ErrorType ErrorType
        { get; set; }

        /// <summary>
        /// Gets or sets the error type code.
        /// </summary>
        [DataMember()]
        public string ErrorTypeCode
        { get; set; }

        /// <summary>
        /// Gets or sets an array of context ids
        /// </summary>
        [DataMember()]
        public int[] ContextIds
        { get; set; }
        
        /// <summary>
        /// Gets or sets an array of message arguments.
        /// </summary>
        [DataMember()]
        public string[] MessageArguments
        { get; set; }
      
        /// <summary>
        /// Gets the formatted error message.
        /// </summary>
        public string Message
        {
            get
            {
                // Default value.
                string result = String.Empty;
                
                // Format the unformatted message if arguments have been passed.
                if (this.MessageArguments != null && 
                    this.ErrorType != null)
                {
                    try
                    {
                        result = String.Format(this.ErrorType.Message, MessageArguments);
                    }
                    catch
                    {
                        result = this.ErrorType.Message;
                    }
                }
                else if (ErrorType != null)
                {
                    result = this.ErrorType.Message;
                }
                
                // Trim.
                result = result.Trim();

                // Return.
                return result;
            }
        }

        #endregion Properties

        #region Static factory

        /// <summary>
        /// Simple static factory for returning an instantiated instance.
        /// </summary>
        /// <param name="errorTypeCode">The error type code.</param>
        /// <param name="messageArguments">The error message arguments.</param>
        /// <returns>A processing error instance.</returns>
        public static E Create<E>(
            string errorTypeCode, 
            string[] messageArguments)
            where E : Error, new()
        {
            E error = new E()
            {
                ErrorTypeCode = errorTypeCode,
                MessageArguments = messageArguments
            };
            return error;
        }

        /// <summary>
        /// Simple static factory for returning an instantiated instance.
        /// </summary>
        /// <param name="errorTypeCode">The error type code.</param>
        /// <param name="messageArguments">The error message arguments.</param>
        /// <returns>A processing error instance.</returns>
        public static E Create<E>(
            ApplicationExceptionType errorTypeCode,             
            string[] messageArguments)
            where E : Error, new()
        {
            E error = Create<E>(
                errorTypeCode.ToString(), 
                messageArguments);
            return error;
        }

        /// <summary>
        /// Simple static factory for returning an instantiated instance.
        /// </summary>
        /// <param name="exception">The exception that has occurred.</param>
        /// <returns>A processing error instance.</returns>
        public static E Create<E>(
            Exception exception)
            where E : Error, new()
        {
            E error = Create<E>(
                ApplicationExceptionType.UnknownException, 
                new string[] { exception.Message });
            return error;
        }

        #endregion Static factory
    }
}