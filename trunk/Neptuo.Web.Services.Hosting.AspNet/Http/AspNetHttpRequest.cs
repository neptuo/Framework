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
    public class AspNetHttpRequest : IHttpRequest
    {
        private HttpRequest request;

        public HttpMethod Method
        {
            get { throw new NotImplementedException(); }
        }

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

        public IMediaTypeContext OutputContext
        {
            get { throw new NotImplementedException(); }
        }

        public NameValueCollection QueryString
        {
            get { return request.QueryString; }
        }

        public IEnumerable<IHttpFile> Files
        {
            get { throw new NotImplementedException(); }
        }

        public AspNetHttpRequest(HttpRequest request)
        {
            Guard.NotNull(request, "request");
            this.request = request;
        }
    }
}
