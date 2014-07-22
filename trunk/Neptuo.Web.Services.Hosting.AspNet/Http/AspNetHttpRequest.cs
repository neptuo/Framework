using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Wraps <see cref="HttpRequest"/>.
    /// </summary>
    public class AspNetHttpRequest : IHttpRequest
    {
        private HttpRequest request;

        public HttpMethod Method { get; private set; }

        public Uri Url
        {
            get { return request.Url; }
        }

        public NameValueCollection Headers
        {
            get { return request.Headers; }
        }

        public Stream Input
        {
            get { return request.InputStream; }
        }

        public IMediaTypeContext InputContext
        {
            get { throw new NotImplementedException(); }
        }

        public NameValueCollection QueryString
        {
            get { return request.QueryString; }
        }

        public NameValueCollection Form
        {
            get { return request.Form; }
        }

        public IEnumerable<IHttpFile> Files
        {
            get { throw new NotImplementedException(); }
        }

        public AspNetHttpRequest(HttpRequest request)
        {
            Guard.NotNull(request, "request");
            this.request = request;
            Method = (HttpMethod)request.HttpMethod;
        }
    }
}
