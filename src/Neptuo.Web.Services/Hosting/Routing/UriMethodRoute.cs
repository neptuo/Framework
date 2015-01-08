using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing
{
    /// <summary>
    /// Route for single url that must be matched exactly and on of acceptable Http methods.
    /// </summary>
    public class UriMethodRoute : UriRoute
    {
        /// <summary>
        /// Enumeration of acceptable Http methods for this route.
        /// </summary>
        public IEnumerable<HttpMethod> Methods { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="url"/> as target URL and <paramref name="methods" /> as acceptable Http methods.
        /// </summary>
        /// <param name="url">Target URL.</param>
        public UriMethodRoute(Uri url, params HttpMethod[] methods)
            : base(url)
        {
            Methods = methods;
        }

        /// <summary>
        /// Creates new instance with <paramref name="url"/> and <paramref name="uriKind"/> as acceptable Http methods.
        /// </summary>
        /// <param name="url">Url string.</param>
        /// <param name="uriKind">Kind of uri.</param>
        /// <exception cref="System.ArgumentException">When url is not 'uri'.</exception>
        /// <exception cref="System.ArgumentNullException">When url is not 'uri'.</exception>
        /// <exception cref="System.UriFormatException">When url is not 'uri'.</exception>
        public UriMethodRoute(string url, UriKind uriKind, params HttpMethod[] methods)
            : base(url, uriKind)
        {
            Methods = methods;
        }

        /// <summary>
        /// Returns <c>true</c> if <c>request.Url</c> is matched exactly with <see cref="UriRoute.Url"/> 
        /// and <c>request.Method</c> is in acceptable <see cref="UriMethodRoute.Methods"/>.
        /// </summary>
        /// <param name="request">Current Http request.</param>
        /// <returns>
        /// <c>true</c> if <c>request.Url</c> is matched exactly with <see cref="UriRoute.Url"/> 
        /// and <c>request.Method</c> is in acceptable <see cref="UriMethodRoute.Methods"/>; false otherwise.
        /// </returns>
        public override bool IsMatch(IHttpRequest request)
        {
            return base.IsMatch(request) && Methods.Contains(request.Method);
        }
    }
}
