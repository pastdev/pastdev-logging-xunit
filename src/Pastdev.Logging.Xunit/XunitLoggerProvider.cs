namespace Pastdev.Logging.Xunit
{
    using System;
    using System.Collections.Concurrent;
    using global::Xunit.Abstractions;
    using Microsoft.Extensions.Logging;

    /// <summary>XunitLoggerProvider using XunitMessageWriter to write the log
    /// messages.</summary>
    public class XunitLoggerProvider : XunitLoggerProvider<XunitMessageWriter>
    {
    }

    /// <summary>A logger provider implementation that works with Xunit by
    /// allowing the output mechanism to be replaced after loggers have been
    /// created.  This is necessary because you need to write to an
    /// <see cref="ITestOutputHelper"/> when using Xunit and you are only
    /// provided an instance during test class construction time.  To use this
    /// provider you must maintain an reference to the instance of this provider
    /// that was used to create the loggers, then obtain a refernce to the
    /// ITestOutputHelper for your test class, and set it to the
    /// <see cref="OutputHelper"/> property of this class.
    /// </summary>
    /// <typeparam name="TWriter">The implemntation class of the message writer.</typeparam>
    public class XunitLoggerProvider<TWriter>
        : ILoggerProvider
        where TWriter : IXunitMessageWriter, new()
    {
        private IXunitMessageWriter writer;
        private readonly ConcurrentDictionary<string, XunitLogger> loggers
            = new ConcurrentDictionary<string, XunitLogger>();

        /// <summary>
        /// Initializes a new instance of the <see cref="XunitLoggerProvider{TWriter}"/> class.
        /// </summary>
        public XunitLoggerProvider()
        {
            this.writer = new TWriter();
        }

        /// <summary/><value>The current output helper to write messages to.  If null,
        /// messages will be ignored.</value>
        public ITestOutputHelper OutputHelper
        {
            get
            {
                return this.writer.OutputHelper;
            }

            set
            {
                this.writer.OutputHelper = value;
            }
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
            => this.loggers.GetOrAdd(
                categoryName,
                loggerName => new XunitLogger(
                    loggerName,
                    this.writer,
                    new XunitScopeProvider()));

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>Releases resources.</summary>
        /// <param name="disposing">True if called by Dispose.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // nothing to do currently...
            }
        }
    }
}
