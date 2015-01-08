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
        /// <summary>
        /// Gets current unit of work, if non was created, returns null.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Starts new unit of work.
        /// </summary>
        IUnitOfWork StartUnitOfWork();
    }
}
