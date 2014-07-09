using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing
{
    /// <summary>
    /// Single item in route table.
    /// Matches URL.
    /// </summary>
    public interface IRoute
    {
        /// <summary>
        /// Returns <c>true</c> if <paramref name="request"/> is matched be current route; false otherwise.
        /// </summary>
        /// <param name="request">Current Http request.</param>
        /// <returns><c>true</c> if <paramref name="request"/> is matched be current route; false otherwise.</returns>
        bool IsMatch(IHttpRequest request);
    }
}
