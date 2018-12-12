using Neptuo.Observables.Collections.Features;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables.Commands
{
    /// <summary>
    /// A base class for command which can move with items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MoveCommand<T> : Command<T>, IDisposable
    {
        /// <summary>
        /// Gets a collection of items.
        /// </summary>
        protected IMoveCollection<T> Collection { get; }

        /// <summary>
        /// Creates a new instance.
        /// If <paramref name="sources"/> implements also <see cref="INotifyCollectionChanged"/>, than it binds <see cref="System.Windows.Input.ICommand.CanExecuteChanged"/> to its changes.
        /// </summary>
        /// <param name="sources">A collection to move items in.</param>
        protected MoveCommand(IMoveCollection<T> sources)
        {
            Ensure.NotNull(sources, "sources");
            this.Collection = sources;

            if (sources is INotifyCollectionChanged notifyCollection)
                notifyCollection.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            => RaiseCanExecuteChanged();

        public override void Execute(T item)
        {
            if (CanExecute(item))
            {
                int oldIndex = Collection.IndexOf(item);
                int? newIndex = FindNewIndex(item, oldIndex);
                if (newIndex != null)
                    Collection.Move(oldIndex, newIndex.Value);
            }
        }

        /// <summary>
        /// Tries to find a new index for the <paramref name="item"/>.
        /// If <c>null</c> is returned, nothing is moved.
        /// </summary>
        /// <param name="item">An item to find index for.</param>
        /// <param name="itemIndex">A current item index in the <see cref="Collection"/>.</param>
        /// <returns>A new index for the <paramref name="item"/> or <c>null</c>.</returns>
        protected abstract int? FindNewIndex(T item, int itemIndex);

        public new void RaiseCanExecuteChanged()
            => base.RaiseCanExecuteChanged();

        public void Dispose()
        {
            if (Collection is INotifyCollectionChanged notifyCollection)
                notifyCollection.CollectionChanged -= OnCollectionChanged;
        }
    }
}
