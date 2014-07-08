using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services
{
    /// <summary>
    /// Handler for Http POST requests.
    /// </summary>
    public interface IPost
    {
        /// <summary>
        /// Invoked on Http POST request.
        /// </summary>
        void Post();
    }
}
