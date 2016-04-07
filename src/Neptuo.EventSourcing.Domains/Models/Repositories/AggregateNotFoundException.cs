using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Repositories
{
    /// <summary>
    /// Exception raised when to such aggregate root exists,
    /// </summary>
    public class AggregateNotFoundException : Exception
    {
        /// <summary>
        /// The aggregate root key which is not found in the repository.
        /// </summary>
        public IKey AggregateKey { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="aggregateKey">The aggregate root key which is not found in the repository.</param>
        public AggregateNotFoundException(IKey aggregateKey)
        {
            AggregateKey = aggregateKey;
        }
    }
}
