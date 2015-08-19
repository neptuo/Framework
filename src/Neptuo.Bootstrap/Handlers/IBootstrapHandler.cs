using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Handlers
{
    /// <summary>
    /// Single bootstrap step handler.
    /// </summary>
    public interface IBootstrapHandler
    {
        /// <summary>
        /// Called to process intialization.
        /// </summary>
        void Handle();
    }
}
