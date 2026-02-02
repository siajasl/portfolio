using System.Runtime.Serialization;
using System.Threading;
using System;

namespace Keane.CH.Framework.Services.Core.Operation
{
    /// <summary>
    /// A basic operation request within a SOA system.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public class OperationRequest
    {
        #region Constructor

        public OperationRequest()
        {
            InitialiseState();
        }

        public OperationRequest(OperationRequestContext context) : this()
        {
            if (context == null)
                throw new ArgumentNullException("context");
            Context = context;
        }

        /// <summary>
        /// Initialises the response state.
        /// </summary>
        protected virtual void InitialiseState()
        {
            Context = new OperationRequestContext();
            Context.UserName = Thread.CurrentPrincipal.Identity.Name;
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the request context.
        /// </summary>
        [DataMember()]
        public OperationRequestContext Context
        { get; set; }

        #endregion Properties
    }

    /// <summary>
    /// Generic base class inherited by all request messages within a SOA system.
    /// </summary>
    /// <typeparam name="T">The type of request being returned.</typeparam>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    [Serializable]
    public class OperationRequest<T> : OperationRequest
    {
        #region Constructor

        public OperationRequest() 
            : base()
        { }

        public OperationRequest(OperationRequestContext context)
            : base(context)
        { }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// The request data.
        /// </summary>
        [DataMember()]
        public T Data
        { get; set; }

        #endregion Properties

        #region Static factory

        /// <summary>
        /// Static factory instance.
        /// </summary>
        /// <typeparam name="T">The type of request being returned.</typeparam>
        /// <param name="data">The request data.</param>
        /// <returns>An instance.</returns>
        public static OperationRequest<T> Create(T data)
        {
            OperationRequest<T> result = new OperationRequest<T>();
            result.Data = data;
            return result;
        }

        /// <summary>
        /// Static factory instance.
        /// </summary>
        /// <typeparam name="T">The type of request being returned.</typeparam>
        /// <param name="data">The request data.</param>
        /// <param name="context">The request context.</param>
        /// <returns>An instance.</returns>
        public static OperationRequest<T> Create(
            T data, OperationRequestContext context)
        {
            OperationRequest<T> result = Create(data);
            result.Context = context;
            return result;
        }

        #endregion Static factory
    }
}