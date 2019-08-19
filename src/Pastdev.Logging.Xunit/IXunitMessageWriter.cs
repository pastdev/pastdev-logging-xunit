namespace Pastdev.Logging.Xunit
{
    using System;
    using global::Xunit.Abstractions;
    using Microsoft.Extensions.Logging;

    /// <summary>The output writer for logging while using Xunit.</summary>
    public interface IXunitMessageWriter
    {
        /// <summary/><value>The output helper to write to. Should be replaced
        /// at the beginning of each test constructure with the injected test
        /// output helper.</value>
        ITestOutputHelper OutputHelper { get; set; }

        /// <summary>Writes a message to the OutputHelper.</summary>
        /// <typeparam name="TState">The type of the state object.</typeparam>
        /// <param name="logLevel">The level to log this message at.</param>
        /// <param name="eventId">The id of this event.</param>
        /// <param name="state">The current state object.</param>
        /// <param name="exception">The exception to log (may be null).</param>
        /// <param name="name">The name of the logger.</param>
        /// <param name="message">The message text.</param>
        void WriteMessage<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            string name,
            string message);
    }
}
