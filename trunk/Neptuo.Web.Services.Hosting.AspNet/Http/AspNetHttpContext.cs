using Neptuo.Web.Services.Hosting.Http;
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
        private readonly HttpContext httpContext;
        private IHttpRequest request;
        private IHttpResponse response;

        public IHttpRequest Request
        {
            get
            {
                if (request == null)
                    request = new AspNetHttpRequest(httpContext.Request);

                return request;
            }
        }

        public IHttpResponse Response
        {
            get
            {
                if (response == null)
                    response = new AspNetHttpResponse(httpContext.Response);

                return response;
            }
        }

        public AspNetHttpContext(HttpContext httpContext)
        {
            Guard.NotNull(httpContext, "httpContext");
            this.httpContext = httpContext;
        }


        public string ResolveUrl(string appRelativeUrl)
        {
            Guard.NotNullOrEmpty(appRelativeUrl, "appRelativeUrl");
            return VirtualPathUtility.ToAbsolute(appRelativeUrl);
        }
    }
}
