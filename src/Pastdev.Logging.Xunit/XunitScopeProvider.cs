namespace Pastdev.Logging.Xunit
{
    using System;
    using Microsoft.Extensions.Logging;

    /// <summary>Implements <see cref="IExternalScopeProvider"/> using a simple
    /// linked list style stack.</summary>
    internal class XunitScopeProvider : IExternalScopeProvider
    {
        private Scope Current { get; set; }

        /// <inheritdoc/>
        public void ForEachScope<TState>(Action<object, TState> callback, TState state)
            => ForEachScope(this.Current, callback, state);

        private static void ForEachScope<TState>(
            Scope current,
            Action<object, TState> callback,
            TState state)
        {
            if (current == null)
            {
                return;
            }

            ForEachScope(current.Parent, callback, state);
            callback(current, state);
        }

        /// <inheritdoc/>
        public IDisposable Push(object state)
        {
            this.Current = new Scope
            {
                Provider = this,
                State = state,
                Parent = this.Current,
            };

            return this.Current;
        }

        private class Scope : IDisposable
        {
            internal object State { get; set; }

            internal XunitScopeProvider Provider { get; set; }

            internal Scope Parent { get; set; }

            /// <inheritdoc/>
            public void Dispose()
            {
                this.Dispose(true);
                GC.SuppressFinalize(this);
            }

            /// <summary>Removes this scope from the provider.</summary>
            /// <param name="disposing">True if called from Dispose.</param>
            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this.Provider.Current = this.Parent;
                }
            }
        }
    }
}
