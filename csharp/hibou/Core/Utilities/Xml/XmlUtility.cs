using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace Keane.CH.Framework.Core.Utilities.Xml
{
    /// <summary>
    /// Encapsualtes xml utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class XmlUtility
    {
        #region Ctor.

        private XmlUtility() { }

        #endregion Ctor.

        #region Methods

        /// <summary>
        /// Reads the xml contained within the passed xml dictionary reader.
        /// </summary>
        /// <param name="ignoreRootElement">Flag to indicate whether the root element will be ignored.</param>
        /// <param name="reader">The xml reader containing the xml.</param>
        public static bool IsWellFormed(
            XmlDictionaryReader reader,
            out XmlDocument doc)
        {
            // Document loading will exception if the reader contains invalid xml.
            try
            {
                doc = new XmlDocument();
                doc.Load(reader);
                return true;
            }
            catch 
            {
                doc = null;
                return false;
            }
        }

        /// <summary>
        /// Determines whether the raw xml is well formed or not.
        /// </summary>
        /// <param name="rawXml">The raw xml being tested.</param>
        public static bool IsWellFormed(
            string rawXml,
            out XmlDocument doc)
        {
            // Document loading will exception if the reader contains invalid xml.
            try
            {
                doc = new XmlDocument();
                doc.LoadXml(rawXml);
                return true;
            }
            catch 
            {
                doc = null;
                return false;
            }
        }

        /// <summary>
        /// Returns the passed raw xml as an xml reader.
        /// </summary>
        /// <param name="rawXml">The raw xml to be returned as a reader.</param>
        public static XmlReader GetReaderFromRawXml(
            string rawXml)
        {
            try
            {
                StringReader sr = new StringReader(rawXml);
                XmlReader reader = XmlReader.Create(sr);
                return reader;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Reads the xml contained within the passed xml dictionary reader.
        /// </summary>
        /// <param name="ignoreRootElement">Flag to indicate whether the root element will be ignored.</param>
        /// <param name="reader">The xml reader containing the xml.</param>
        public static string GetRawXmlFromReader(
            XmlDictionaryReader reader, 
            bool ignoreRootElement)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(reader);
                if (ignoreRootElement)
                {
                    return doc.FirstChild.InnerXml;
                }
                else
                {
                    return doc.InnerXml;
                }
            }
            catch
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns an xml doc from the passed raw xml string.
        /// </summary>
        /// <returns>An xml document.</returns>
        public static XmlDocument GetDocFromRawXml(
            string rawXml)
        {
            try
            {
                XmlDocument returnValue = new XmlDocument();
                returnValue.LoadXml(rawXml);
                return returnValue;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Returns an xml doc from the passed filepath.
        /// </summary>
        /// <param name="filePath">The filepath to the xml document in question.</param>
        /// <returns>An xml document.</returns>
        public static XmlDocument GetDocFromFile(
            string filePath)
        {
            try
            {
                XmlDocument returnValue = new XmlDocument();
                returnValue.Load(filePath);
                return returnValue;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Returns an xml doc from the passed xml reader.
        /// </summary>
        /// <returns>An xml document.</returns>
        public static XmlDocument GetDocFromXmlReader(
            XmlDictionaryReader reader)
        {
            try
            {
                XmlDocument returnValue = new XmlDocument();
                returnValue.Load(reader);
                return returnValue;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Transforms the passed xml with the passed transformer.
        /// </summary>
        /// <param name="rawXml">The raw xml to be transformed.</param>
        /// <returns>The transformed xml.</returns>
        public static string TransformXml(
            string rawXml, 
            XslCompiledTransform xsltTransformer)
        {
            StringBuilder sb = new StringBuilder();
            XmlReaderSettings xrs = new XmlReaderSettings();
            xrs.ConformanceLevel = ConformanceLevel.Fragment;
            XmlReader xr = XmlReader.Create(new StringReader(rawXml));
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.ConformanceLevel = ConformanceLevel.Fragment;
            XmlWriter xw = XmlWriter.Create(sb, xws);
            xsltTransformer.Transform(xr, xw);
            return sb.ToString();
        }

        #endregion Methods
    }
}
