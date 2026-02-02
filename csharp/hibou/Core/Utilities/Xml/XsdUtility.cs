using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Keane.CH.Framework.Core.Utilities.Xml
{
    /// <summary>
    /// Encapsualtes xsd utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class XsdUtility
    {
        #region Ctor.

        private XsdUtility() { }

        #endregion Ctor.

        #region Fields

        private static XsdErrorDetails xsdErrorDetails;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets\Sets the error details.
        /// </summary>
        private static XsdErrorDetails ErrorDetails
        {
            get
            {
                // Just-in-time instantiation.
                if (xsdErrorDetails == null)
                    ErrorDetails = new XsdErrorDetails();
                return xsdErrorDetails;
            }
            set { xsdErrorDetails = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the schema from the file system
        /// </summary>
        /// <param name="xsdFilePath">The path to the schema.</param>
        /// <returns>An xml schema if loaded otherwise null.</returns>
        public static XmlSchema LoadSchemaFromFileSystem(
            string xsdFilePath)
        {
            XmlSchema instance = null;
            if (File.Exists(xsdFilePath))
            {
                try
                {
                    // Reset error details.
                    ErrorDetails.Reset();

                    // Load.
                    StreamReader sr = new StreamReader(xsdFilePath);
                    XmlSchema xmlSchema = XmlSchema.Read(sr, ErrorDetails.XsdLoadingFailureHandler);

                    // Use if loaded.
                    if (ErrorDetails.IsValid)
                        instance = xmlSchema;
                }
                catch
                {
                    instance = null;
                }
            }
            return instance;
        }

        /// <summary>
        /// Loads the schema from the file system
        /// </summary>
        /// <param name="xsdAsString">The nsd in a string format.</param>
        /// <returns>An xml schema if loaded otherwise null.</returns>
        public static XmlSchema LoadSchemaFromString(
            string xsdAsString)
        {
            XmlSchema instance = null;
            if (!String.IsNullOrEmpty(xsdAsString))
            {
                try
                {
                    // Reset error details.
                    ErrorDetails.Reset();

                    // Load.
                    XmlTextReader tr = new XmlTextReader(xsdAsString, XmlNodeType.Document, null);
                    XmlSchema xmlSchema = XmlSchema.Read(tr, ErrorDetails.XsdLoadingFailureHandler);

                    // Use if loaded.
                    if (ErrorDetails.IsValid)
                        instance = xmlSchema;
                }
                catch
                {
                    instance = null;
                }
            }
            return instance;
        }

        /// <summary>
        /// Validates the passed xml document against the passed xsd document.
        /// </summary>
        /// <param name="doc">The xml document to be validated.</param>
        /// <param name="xsd">The xsd document to validate against.</param>
        public static bool ValidateXml(
            XmlDocument xml,
            XmlSchema xsd)
        {
            return ValidateXml(xml.OuterXml, xsd);
        }

        /// <summary>
        /// Validates the passed xml string against the passed xsd document.
        /// </summary>
        /// <param name="doc">The xml string to be validated.</param>
        /// <param name="xsd">The xsd document to validate against.</param>
        public static bool ValidateXml(
            string xml,
            XmlSchema xsd)
        {
            if (xml == null)
                return false;
            if (xsd == null)
                return false;
            bool result = false;
            try
            {
                // Create the xml reader settings.
                XmlReaderSettings xrs = new XmlReaderSettings();
                xrs.ConformanceLevel = ConformanceLevel.Fragment;
                xrs.Schemas.Add(xsd);
                xrs.ValidationType = ValidationType.Schema;
                xrs.ValidationEventHandler += new ValidationEventHandler(ErrorDetails.XsdValidationFailureHandler);

                // Validate via the xml reader.
                XmlTextReader tr = new XmlTextReader(xml, XmlNodeType.Element, null);
                XmlReader xr = XmlReader.Create(tr, xrs);
                XmlDocument doc = new XmlDocument();
                doc.Load(xr);
                result = ErrorDetails.IsValid;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        #endregion Methods

        #region Nested class

        /// <summary>
        /// Encapsulates the error details when loading/validaton occurs.
        /// </summary>
        private class XsdErrorDetails
        {
            #region Ctor

            /// <summary>
            /// Ctor.
            /// </summary>
            public XsdErrorDetails()
            {
                Reset();
            }

            #endregion Ctor

            #region Fields

            private int errorCount;
            private string errorMessages;

            #endregion Fields

            #region Properties

            /// <summary>
            /// The count of validation errors that occurred when a schema was loaded.
            /// </summary>
            public int ErrorCount
            {
                get { return this.errorCount; }
                set { this.errorCount = value; }
            }

            /// <summary>
            /// The validation messages that were received when a schema was loaded.
            /// </summary>
            public string ErrorMessages
            {
                get { return this.errorMessages; }
                set { this.errorMessages = value; }
            }

            #endregion Properties

            #region Methods

            /// <summary>
            /// Resets the count/message.
            /// </summary>
            public void Reset()
            {
                ErrorMessages = String.Empty;
                ErrorCount = 0;
            }

            /// <summary>
            /// Was the schema process valid or not.
            /// </summary>
            public bool IsValid
            {
                get { return (ErrorCount == 0); }
            }

            /// <summary>
            /// Adds an error message.
            /// </summary>
            public void AddErrorMessage(string errorMessage)
            {
                ErrorMessages += errorMessage;
                ErrorMessages += "\r\n";
                ErrorCount++;
            }

            #endregion Methods

            #region Event handler

            /// <summary>
            /// Handler invoked when xsd loading fails.
            /// </summary>
            public void XsdLoadingFailureHandler(
                object sender,
                ValidationEventArgs args)
            {
                ErrorDetails.AddErrorMessage(args.Message);
            }

            /// <summary>
            /// Handler invoked when xsd validation fails.
            /// </summary>
            public void XsdValidationFailureHandler(
                object sender,
                ValidationEventArgs args)
            {
                ErrorDetails.AddErrorMessage(args.Message);
            }

            #endregion Event handler
        }

        #endregion Nested class
    }
}
