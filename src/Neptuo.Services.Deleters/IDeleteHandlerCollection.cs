using Neptuo.Services.Deleters.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Deleters
{
    /// <summary>
    /// Collection of registered delete handlers.
    /// </summary>
    public interface IDeleteHandlerCollection
    {
        /// <summary>
        /// Registers <paramref name="handler"/> to be executed for keys with type <paramref name="objectType"/>.
        /// </summary>
        /// <param name="objectType">Type of object to be handled by <paramref name="handler"/>.</param>
        /// <param name="handler">Handler to be executed for keys with type <paramref name="objectType"/>.</param>
        /// <returns>Self (for fluency).</returns>
        IDeleteHandlerCollection Add(string objectType, IDeleteHandler handler);
    }
}
