using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Keane.CH.Framework.Core.Utilities.DataContract;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Keane.CH.Framework.Apps.UI.Web.AjaxResponse
{
    /// <summary>
    /// A data transfer object for sending data back to the browser during an Ajax callback.
    /// </summary>
    [DataContract(Namespace = "www.Keane.com/CH/2009/01")]
    public class AjaxResponseData  
    {
        #region Ctor

        public AjaxResponseData()
        {
            IntialiseMembers();
        }

        /// <summary>
        /// Standard member initialisation routine.
        /// </summary>
        private void IntialiseMembers()
        {
            ExceptionCollection = new List<ProcessingException>();
            DomUpdateCollection = new List<DomUpdate>();
            NameValuePairCollection = new List<NameValuePair>();
            FormWasValidated = true;
        }

        #endregion Ctor

        #region Properties

        /// <summary>
        /// Gets or sets the collection of processing exceptions to be returned to the client.
        /// </summary>
        [DataMember(Name = "exceptionCollection")]
        internal List<ProcessingException> ExceptionCollection
        { get; set; }

        /// <summary>
        /// Gets or sets the collection of dom element updates.
        /// </summary>
        [DataMember(Name = "domUpdateCollection")]
        internal List<DomUpdate> DomUpdateCollection
        { get; set; }

        /// <summary>
        /// Gets or sets the collection of name value pairs.
        /// </summary>
        [DataMember(Name = "nameValuePairCollection")]
        internal List<NameValuePair> NameValuePairCollection
        { get; set; }

        /// <summary>
        /// Gets or sets the redirect url to be passed back to the client.
        /// </summary>
        [DataMember(Name = "redirectUrl")]
        public string RedirectUrl
        { get; set; }

        /// <summary>
        /// Gets or sets custom data that may be passed back to the client.
        /// </summary>
        [DataMember(Name = "customData")]
        public object CustomData
        { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether server side form validation passed.
        /// </summary>
        [DataMember(Name = "formWasValidated")]
        public bool FormWasValidated
        { get; set; }

        #endregion Properties

        #region Methods

        #region Processing exceptions

        /// <summary>
        /// Adds a processing exception to be returned to the user.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="severity">The exception sevirty.</param>
        public void AddException(
            string message, ProcessingExceptionSeverityType severity)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");
            ProcessingException item =
                new ProcessingException() { Message = message, Severity = severity };
            ExceptionCollection.Add(item);
        }

        #endregion Processing exceptions

        #region Dom updates

        /// <summary>
        /// Adds a dom update to the list.
        /// </summary>
        /// <param name="clientId">The client side id of the dom element to be updated.</param>
        /// <param name="value">The value of the dom element to be updated.</param>
        /// <param name="type">The type of the dom element to be updated.</param>
        public void AddDomUpdate(
            string clientId, string value, DomUpdateTargetType type)
        {
            AddDomUpdate(clientId, value, type.ToString());
        }

        /// <summary>
        /// Adds a dom update to the list.
        /// </summary>
        /// <param name="clientId">The client side id of the dom element to be updated.</param>
        /// <param name="value">The value of the dom element to be updated.</param>
        /// <param name="type">The type of the dom element to be updated.</param>
        public void AddDomUpdate(
            string clientId, string value, string type)
        {
            if (String.IsNullOrEmpty(clientId))
                throw new ArgumentNullException("clientId");
            DomUpdate item =
                new DomUpdate() { ClientId = clientId, Value = value, Type = type };
            DomUpdateCollection.Add(item);
        }

        #endregion Dom updates

        #region Name value pairs

        /// <summary>
        /// Adds a name value pair to the list.
        /// </summary>
        /// <param name="name">The name of the item being added to the list.</param>
        /// <param name="value">The value of the item being added to the list.</param>
        public void AddNameValuePair(
            string name, string value)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty.");
            NameValuePair item =
                new NameValuePair() { Name = name, Value = value };
            AddNameValuePair(item);
        }

        /// <summary>
        /// Adds a name value pair to the list.
        /// </summary>
        /// <param name="item">The item being added to the list.</param>
        public void AddNameValuePair(
            NameValuePair item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            NameValuePairCollection.Add(item);
        }

        #endregion Name value pairs

        #region Serialization

        /// <summary>
        /// Returns a Json representation.
        /// </summary>
        /// <returns>a Json representation.</returns>
        public string AsJson()
        {
            return SerializationUtility.SerializeToJson(this);
        }

        #endregion Serialization

        #endregion Methods

        #region Static methods


        #endregion Static methods
    }
}