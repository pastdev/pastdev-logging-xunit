namespace Pastdev.Logging.Xunit
{
    using System;
    using System.Globalization;
    using global::Xunit.Abstractions;
    using Microsoft.Extensions.Logging;

    /// <summary>The default implementation of IXunitMessageWriter.  Formats
    /// incoming messages to include a timestamp, loglevel, and logger name
    /// prefix for the message.</summary>
    public class XunitMessageWriter : IXunitMessageWriter
    {
        /// <inheritdoc/>
        public ITestOutputHelper OutputHelper { get; set; }

        private string Level(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.None: return "NONE";
                case LogLevel.Trace: return "TRACE";
                case LogLevel.Debug: return "DEBUG";
                case LogLevel.Information: return "INFO";
                case LogLevel.Warning: return "WARN";
                case LogLevel.Error: return "ERROR";
                case LogLevel.Critical: return "CRIT";
                default: return "UNKN";
            }
        }

        /// <inheritdoc/>
        public void WriteMessage<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            string name,
            string message)
        {
            this.OutputHelper?.WriteLine(
                "{0} {1} {2}: {3}",
                DateTime.Now.ToString("hh:mm:ss.fff", CultureInfo.InvariantCulture),
                this.Level(logLevel),
                name,
                message);
            if (exception != null)
            {
                this.OutputHelper?.WriteLine(exception.ToString());
            }
        }
    }
}
