namespace Pastdev.Logging.Xunit
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>A logger for Xunit tests with a writer whose output mechanism
    /// can be replaced.</summary>
    internal class XunitLogger : ILogger
    {
        private IXunitMessageWriter writer;
        private string name;
        private IExternalScopeProvider scopeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="XunitLogger"/> class.
        /// </summary>
        /// <param name="name">The logger name.</param>
        /// <param name="writer">The message output writer.</param>
        /// <param name="scopeProvider">Provides scope for log messages.</param>
        internal XunitLogger(
            string name,
            IXunitMessageWriter writer,
            IExternalScopeProvider scopeProvider)
        {
            this.name = name;
            this.writer = writer;
            this.scopeProvider = scopeProvider;
        }

        /// <inheritdoc/>
        public IDisposable BeginScope<TState>(TState state)
            => this.scopeProvider.Push(state);

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
        {
            // This is the same as ConsoleLogger... Not sure why ConsoleLogger
            // is implemented this way.
            return logLevel != LogLevel.None;
        }

        /// <inheritdoc/>
        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            this.writer.WriteMessage(
                logLevel,
                eventId,
                state,
                exception,
                this.name,
                formatter(state, exception));
        }
    }
}
