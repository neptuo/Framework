using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// The base implementation of <see cref="ICommand"/>.
    /// </summary>
    public abstract class Command : ICommand
    {
        public IKey Key { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        protected Command()
        {
            Key = GuidKey.Create(Guid.NewGuid(), GetType().Name);
        }
    }
}
