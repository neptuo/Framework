using Neptuo.Observables.Collections.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables.Commands
{
    /// <summary>
    /// A command which can move items from <see cref="IMoveCollection{T}"/> down.
    /// </summary>
    /// <typeparam name="T">A type of the item.</typeparam>
    public class MoveDownCommand<T> : MoveCommand<T>
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="collection">A collection to move items in.</param>
        public MoveDownCommand(IMoveCollection<T> collection)
            : base(collection)
        { }

        public override bool CanExecute(T item)
            => !Equals(item, default(T)) && Collection.IndexOf(item) > 0;

        protected override int? FindNewIndex(T item, int itemIndex)
            => itemIndex - 1;
    }
}
