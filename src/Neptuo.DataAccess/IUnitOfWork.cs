using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess
{
    /// <summary>
    /// Unit of work abstraction.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves changes.
        /// </summary>
        void Commit();
    }
}
