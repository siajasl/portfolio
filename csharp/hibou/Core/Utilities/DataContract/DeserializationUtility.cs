using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.Diagnostics;

namespace Keane.CH.Framework.Core.Utilities.DataContract
{
    /// <summary>
    /// Encapsulates data contract deserialization utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class DeserializationUtility
    {
        #region Ctor.

        private DeserializationUtility() { }

        #endregion Ctor.

        #region Deserialization

        #region String

        /// <summary>
        /// Deserializes a data contract from a string.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractAsString">A string representation (i.e. xml) of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static T DeserializeFromString<T>(string dataContractAsString)
        {
            return (T)DeserializeFromString(dataContractAsString, typeof(T));
        }

        /// <summary>
        /// Deserializes a data contract from a string.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractAsString">A string representation (i.e. xml) of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static object DeserializeFromString(string dataContractAsString, Type objectType)
        {
            using (StringReader sr = new StringReader(dataContractAsString))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    using (XmlDictionaryReader xdr = XmlDictionaryReader.CreateDictionaryReader(xr))
                    {
                        DataContractSerializer dcs =
                            new DataContractSerializer(objectType);
                        return dcs.ReadObject(xdr, true);
                    }
                }
            }
        }

        /// <summary>
        /// Deserializes a data contract from a string.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractAsString">A string representation (i.e. xml) of the data contract.</param>
        /// <param name="rootName">The root node name.</param>
        /// <param name="rootNamespace">The root namespace name embedded within the document.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static T DeserializeFromString<T>(
            string dataContractAsString,
            string rootName,
            string rootNamespace)
        {
            return (T)DeserializeFromString(dataContractAsString, rootName, rootNamespace, typeof(T));
        }

        /// <summary>
        /// Deserializes a data contract from a string.
        /// </summary>
        /// <param name="dataContractAsString">A string representation (i.e. xml) of the data contract.</param>
        /// <param name="rootName">The root node name.</param>
        /// <param name="rootNamespace">The root namespace name embedded within the document.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static object DeserializeFromString(
            string dataContractAsString,
            string rootName,
            string rootNamespace,
            Type objectType)
        {
            using (StringReader sr = new StringReader(dataContractAsString))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    using (XmlDictionaryReader xdr = XmlDictionaryReader.CreateDictionaryReader(xr))
                    {
                        DataContractSerializer dcs =
                            new DataContractSerializer(objectType, rootName, rootNamespace);
                        return dcs.ReadObject(xdr, true);
                    }
                }
            }
        }

        #endregion String

        #region Xml Document

        /// <summary>
        /// Deserializes a data contract from an xml document.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractXmlDocument">An xml document representation of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static T DeserializeFromXmlDocument<T>(
            XmlDocument dataContractXmlDocument)
        {
            return DeserializeFromString<T>(dataContractXmlDocument.OuterXml);
        }

        /// <summary>
        /// Deserializes a data contract from an xml document.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractXmlDocument">An xml document representation of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static object DeserializeFromXmlDocument(
            XmlDocument dataContractXmlDocument,
            Type objectType)
        {
            return DeserializeFromString(dataContractXmlDocument.OuterXml, objectType);
        }

        /// <summary>
        /// Deserializes a data contract from an xml document.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractXmlDocument">An xml document representation of the data contract.</param>
        /// <param name="rootName">The root node name.</param>
        /// <param name="rootNamespace">The root namespace name embedded within the document.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static object DeserializeFromXmlDocument(
            XmlDocument dataContractXmlDocument,
            string rootName,
            string rootNamespace,
            Type objectType)
        {
            return DeserializeFromString(dataContractXmlDocument.OuterXml, rootName, rootNamespace, objectType);
        }

        /// <summary>
        /// Deserializes a data contract from an xml document.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractXmlDocument">An xml document representation of the data contract.</param>
        /// <param name="rootName">The root node name.</param>
        /// <param name="rootNamespace">The root namespace name embedded within the document.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static T DeserializeFromXmlDocument<T>(
            XmlDocument dataContractXmlDocument,
            string rootName,
            string rootNamespace)
        {
            return DeserializeFromString<T>(dataContractXmlDocument.OuterXml, rootName, rootNamespace);
        }

        #endregion Xml Document

        #region File

        /// <summary>
        /// Deserializes a data contract from a file.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="file">The file representation of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static T DeserializeFromFile<T>(
            FileInfo file)
        {
            return (T)DeserializeFromFile(file, typeof(T));
        }

        /// <summary>
        /// Deserializes a data contract from a file.
        /// </summary>
        /// <param name="file">The file representation of the data contract.</param>
        /// <param name="objectType">The type of object being deserialized.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static object DeserializeFromFile(
            FileInfo file,
            Type objectType)
        {
            // Defensive programming.
            Debug.Assert(file != null, "file parameter is null.");
            Debug.Assert(file.Exists, string.Format("file does not exist : {0}", file.FullName));
            Debug.Assert(objectType != null, "objectType parameter is null.");

            // Assign default value.
            object result = null;

            // Exception if this is unloadable as xml.
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(file.FullName);
            }
            catch
            {
                throw new ArgumentException(
                    String.Format("Invalid xml file :: {0}.", file.FullName));
            }

            // Exception if deserialzation fails.
            result = DeserializeFromString(xmlDoc.OuterXml, objectType);
            if (result == null)
            {
                throw new ArgumentException(
                    String.Format("Non-deserializable xml file :: {0}.", file.FullName));
            }

            // Return result.
            return result;
        }

        #endregion File

        #region File path

        /// <summary>
        /// Deserializes a data contract from a file.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="filePath">The file path representation of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static T DeserializeFromFilePath<T>(
            string filePath)
        {
            return (T)DeserializeFromFilePath(filePath, typeof(T));
        }

        /// <summary>
        /// Deserializes a data contract from a file.
        /// </summary>
        /// <param name="filePath">The file path representation of the data contract.</param>
        /// <param name="objectType">The type of object being deserialized.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static object DeserializeFromFilePath(
            string filePath, 
            Type objectType)
        {
            // Defensive programming.
            Debug.Assert(File.Exists(filePath), string.Format("file does not exist : {0}", filePath));

            // Delegate.
            FileInfo file = new FileInfo(filePath);
            return DeserializeFromFile(file, objectType);
        }

        #endregion File path

        #region Byte array

        /// <summary>
        /// Deserializes a data contract from a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractAsString">A binary representation (i.e. byte array) of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static T DeserializeFromByteArray<T>(byte[] dataContractAsByteArray)
        {
            return (T)DeserializeFromByteArray(dataContractAsByteArray, typeof(T));
        }

        /// <summary>
        /// Deserializes a data contract from a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContractAsString">A binary representation (i.e. byte array) of the data contract.</param>
        /// <returns>An instance of the data contract type.</returns>
        public static object DeserializeFromByteArray(byte[] dataContractAsByteArray, Type objectType)
        {
            throw new NotImplementedException("TODO: Implement this method correctly.");
            //using (XmlDictionaryReader xdr = XmlDictionaryReader.CreateBinaryReader(dataContractAsByteArray, XmlDictionaryReaderQuotas.Max))
            //{
            //    DataContractSerializer dcs = new DataContractSerializer(typeof(E));
            //    return (E)dcs.ReadObject(xdr, true);
            //}
        }

        #endregion Byte array

        #endregion Deserialization
    }
}