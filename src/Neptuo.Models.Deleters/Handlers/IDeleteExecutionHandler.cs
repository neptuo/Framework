using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Deleters.Handlers
{
    /// <summary>
    /// Executes deletion.
    /// </summary>
    public interface IDeleteExecutionHandler
    {
        /// <summary>
        /// Executes delete operation.
        /// This method should be called <see cref="IDeleteContext"/> has set <see cref="IDeleteContext.CanDelete"/> to <c>true</c>.
        /// </summary>
        /// <param name="key">Key of the object to delete.</param>
        void Handle(IKey key);
    }
}
