using System;
using System.Xml;
using System.Xml.Serialization;

namespace Keane.CH.Framework.Core.Utilities.Xml
{
    /// <summary>
    /// Encapsualtes xml deserialization utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class XmlDeserializationUtility
    {
        #region Ctor.

        private XmlDeserializationUtility() { }

        #endregion Ctor.

        #region Methods

        /// <summary>
        /// Deserializes an instance of an object from the passed type and xml.
        /// </summary>
        /// <param name="type">The type to be deserialized.</param>
        /// <param name="rawXml">The raw xml to deserialize.</param>
        /// <returns>An object pointer if xml deserialization passed.</returns>
        public static object DeserializeTypeFromXml(Type type, string rawXml)
        {
            XmlReader reader = XmlUtility.GetReaderFromRawXml(rawXml);
            return DeserializeTypeFromXmlReader(type, reader);
        }

        /// <summary>
        /// Deserializes an instance of an object from the passed type and xml reader.
        /// </summary>
        /// <param name="type">The type to be deserialized.</param>
        /// <param name="reader">The reader to deserialize from.</param>
        /// <returns>An object pointer if xml deserialization passed.</returns>
        public static object DeserializeTypeFromXmlReader(Type type, XmlReader reader)
        {
            Type[] typeList = { type };
            XmlSerializer[] s = XmlSerializer.FromTypes(typeList);
            if (s[0].CanDeserialize(reader))
            {
                return s[0].Deserialize(reader);
            }
            else
            {
                return null;
            }
        }

        #endregion Methods
    }
}
