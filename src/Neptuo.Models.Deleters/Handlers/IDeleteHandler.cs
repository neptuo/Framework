using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Deleters.Handlers
{
    /// <summary>
    /// Prepares <see cref="IDeleteContext"/>.
    /// </summary>
    public interface IDeleteHandler
    {
        /// <summary>
        /// Prepares <see cref="IDeleteContext"/> for <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A key of the object to delete.</param>
        /// <returns>A continuation task contaning a deletion context.</returns>
        Task<IDeleteContext> HandleAsync(IKey key);
    }
}
