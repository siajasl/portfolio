using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using Keane.CH.Framework.Services.Core.Operation;

namespace Keane.CH.Framework.Services.Core.Operation
{
    /// <summary>
    /// A basic operation response within a SOA system.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public class OperationResponse
    {
        #region Constructor

        public OperationResponse()
        {
            InitialiseState();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the response status.
        /// </summary>
        [DataMember()]
        public OperationResponseStatus Status
        { get; set; }

        /// <summary>
        /// Gets or sets the list of messages associated with the response.
        /// </summary>
        [DataMember()]
        public List<OperationContextMessage> Messages
        { get; set; }

        #region Non DataMembers

        /// <summary>
        /// Gets a flag indicating whether the operation succeeded.
        /// </summary>
        public bool Succeeded
        { get { return (Status == OperationResponseStatus.Success); } }

        /// <summary>
        /// Gets a flag indicating whether the operation failed.
        /// </summary>
        public bool Failed
        { get { return (Status == OperationResponseStatus.Failure); } }

        /// <summary>
        /// Gets a flag indicating whether the operation faulted.
        /// </summary>
        public bool Faulted
        { get { return (Status == OperationResponseStatus.Exception); } }

        /// <summary>
        /// Gets a flag indicating whether there are messages with the response.
        /// </summary>
        public bool HasMessages
        { get { return (Messages != null && Messages.Count > 0); } }

        #endregion Non DataMembers

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialises the response state.
        /// </summary>
        protected virtual void InitialiseState()
        {
            Messages = new List<OperationContextMessage>();
            Status = OperationResponseStatus.Unknown;
        }

        /// <summary>
        /// Adds a context message to the collection.
        /// </summary>
        /// <param name="contextMessage">The context message being dispatched.</param>
        public void AddOperationContextMessage(
            OperationContextMessage contextMessage)
        {
            Messages.Add(contextMessage);
        }

        /// <summary>
        /// Adds a context message to the collection.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <param name="type">The message type.</param>
        public void AddOperationContextMessage(
            string text, OperationContextMessageType type)
        {
            OperationContextMessage contextMessage = new OperationContextMessage()
            {
                 Text = text,
                 Type = type
            };
            Messages.Add(contextMessage);
        }

        /// <summary>
        /// Adds a context message to the collection.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <param name="type">The message type.</param>
        public void AddOperationContextMessage(
            string text, OperationContextMessageType type, string culture)
        {
            OperationContextMessage contextMessage = new OperationContextMessage()
            {
                Text = text,
                Type = type,
                Culture = culture
            };
            Messages.Add(contextMessage);
        }

        #endregion Methods

        #region Static factory methods

        /// <summary>
        /// Factory method to return an instance injected with the passed status.
        /// </summary>
        /// <param name="status">The operation response status.</param>
        /// <returns>An operation response instance.</returns>
        private static OperationResponse GetResponseForStatus(
            OperationResponseStatus status)
        {
            OperationResponse result = new OperationResponse();
            result.Status = status;
            return result;
        }

        /// <summary>
        /// Factory method to return an unknown operation response.
        /// </summary>
        /// <returns>An operation response instance.</returns>
        public static OperationResponse GetUnknown()
        {
            return GetResponseForStatus(OperationResponseStatus.Unknown);
        }

        /// <summary>
        /// Factory method to return a success operation response.
        /// </summary>
        /// <returns>An operation response instance.</returns>
        public static OperationResponse GetSuccess()
        {
            return GetResponseForStatus(OperationResponseStatus.Success);
        }

        /// <summary>
        /// Factory method to return a failure operation response.
        /// </summary>
        /// <returns>An operation response instance.</returns>
        public static OperationResponse GetFailure()
        {
            return GetResponseForStatus(OperationResponseStatus.Failure);
        }

        /// <summary>
        /// Creates & returns an instance assigned a failure status.
        /// </summary>
        /// <typeparam name="T">The type of response being returned.</typeparam>
        /// <returns>An instance.</returns>
        public static OperationResponse<T> GetFailure<T>()
        {
            OperationResponse<T> response = new OperationResponse<T>();
            response.Status = OperationResponseStatus.Failure;
            return response;
        }

        /// <summary>
        /// Creates & returns an instance assigned a failure status.
        /// </summary>
        /// <typeparam name="T">The type of response being returned.</typeparam>
        /// <param name="result">The response result.</param>
        /// <returns>An instance.</returns>
        public static OperationResponse<T> GetFailure<T>(T result)
        {
            OperationResponse<T> response = GetFailure<T>();
            response.Result = result;
            return response;
        }

        /// <summary>
        /// Factory method to return an exception operation response.
        /// </summary>
        /// <returns>An exception operation response instance.</returns>
        public static OperationResponse GetException(Exception exception)
        {
            OperationResponse response = new OperationResponse();
            response.Status = OperationResponseStatus.Exception;
            response.AddOperationContextMessage(exception.Message, OperationContextMessageType.Error);
            return response;
        }

        #region Generic response factory methods

        /// <summary>
        /// Creates & returns an instance assigned an unknown status.
        /// </summary>
        /// <typeparam name="T">The type of response data being returned.</typeparam>
        /// <param name="result">The response result.</param>
        /// <returns>A generic operation instance.</returns>
        public static OperationResponse<T> GetUnknown<T>()
        {
            OperationResponse<T> response = new OperationResponse<T>();
            response.Result = default(T);
            response.Status = OperationResponseStatus.Unknown;
            return response;
        }

        /// <summary>
        /// Creates & returns an instance assigned a success status.
        /// </summary>
        /// <typeparam name="T">The type of response being returned.</typeparam>
        /// <param name="result">The response result.</param>
        /// <returns>An instance.</returns>
        public static OperationResponse<T> GetSuccess<T>(T result)
        {
            OperationResponse<T> response = new OperationResponse<T>();
            response.Result = result;
            response.Status = OperationResponseStatus.Success;
            return response;
        }

        /// <summary>
        /// Creates & returns an instance assigned a failure status.
        /// </summary>
        /// <typeparam name="T">The type of response being returned.</typeparam>
        /// <param name="exception">The application exception that has occurred.</param>
        /// <param name="result">The response result.</param>
        /// <returns>An instance.</returns>
        public static OperationResponse<T> GetException<T>(Exception exception)
        {
            OperationResponse<T> response = new OperationResponse<T>();
            response.Result = default(T);
            response.Status = OperationResponseStatus.Exception;
            response.AddOperationContextMessage(exception.Message, OperationContextMessageType.Error);
            return response;
        }

        #endregion Generic response factory methods

        #endregion Static factory methods
    }

    /// <summary>
    /// Generic base class inherited by operation response messages within a SOA system.
    /// </summary>
    /// <typeparam name="T">The type of response data being returned.</typeparam>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class OperationResponse<T> : OperationResponse
    {
        #region Constructor

        public OperationResponse()
            : base()
        { }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// The operation response result.
        /// </summary>
        [DataMember()]
        public T Result
        { get; set; }

        #endregion Properties
    }
}