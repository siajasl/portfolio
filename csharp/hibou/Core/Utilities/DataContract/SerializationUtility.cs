using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace Keane.CH.Framework.Core.Utilities.DataContract
{
    /// <summary>
    /// Encapsulates data contract serialization utility functions.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jan-2008" />
    public sealed class SerializationUtility
    {
        #region Ctor.

        private SerializationUtility() { }

        #endregion Ctor.

        #region Public static methods

        #region Serialization

        /// <summary>
        /// Serializes the data contract according to the target serialization type.
        /// </summary>
        /// <param name="serializationType">The target serialization type.</param>
        /// <returns>The serialized object.</returns>
        public static object Serialize(
            SerializationType serializationType, object dataContract)
        {
            object result = null;
            switch (serializationType)
            {
                case SerializationType.Json:
                    result = SerializeToJson(dataContract);
                    break;
                case SerializationType.XmlDoc:
                    result = SerializeToXmlDocument(dataContract);
                    break;
                case SerializationType.XmlString:
                    result = SerializeToString(dataContract);
                    break;
                default:
                    break;
            }
            return result;
        }

        #region String

        /// <summary>
        /// Serializes the data contract to string.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to a string.</param>
        /// <returns>A string representation (i.e. xml) of the data contract.</returns>
        public static string SerializeToString<T>(T dataContract)
        {
            return SerializeToString(dataContract);
        }

        /// <summary>
        /// Serializes the data contract to string.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to a string.</param>
        /// <returns>A string representation (i.e. xml) of the data contract.</returns>
        public static string SerializeToString(object dataContract)
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter xw = XmlWriter.Create(sb))
            {
                using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateDictionaryWriter(xw))
                {
                    DataContractSerializer dcs =
                        new DataContractSerializer(dataContract.GetType());
                    dcs.WriteObject(xdw, dataContract);
                }
            }
            return sb.ToString();
        }

        #endregion String

        #region XmlDocument

        /// <summary>
        /// Serializes the data contract to xml document.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to a xml document.</param>
        /// <returns>An xml document representation of the data contract.</returns>
        public static XmlDocument SerializeToXmlDocument<T>(T dataContract)
        {
            return SerializeToXmlDocument(dataContract);
        }

        /// <summary>
        /// Serializes the data contract to xml document.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to a xml document.</param>
        /// <returns>An xml document representation of the data contract.</returns>
        public static XmlDocument SerializeToXmlDocument(object dataContract)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(SerializeToString(dataContract));
            return doc;
        }

        #endregion XmlDocument

        #region Xml File

        /// <summary>
        /// Serializes the data contract to an xml file on the file system and returns a file pointer.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to an xml file.</param>
        /// <returns>An xml document representation of the data contract.</returns>
        public static XmlDocument SerializeToXmlFile(object dataContract, string filePath)
        {
            XmlDocument doc = SerializeToXmlDocument(dataContract);
            doc.Save(filePath);
            return doc;
        }

        #endregion Xml File

        #region Byte array

        /// <summary>
        /// Serializes the data contract to a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to a string..</param>
        /// <returns>A binary representation (i.e. byte array) of the data contract.</returns>
        public static byte[] SerializeToByteArray<T>(T dataContract)
        {
            return SerializeToByteArray(dataContract, typeof(T));
        }

        /// <summary>
        /// Serializes the data contract to a byte array.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to a string..</param>
        /// <returns>A binary representation (i.e. byte array) of the data contract.</returns>
        public static byte[] SerializeToByteArray(object dataContract, Type objectType)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateBinaryWriter(ms))
                {
                    DataContractSerializer dcs = new DataContractSerializer(objectType);
                    dcs.WriteObject(ms, dataContract);
                    return ms.GetBuffer();
                }
            }
        }

        #endregion Byte array

        #region Json

        /// <summary>
        /// Serializes the data contract to json.
        /// </summary>
        /// <typeparam name="T">The type of the data contract.</typeparam>
        /// <param name="dataContract">The data contract to be serialized to json.</param>
        /// <returns>A json representation (i.e. xml) of the data contract.</returns>
        public static string SerializeToJson(object dataContract)
        {
            // Serialize to a memory stream.
            MemoryStream stream1 = 
                new MemoryStream();
            DataContractJsonSerializer ser =
                new DataContractJsonSerializer(dataContract.GetType());
            ser.WriteObject(stream1, dataContract);

            // Now read & return the stream.
            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            return sr.ReadToEnd();
        }

        #endregion Json

        #endregion Serialization

        #endregion Public static methods

        #region Byte array

        /// <summary>
        /// Serializes the instance to a byte array.
        /// </summary>
        /// <param name="instance">The instance to be serialized.</param>
        /// <returns>A binary representation (i.e. byte array) of the instance.</returns>
        public static byte[] SerializeToByteArray1(object instance)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, instance);
                return ms.GetBuffer();
            }
        }

        #endregion Byte array
    }
}
