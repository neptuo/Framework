using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Neptuo.Observables.Collections.Features;
using Neptuo;

namespace Neptuo.Observables.Collections
{
    /// <summary>
    /// An extended <see cref="System.Collections.ObjectModel.ObservableCollection{T}"/> which raises <see cref="INotifyPropertyChanged.PropertyChanged"/>
    /// for <see cref="ICollection{T}.Count"/> changes.
    /// Contains <see cref="AddRange(IEnumerable{T})"/> for adding mutli items.
    /// Also implements feature interfaces <see cref="IRemoveAtCollection"/> and <see cref="ICountCollection"/>.
    /// It implements <see cref="IMoveCollection{T}"/> to support ordering items using standard commands.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>, IRemoveAtCollection, ICountCollection, IMoveCollection<T>
    {
        /// <summary>
        /// Creates new empty instance.
        /// </summary>
        public ObservableCollection()
        { }

        /// <summary>
        /// Creates new instance from <paramref name="items"/>.
        /// </summary>
        /// <param name="items">An enumeration of initial collection items.</param>
        public ObservableCollection(IEnumerable<T> items)
        {
            AddRange(items);
        }

        /// <summary>
        /// Adds <paramref name="items"/> to the collection.
        /// </summary>
        /// <param name="items">An enumeration of items to add.</param>
        public void AddRange(IEnumerable<T> items)
        {
            Ensure.NotNull(items, "items");
            foreach (T item in items)
                Add(item);
        }

        /// <summary>
        /// Adds <paramref name="items"/> to the collection.
        /// </summary>
        /// <param name="items">An array of items to add.</param>
        public void AddRange(params T[] items)
        {
            Ensure.NotNull(items, "items");
            foreach (T item in items)
                Add(item);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
        }
    }
}
