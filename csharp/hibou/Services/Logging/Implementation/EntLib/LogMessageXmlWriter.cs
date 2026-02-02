using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Keane.CH.Framework.Core.Utilities.Xml;
using Keane.CH.Framework.Services.Logging.Contracts.Service;
using Keane.CH.Framework.Services.Logging.Implementation.EntLib.Resources;
using Keane.CH.Framework.Services.Logging.Contracts.Data;
using Keane.CH.Framework.Core.Utilities.DataContract;

namespace Keane.CH.Framework.Services.Logging.Implementation.EntLib
{
    /// <summary>
    /// Encapsualtes writing log messages in xml format.
    /// </summary>
    public class LogMessageXmlWriter
    {
        /// <summary>
        /// Serializes the passed type to an xml string.
        /// </summary>
        /// <param name="instance">The instance being serialized to xml.</param>
        /// <returns>An xml representation of the instance.</returns>
        public static string AsXml(LogMessage instance)
        {
            // Defensive programming.
            if (instance == null)
                throw new ArgumentNullException("instance"); ;

            // Return with a 0 tab offset.
            return AsXml(instance, 0);
        }

        /// <summary>
        /// Serializes the passed type to an xml string.
        /// </summary>
        /// <param name="instance">The instance being serialized to xml.</param>
        /// <param name="tabOffSet">The number of tabs to prepend to each line.</param>
        /// <returns>An xml representation of the instance.</returns>
        public static string AsXml(
            LogMessage instance,
            uint tabOffSet)
        {
            // Defensive programming.
            if (instance == null)
                throw new ArgumentNullException("instance"); ;

            // Begin.
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet, XmlElementConstants.LogMessage));

            // Message.
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.LogMessageMessage, instance.Message));
            AsXml("MessageData", instance.MessageData, tabOffSet + 1, sb);

            // Message Context.
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet + 1, XmlElementConstants.LogMessageMessageContext));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.LogMessageId, instance.Id));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.LogMessageTimeStamp, instance.TimeStamp));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.LogMessagePriority, instance.Priority));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.LogMessageEventType, instance.EventType));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.LogMessageCategory, instance.WriterType));
            sb.AppendLine(XmlWriterUtility.WriteEndElement(tabOffSet + 1, XmlElementConstants.LogMessageMessageContext));

            // Execution context.
            if (instance.Verbosity == LogMessageVerbosityType.High)
                AsXml(instance.ExecutionContext, tabOffSet + 1, sb);

            // Process context.
            if (instance.Verbosity != LogMessageVerbosityType.Low)
                AsXml(instance.ProcessContext, tabOffSet + 1, sb);

            // Fault information.
            AsXml("Fault", instance.FaultInfo, tabOffSet + 1, sb);

            // Close & return.
            sb.Append(XmlWriterUtility.WriteEndElement(tabOffSet, XmlElementConstants.LogMessage));
            return sb.ToString();
        }

        /// <summary>
        /// Serializes the passed type to an xml string.
        /// </summary>
        /// <param name="instance">The instance being serialized to xml.</param>
        /// <param name="tabOffSet">The number of tabs to prepend to each line.</param>
        /// <param name="sb">The string builder being used to construct the xml.</param>
        private static void AsXml(
            ExecutionContextInfo instance,
            uint tabOffSet,
            StringBuilder sb)
        {
            // Defensive programming.
            if (instance == null)
                return;
            
            // Execution context.
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet, XmlElementConstants.ExecutionContext));            
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ExecutionContextSystemLayer, instance.SystemLayer));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ExecutionContextAssembly, instance.Assembly));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ExecutionContextTypeName, instance.TypeName));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ExecutionContextMethodName, instance.Method));           
            if (!string.IsNullOrEmpty(instance.MethodResult))
                sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ExecutionContextMethodResult, instance.MethodResult));
            AsXml(XmlElementConstants.ExecutionContextMethodParameters, instance.MethodParameterList, tabOffSet + 1, sb);
            sb.AppendLine(XmlWriterUtility.WriteEndElement(tabOffSet, XmlElementConstants.ExecutionContext));            
        }

        /// <summary>
        /// Serializes the passed type to an xml string.
        /// </summary>
        /// <param name="instance">The instance being serialized to xml.</param>
        /// <param name="tabOffSet">The number of tabs to prepend to each line.</param>
        /// <param name="sb">The string builder being used to construct the xml.</param>
        private static void AsXml(
            string parentElementName,
            FaultInfo instance,
            uint tabOffSet,
            StringBuilder sb)
        {
            // Defensive programming.
            if (instance == null)
                return;

            // Execution context.
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet, parentElementName));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.FaultMessage, instance.Message));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.FaultSource, instance.Source));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.FaultStackTrace, instance.StackTrace));
            if (instance.InnerFault != null)
                AsXml(@"InnerFault", instance.InnerFault, tabOffSet + 1, sb);
            sb.AppendLine(XmlWriterUtility.WriteEndElement(tabOffSet, parentElementName));
        }

        /// <summary>
        /// Serializes the passed type to an xml string.
        /// </summary>
        /// <param name="instance">The instance being serialized to xml.</param>
        /// <param name="tabOffSet">The number of tabs to prepend to each line.</param>
        /// <param name="sb">The string builder being used to construct the xml.</param>
        private static void AsXml(
            ProcessContextInfo instance,
            uint tabOffSet,
            StringBuilder sb)
        {
            // Defensive programming.
            if (instance == null)
                return;

            // Process context.
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet, XmlElementConstants.ProcessContext));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ProcessContextMachineName, instance.MachineName));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ProcessContextMachineIp, instance.MachineIpAddress));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ProcessContextMachineOSVersion, instance.MachineOSVersion));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ProcessContextUserName, instance.UserName));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ProcessContextUserDomainName, instance.UserDomainName));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ProcessContextThreadId, instance.ThreadId));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, XmlElementConstants.ProcessContextThreadName, instance.ThreadName));
            sb.AppendLine(XmlWriterUtility.WriteEndElement(tabOffSet, XmlElementConstants.ProcessContext));
        }

        /// <summary>
        /// Serializes the passed type to an xml string.
        /// </summary>
        /// <param name="instance">The instance being serialized to xml.</param>
        /// <param name="tabOffSet">The number of tabs to prepend to each line.</param>
        /// <param name="sb">The string builder being used to construct the xml.</param>
        private static void AsXml(
            Exception instance,
            uint tabOffSet,
            StringBuilder sb)
        {
            // Defensive programming.
            if (instance == null)
                return;
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet, XmlElementConstants.ExecutionContextMethodResult));
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet + 1, XmlElementConstants.Fault));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.FaultMessage, instance.Message));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.FaultSource, instance.Source));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.FaultStackTrace, instance.StackTrace));
            sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 2, XmlElementConstants.FaultTargetSite, instance.TargetSite));
            sb.AppendLine(XmlWriterUtility.WriteEndElement(tabOffSet + 1, XmlElementConstants.Fault));
            sb.AppendLine(XmlWriterUtility.WriteEndElement(tabOffSet, XmlElementConstants.ExecutionContextMethodResult));
        }

        /// <summary>
        /// Serializes the passed type to an xml string.
        /// </summary>
        /// <param name="parentElementName">The name of the parent element.</param>
        /// <param name="instance">The instance being formatted as xml.</param>
        /// <param name="tabOffSet">The number of tabs to prepend to each line.</param>
        /// <param name="sb">The string builder being used to construct the xml.</param>
        private static void AsXml(
            string parentElementName,
            List<NameValuePair<string>> instance,
            uint tabOffSet,
            StringBuilder sb)
        {
            // Defensive programming.
            if (instance == null)
                return;
            if (instance.Count == 0)
                return;

            // For each key value pair output the relevant element.
            sb.AppendLine(XmlWriterUtility.WriteStartElement(tabOffSet, parentElementName));            
            foreach (NameValuePair<string> nvp in instance)
            {
                if ((!String.IsNullOrEmpty(nvp.Name.Trim())) &
                    (!String.IsNullOrEmpty(nvp.Value.Trim())))
                {
                    sb.AppendLine(XmlWriterUtility.WriteElement(tabOffSet + 1, nvp.Name.Trim(), nvp.Value.ToString()));
                }
            }
            sb.AppendLine(XmlWriterUtility.WriteEndElement(tabOffSet, parentElementName));
        }
    }
}
