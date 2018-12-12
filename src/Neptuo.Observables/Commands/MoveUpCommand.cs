using Neptuo.Observables.Collections.Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables.Commands
{
    /// <summary>
    /// A command which can move items from <see cref="IMoveCollection{T}"/> up.
    /// </summary>
    /// <typeparam name="T">A type of the item.</typeparam>
    public class MoveUpCommand<T> : MoveCommand<T>
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="collection">A collection to move items in.</param>
        public MoveUpCommand(IMoveCollection<T> collection)
            : base(collection)
        { }

        public override bool CanExecute(T item)
            => !Equals(item, default(T)) && Collection.IndexOf(item) < Collection.Count - 1;

        protected override int? FindNewIndex(T item, int itemIndex)
            => itemIndex + 1;
    }
}
