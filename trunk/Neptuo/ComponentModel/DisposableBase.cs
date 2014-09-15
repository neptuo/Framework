using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Base class for implementing <see cref="IDisposable"/>.
    /// Provides posibility to distinguish between disposiing managed and unmanaged resources.
    /// Provides flag to see if object is already disposed.
    /// Once object is dispose, calling <see cref="IDisposable.Disponse"/> has no effect.
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        public bool IsDisposed { get; private set; }

        ~DisposableBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
                DisposeManagedResources();

            DisposeUnmanagedResources();
            IsDisposed = true;
        }

        /// <summary>
        /// Disposes the managed resources.
        /// </summary>
        protected virtual void DisposeManagedResources()
        { }

        /// <summary>
        /// Disposes the unmanaged resources.
        /// </summary>
        protected virtual void DisposeUnmanagedResources()
        { }

    }
}
