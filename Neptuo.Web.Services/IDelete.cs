using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services
{
    /// <summary>
    /// Handler for Http DELETE requests.
    /// </summary>
    public interface IDelete
    {
        /// <summary>
        /// Invoked on Http DELETE request.
        /// </summary>
        void Execute();
    }
}
