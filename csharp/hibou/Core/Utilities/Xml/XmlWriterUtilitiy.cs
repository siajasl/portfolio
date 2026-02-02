using System;
using System.Text;
using System.Xml;
using Keane.CH.Framework.Core.Resources.Xml;

namespace Keane.CH.Framework.Core.Utilities.Xml
{
    /// <summary>
    /// Encapsulates the writing of common elements.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class XmlWriterUtility
    {
        #region Ctor

        private XmlWriterUtility() { }

        #endregion Ctor

        #region Methods

        public static void EmitDateTimeStamp(XmlWriter writer, DateTime value)
        {
            EmitElement(
                writer,
                XmlElementConstants.DateTimeStamp,
                Convert.ToString(value));
        }

        public static XmlNode EmitDateTimeStamp(XmlNode contextNode, DateTime value)
        {
            return
                EmitElement(
                    contextNode,
                    XmlElementConstants.DateTimeStamp,
                    Convert.ToString(value));
        }

        public static void EmitDateTimeStamp(XmlWriter writer)
        {
            EmitDateTimeStamp(writer, DateTime.Now);
        }

        public static XmlNode EmitDateTimeStamp(XmlNode contextNode)
        {
            return EmitDateTimeStamp(contextNode, DateTime.Now);
        }

        public static void EmitResponseCode(XmlWriter writer, string value)
        {
            EmitElement(
                writer,
                XmlElementConstants.ResponseCode,
                value);
        }

        public static XmlNode EmitResponseCode(XmlNode contextNode, string value)
        {
            return
                EmitElement(
                    contextNode,
                    XmlElementConstants.ResponseCode,
                    value);
        }

        public static void EmitType(XmlWriter writer, string value)
        {
            EmitElement(
                writer,
                XmlElementConstants.Type,
                value);
        }

        public static XmlNode EmitType(XmlNode contextNode, string value)
        {
            return
                EmitElement(
                    contextNode,
                    XmlElementConstants.Type,
                    value);
        }

        public static void EmitDataContract(XmlWriter writer, string value)
        {
            EmitElement(
                writer,
                XmlElementConstants.DataContract,
                value);
        }

        public static XmlNode EmitDataContract(XmlNode contextNode, string value)
        {
            return
                EmitElement(
                    contextNode,
                    XmlElementConstants.DataContract,
                    value);
        }

        public static void EmitFault(XmlWriter writer, Exception value)
        {
            EmitElement(
                writer,
                XmlElementConstants.ErrorMessage,
                value.Message);
        }

        public static XmlNode EmitFault(XmlNode contextNode, Exception value)
        {
            return
                EmitElement(
                    contextNode,
                    XmlElementConstants.ErrorMessage,
                    value.Message);
        }

        public static void EmitElement(XmlWriter writer, string name, string value)
        {
            writer.WriteStartElement(name);
            if (!String.IsNullOrEmpty(value)) writer.WriteRaw(value);
            writer.WriteEndElement();
        }

        public static XmlNode EmitElement(XmlNode contextNode, string name, string value)
        {
            XmlNode node = EmitElement(contextNode, name);
            try
            {
                node.InnerXml = value;
            }
            catch (XmlException ex)
            {
                if (ex != null) // done so as to override warning.
                    node.InnerText = value;
            }
            return node;
        }

        public static XmlNode EmitElement(XmlNode contextNode, string name)
        {
            XmlNode node =
                contextNode.OwnerDocument.CreateElement(name);
            contextNode.AppendChild(node);
            return node;
        }

        public static XmlNode EmitElement(XmlDocument contextDoc, string name)
        {
            XmlNode node =
                contextDoc.CreateElement(name);
            contextDoc.AppendChild(node);
            return node;
        }

        /// <summary>
        /// Writes an xml start element.
        /// </summary>
        /// <param name="tabCount">The number of tabs to prepend.</param>
        /// <param name="name">The name of the xml element.</param>
        /// <returns>The xml start element.</returns>
        public static string WriteElement(uint tabCount, string name, object value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WriteStartElement(tabCount, name));
            if (value != null)
                sb.Append(value.ToString());
            else
                sb.Append(String.Empty);
            sb.Append(WriteEndElement(0, name));
            return sb.ToString();
        }

        /// <summary>
        /// Writes an xml start element.
        /// </summary>
        /// <param name="tabCount">The number of tabs to prepend.</param>
        /// <param name="name">The name of the xml element.</param>
        /// <returns>The xml start element.</returns>
        public static string WriteStartElement(uint tabCount, string name, bool appendCloseTag)
        {
            StringBuilder sb = new StringBuilder();
            for (uint i = 0; i < tabCount; i++)
            {
                sb.Append("\t");
            }
            sb.Append("<");
            sb.Append(name);
            if (appendCloseTag)
                sb.Append(">");
            return sb.ToString();
        }

        /// <summary>
        /// Writes an xml start element.
        /// </summary>
        /// <param name="tabCount">The number of tabs to prepend.</param>
        /// <param name="name">The name of the xml element.</param>
        /// <returns>The xml start element.</returns>
        public static string WriteStartElement(uint tabCount, string name)
        {
            return WriteStartElement(tabCount, name, true);
        }

        /// <summary>
        /// Writes an xml end element.
        /// </summary>
        /// <param name="tabCount">The number of tabs to prepend.</param>
        /// <param name="name">The name of the xml element.</param>
        /// <returns>The xml end element.</returns>
        public static string WriteEndElement(uint tabCount, string name)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tabCount; i++)
            {
                sb.Append("\t");
            }
            sb.Append("</");
            sb.Append(name);
            sb.Append(">");
            return sb.ToString();
        }

        /// <summary>
        /// Writes an xml attribute.
        /// </summary>
        /// <param name="name">The name of the xml attribute.</param>
        /// <param name="name">The value of the xml attribute.</param>
        /// <returns>The xml attribute.</returns>
        public static string WriteAttribute(string name, object value)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@" ");
            sb.Append(name);
            sb.Append(@"=");
            sb.Append(@"""");
            sb.Append(value);
            sb.Append(@"""");
            return sb.ToString();
        }

        #endregion Methods
    }
}
