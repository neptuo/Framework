using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services
{
    /// <summary>
    /// Handler for Http PUT requests.
    /// </summary>
    public interface IPut
    {
        /// <summary>
        /// Invoked on Http PUT request.
        /// </summary>
        void Execute();
    }
}
