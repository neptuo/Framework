using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables.Collections.Features
{
    /// <summary>
    /// A collection which is able to move with items.
    /// </summary>
    /// <typeparam name="T">A type of the item.</typeparam>
    public interface IMoveCollection<T> : ICountCollection
    {
        /// <summary>
        /// Gets a current index of <paramref name="item"/>; If it's not contained in the collection, returns <c>-1</c>.
        /// </summary>
        /// <param name="item">An item to find the index of.</param>
        /// <returns>An index of the <paramref name="item"/>.</returns>
        int IndexOf(T item);

        /// <summary>
        /// Moves item at an <paramref name="oldIndex"/> to a <paramref name="newIndex"/>.
        /// </summary>
        /// <param name="oldIndex">A current index of the item to move.</param>
        /// <param name="newIndex">A new index to move the item to.</param>
        void Move(int oldIndex, int newIndex);
    }
}
