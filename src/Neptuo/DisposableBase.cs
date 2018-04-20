using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Base class for implementing <see cref="IDisposable"/>.
    /// Provides flag to see if object is already disposed.
    /// Once object is disposed, calling <see cref="IDisposable.Dispose"/> has no effect.
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        /// <summary>
        /// Gets a <c>true</c> if object is already disposed; <c>false</c> otherwise.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        protected DisposableBase()
        { }

        /// <summary>
        /// Ensures this object is not disposed.
        /// If <see cref="IsDisposed"/> is <c>true</c>, throws <see cref="ObjectDisposedException"/>.
        /// </summary>
        protected void ThrowWhenDisposed()
        {
            Ensure.NotDisposed(this, "this");
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;
            else
                IsDisposed = true;

            DisposeManagedResources();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        { }
    }
}
