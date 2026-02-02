using System;
using System.Net;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.Net;
using System.Threading;

namespace Keane.CH.Framework.Services.Logging.Contracts.Data
{
    /// <summary>
    /// Encapsulates logging information pertaining to a method execution.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01/Services/Logging")]
    [Serializable]
    public class ExecutionContextInfo
    {
        #region Ctor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExecutionContextInfo()
        {
            InitialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        protected void InitialiseMembers()
        {
            this.SystemLayer = SystemLayerType.Services;
            this.MethodParameterList = new List<NameValuePair<string>>();
            this.MethodResult = @"Unknown";
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the system layer within which the method has been invoked.
        /// </summary>
        [DataMember()]
        public SystemLayerType SystemLayer
        { get; set; }

        /// <summary>
        /// Gets or sets the assembly that the calling process has loaded.
        /// </summary>
        [DataMember()]
        public string Assembly
        { get; set; }

        /// <summary>
        /// Gets or sets the type that the calling process is using.
        /// </summary>
        [DataMember()]
        public string TypeName
        { get; set; }

        /// <summary>
        /// Gets or sets the method that the calling process is invoking.
        /// </summary>
        [DataMember()]
        public string Method
        { get; set; }

        /// <summary>
        /// Gets or sets the list of parameters passed to the target method.
        /// </summary>
        [DataMember()]
        public List<NameValuePair<string>> MethodParameterList
        { get; set; }

        /// <summary>
        /// Gets or sets the result of the method invocation (in string format).
        /// </summary>
        [DataMember()]
        public string MethodResult
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets the list of parameters.
        /// </summary>
        /// <param name="parameterValues">The list of method parameter values.</param>
        private void SetParameterList(
            MethodBase method, params object[] parameterValues)
        {
            // Defensive programming.
            if (method == null)
                throw new ArgumentNullException("method");
            ParameterInfo[] parameters = method.GetParameters();

            // Exit if there are no parameters.
            if (parameters.Length == 0) 
                return;

            // Exit if the list of name/values do not equate.
            if (parameters.Length != parameterValues.Length) 
                return;

            // Add parameters accordingly.
            MethodParameterList = new List<NameValuePair<string>>();
            for (int i = 0; i < parameters.Length; i++)
            {
                NameValuePair<string> parameter = new NameValuePair<string>();
                parameter.Name = parameters[i].Name;
                parameter.Value = parameterValues[i].ToString();
                MethodParameterList.Add(parameter);
            }
        }

        #endregion Methods

        #region Static factory

        /// <summary>
        /// Factory method to create an instance.
        /// </summary>
        /// <param name="systemAspect">The aspect of the system being invoked.</param>
        /// <param name="method">The method that has been invoked and is being logged.</param>
        /// <returns>An instance.</returns>
        public static ExecutionContextInfo Create(
            SystemLayerType systemAspect, 
            MethodBase method)
        {
            ExecutionContextInfo result = new ExecutionContextInfo() 
            { 
                Assembly = method.DeclaringType.Assembly.FullName,
                Method = method.Name,
                TypeName = method.DeclaringType.Name,
            };
            return result;            
        }

        /// <summary>
        /// Factory method to create an instance.
        /// </summary>
        /// <param name="systemAspect">The aspect of the system being invoked.</param>
        /// <param name="method">The method that has been invoked and is being logged.</param>
        /// <param name="methodParameters">The method parameters.</param>
        /// <returns>An instance.</returns>
        public static ExecutionContextInfo Create(
            SystemLayerType systemAspect,
            MethodBase method,
            params object[] methodParameters)
        {
            ExecutionContextInfo result = Create(systemAspect, method);
            result.SetParameterList(method, methodParameters);
            return result;
        }

        /// <summary>
        /// Factory method to create an instance.
        /// </summary>
        /// <param name="systemAspect">The aspect of the system being invoked.</param>
        /// <param name="method">The method that has been invoked and is being logged.</param>
        /// <param name="methodResult">The method result.</param>
        /// <returns>An instance.</returns>
        public static ExecutionContextInfo Create(
            SystemLayerType systemAspect,
            MethodBase method,
            object methodResult)
        {
            ExecutionContextInfo result = Create(systemAspect, method);
            result.MethodResult = methodResult.ToString();
            return result;
        }

        /// <summary>
        /// Factory method to create an instance.
        /// </summary>
        /// <param name="systemAspect">The aspect of the system being invoked.</param>
        /// <param name="method">The method that has been invoked and is being logged.</param>
        /// <param name="methodResult">The method result.</param>
        /// <param name="methodParameters">The method parameters.</param>
        /// <returns>An instance.</returns>
        public static ExecutionContextInfo Create(
            SystemLayerType systemAspect,
            MethodBase method, 
            object methodResult,
            params object[] methodParameters)
        {
            ExecutionContextInfo result = Create(systemAspect, method, methodParameters);
            result.MethodResult = methodResult.ToString();
            return result;
        }

        #endregion Static factory
    }
}