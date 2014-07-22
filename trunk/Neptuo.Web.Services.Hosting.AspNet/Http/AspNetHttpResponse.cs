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
    /// Wraps <see cref="HttpResponse"/>.
    /// </summary>
    public class AspNetHttpResponse : IHttpResponse
    {
        private HttpResponse response;

        public HttpStatus Status
        {
            get { return response.StatusCode; }
            set
            {
                if (value != null)
                {
                    response.StatusCode = value.Code;
                    //response.Status = value.Text;
                }
            }
        }

        public NameValueCollection Headers
        {
            get { return response.Headers; }
        }

        public Stream Output
        {
            get { return response.OutputStream; }
        }

        public TextWriter OutputWriter
        {
            get { return response.Output; }
        }

        public IMediaTypeContext OutputContext
        {
            get { throw new NotImplementedException(); }
        }

        public AspNetHttpResponse(HttpResponse response)
        {
            Guard.NotNull(response, "response");
            this.response = response;
        }
    }
}
