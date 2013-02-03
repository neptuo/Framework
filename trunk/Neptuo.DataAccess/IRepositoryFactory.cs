using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DataAccess
{
    /// <summary>
    /// Factory for accessing repositories and unit of work.
    /// </summary>
    public interface IRepositoryFactory
    {
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Starts new unit of work.
        /// </summary>
        IUnitOfWork StartUnitOfWork();

        /// <summary>
        /// Creates mapped repository for <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Repository contract.</typeparam>
        /// <returns>Mapped repository for <typeparamref name="T"/>.</returns>
        T Create<T>();
    }
}
