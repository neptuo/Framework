using Neptuo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    /// <summary>
    /// A collection of disposable objects.
    /// </summary>
    public class DisposableCollection : ICollection<IDisposable>, IDisposable
    {
        private readonly List<IDisposable> storage = new List<IDisposable>();

        /// <summary>
        /// Adds <paramref name="item"/> to a list of objects to dispose.
        /// </summary>
        /// <param name="item">An item to add.</param>
        /// <returns>Self (for fluency).</returns>
        public DisposableCollection Add(IDisposable item)
        {
            Ensure.NotNull(item, "item");
            storage.Add(item);
            return this;
        }

        /// <summary>
        /// Disposes all registered objects and removes them from the collection.
        /// The collection can be than reused.
        /// </summary>
        public void Dispose()
        {
            foreach (IDisposable disposable in storage)
                disposable.Dispose();

            Clear();
        }

        #region ICollection<IDisposable>

        public int Count => storage.Count;

        public bool IsReadOnly => false;

        void ICollection<IDisposable>.Add(IDisposable item)
            => Add(item);

        public void Clear()
            => storage.Clear();

        public bool Contains(IDisposable item)
            => storage.Contains(item);

        public void CopyTo(IDisposable[] array, int arrayIndex)
            => storage.CopyTo(array, arrayIndex);

        public bool Remove(IDisposable item)
            => storage.Remove(item);

        public IEnumerator<IDisposable> GetEnumerator()
            => storage.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        #endregion
    }
}
