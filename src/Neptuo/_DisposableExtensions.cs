using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// A common extensions for <see cref="IDisposable"/>.
    /// </summary>
    public static class _DisposableExtensions
    {
        /// <summary>
        /// Tries to cast <paramref name="disposable"/> to <see cref="DisposableBase"/> to read <see cref="DisposableBase.IsDisposed"/>.
        /// </summary>
        /// <param name="disposable">A disposable object.</param>
        /// <returns><c>true</c> if <paramref name="disposable"/> is disposed; <c>false</c> otherwise.</returns>
        public static bool IsDisposed(this IDisposable disposable)
        {
            DisposableBase other = disposable as DisposableBase;
            if (other == null)
                return false;

            return other.IsDisposed;
        }
    }
}
