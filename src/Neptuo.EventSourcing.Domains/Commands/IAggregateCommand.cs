using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// A command which is associate with the aggregate.
    /// </summary>
    public interface IAggregateCommand : ICommand
    {
        /// <summary>
        /// Gets a key of the aggregate.
        /// </summary>
        IKey AggregateKey { get; }
    }
}
