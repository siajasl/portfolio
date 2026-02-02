using System.Diagnostics;
using Keane.CH.Framework.Services.Logging.Contracts.Data;
using System.Collections.Generic;
using System;

namespace Keane.CH.Framework.Services.Logging.Implementation.EntLib
{
    /// <summary>
    /// Acts as a factory for log message writers.
    /// </summary>
    /// <created by="Mark Morgan" date="01-Jul-2008" />
    internal static class LogMessageWriterFactory
    {
        #region Properties

        /// <summary>
        /// Gets or sets a dictionary of log message writers.
        /// </summary>
        private static Dictionary<LogMessageWriterType, LogMessageWriter> MessageWriters
        { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a log message writer.
        /// </summary>
        /// <param name="writerType">The type of writer to return.</param>
        /// <returns>A log message writer.</returns>
        internal static LogMessageWriter GetWriter(LogMessageWriterType writerType)
        {
            // JIT instantiation.
            if (MessageWriters == null)
                MessageWriters = new Dictionary<LogMessageWriterType, LogMessageWriter>();

            // Get from cache.
            LogMessageWriter writer;
            MessageWriters.TryGetValue(writerType, out writer);

            // If necessary instantiate & cache.
            if (writer == null)
            {
                writer = new LogMessageWriter();
                Configure(writer, writerType);
                MessageWriters.Add(writerType, writer);
            }

            // Return.
            return writer;
        }

        /// <summary>
        /// Configures a writer.
        /// </summary>
        /// <param name="writer">The writer being configured.</param>
        /// <param name="writerType">The type of writer being configured.</param>
        private static void Configure(
            LogMessageWriter writer, 
            LogMessageWriterType writerType)
        {
            // Derive name from writer type as string.
            writer.Name = writerType.ToString().Trim();
            writer.Name += @" Writer";

            // Derive id from writer type as int * offset.
            int defaultEventId = (int)writerType;
            defaultEventId *= 100000;
            writer.DefaultEventId = defaultEventId;
        }

        #endregion Methods
    }
}
