using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Wraps <see cref="HttpContext"/>.
    /// </summary>
    public class AspNetHttpContext : IHttpContext
    {
        /// <summary>
        /// Original http context.
        /// </summary>
        private readonly HttpContext httpContext;

        /// <summary>
        /// Collection of all supported media types.
        /// </summary>
        private readonly IMediaTypeCollection mediaTypes;


        /// <summary>
        /// Cached wrapper for <see cref="HttpRequest"/>.
        /// </summary>
        private IHttpRequest request;

        /// <summary>
        /// Cached wrapper for <see cref="HttpResponse"/>.
        /// </summary>
        private IHttpResponse response;

        /// <summary>
        /// Inner collection for <see cref="IHttpContext.Values"/>.
        /// </summary>
        private IDictionary<string, string> values;

        public IHttpRequest Request
        {
            get
            {
                if (request == null)
                    request = new AspNetHttpRequest(httpContext.Request, mediaTypes);

                return request;
            }
        }

        public IHttpResponse Response
        {
            get
            {
                if (response == null)
                    response = new AspNetHttpResponse(httpContext.Response, Request, mediaTypes);

                return response;
            }
        }

        public IDictionary<string, string> Values
        {
            get
            {
                if (values == null)
                    values = new Dictionary<string, string>();

                return values;
            }
        }

        public AspNetHttpContext(HttpContext httpContext, IMediaTypeCollection mediaTypes)
        {
            Guard.NotNull(httpContext, "httpContext");
            Guard.NotNull(mediaTypes, "mediaTypes");
            this.httpContext = httpContext;
            this.mediaTypes = mediaTypes;
        }

        public string ResolveUrl(string appRelativeUrl)
        {
            Guard.NotNullOrEmpty(appRelativeUrl, "appRelativeUrl");
            return VirtualPathUtility.ToAbsolute(appRelativeUrl);
        }
    }
}
