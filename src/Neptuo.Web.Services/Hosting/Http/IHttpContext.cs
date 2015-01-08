using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Describes whole Http context.
    /// </summary>
    public interface IHttpContext
    {
        /// <summary>
        /// Http request.
        /// </summary>
        IHttpRequest Request { get; }

        /// <summary>
        /// Http response.
        /// </summary>
        IHttpResponse Response { get; }

        /// <summary>
        /// Collection of custom values.
        /// </summary>
        IDictionary<string, string> Values { get; }

        /// <summary>
        /// Resolves url starting with ~/...
        /// </summary>
        /// <param name="appRelativeUrl">Application relative url.</param>
        /// <returns>Absolute url.</returns>
        string ResolveUrl(string appRelativeUrl);
    }
}
