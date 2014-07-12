using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing
{
    /// <summary>
    /// Route for single url that must be matched exactly.
    /// </summary>
    public class UriRoute : IRoute
    {
        /// <summary>
        /// Target URL.
        /// </summary>
        public Uri Url { get; private set; }

        /// <summary>
        /// Creates new instance with <paramref name="url"/> as target URL.
        /// </summary>
        /// <param name="url">Target URL.</param>
        public UriRoute(Uri url)
        {
            Guard.NotNull(url, "url");
            Url = url;
        }

        /// <summary>
        /// Creates new instance with <paramref name="url"/> and <paramref name="uriKind"/>.
        /// </summary>
        /// <param name="url">Url string.</param>
        /// <param name="uriKind">Kind of uri.</param>
        /// <exception cref="System.ArgumentException">When url is not 'uri'.</exception>
        /// <exception cref="System.ArgumentNullException">When url is not 'uri'.</exception>
        /// <exception cref="System.UriFormatException">When url is not 'uri'.</exception>
        public UriRoute(string url, UriKind uriKind)
            : this(new Uri(url, uriKind))
        { }

        /// <summary>
        /// Returns <c>true</c> if <c>request.Url</c> is matched exactly with <see cref="UriRoute.Url"/>.
        /// </summary>
        /// <param name="request">Current Http request.</param>
        /// <returns><c>true</c> if <c>request.Url</c> is matched exactly with <see cref="UriRoute.Url"/>; false otherwise.</returns>
        public virtual bool IsMatch(IHttpRequest request)
        {
            return request.Url == Url;
        }
    }
}
