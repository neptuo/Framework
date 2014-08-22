using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services
{
    /// <summary>
    /// Handler for Http GET requests.
    /// </summary>
    public interface IGet
    {
        /// <summary>
        /// Invoked on Http GET request.
        /// </summary>
        void Execute();
    }
}
