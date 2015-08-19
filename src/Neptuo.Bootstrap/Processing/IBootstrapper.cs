using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap
{
    /// <summary>
    /// Application initializer facade.
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// Processes initialization.
        /// </summary>
        void Initialize();
    }
}
