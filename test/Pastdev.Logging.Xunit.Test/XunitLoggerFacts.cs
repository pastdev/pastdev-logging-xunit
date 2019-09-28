namespace Pastdev.Logging.Xunit.Test
{
    using System;
    using global::Xunit;
    using global::Xunit.Abstractions;
    using Microsoft.Extensions.Logging;

    public sealed class XunitLoggerFacts
    {
        public sealed class Log
        {
            public class WriteToLogInDisposeFixture : IDisposable
            {
                private XunitLoggerProvider provider;
                private ILogger logger;

                public WriteToLogInDisposeFixture()
                {
                    this.provider = new XunitLoggerProvider();
                    this.logger = this.provider.CreateLogger("WriteToLogInDisposeFixture");
                }

                /// <summary/><value>The Xunit test output helper.</value>
                public ITestOutputHelper OutputHelper
                {
                    get
                    {
                        return this.provider.OutputHelper;
                    }

                    set
                    {
                        this.provider.OutputHelper = value;
                    }
                }

                public void Dispose()
                {
                    this.Dispose(true);
                    GC.SuppressFinalize(this);
                }

                protected virtual void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        this.logger.LogInformation("this should not cause an error");
                        this.provider.Dispose();
                    }
                }
            }

            public class WriteToLogInDispose : IClassFixture<WriteToLogInDisposeFixture>
            {
                public WriteToLogInDispose(
                    WriteToLogInDisposeFixture fixture,
                    ITestOutputHelper outputHelper)
                {
                    if (fixture == null)
                    {
                        throw new ArgumentNullException(nameof(fixture));
                    }

                    fixture.OutputHelper = outputHelper;
                }

                [Fact]
                public void ShouldNotFailIfNoActiveTest()
                {
                    // fixture shutdown occurs after all tests have completed.
                    // this results in something like:
                    //   [Test Class Cleanup Failure (Pastdev.Logging.Xunit.Test.XunitLoggerFacts+Log+WriteToLogInDispose)]: System.InvalidOperationException : There is no currently active test.
                    // if you try to write to the output helper using the idiom
                    // outlined by this test case.  going to write code to prevent
                    // this now...
                }
            }
        }
    }
}
