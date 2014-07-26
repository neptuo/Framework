using Neptuo.Collections.Specialized;
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
        /// <summary>
        /// Http request.
        /// </summary>
        private readonly IHttpRequest request;

        /// <summary>
        /// Original http response.
        /// </summary>
        private readonly HttpResponse response;

        /// <summary>
        /// Collection of all supported media types.
        /// </summary>
        private readonly IMediaTypeCollection mediaTypes;


        /// <summary>
        /// Cached collection of http headers.
        /// </summary>
        private IDictionary<string, string> headers;

        /// <summary>
        /// Cached output media type context.
        /// </summary>
        private IMediaTypeContext outputContext;

        public HttpStatus Status
        {
            get { return response.StatusCode; }
            set
            {
                if (value != null)
                {
                    response.StatusCode = value.Code;
                    //response.Status = value.Text; TODO: How to fix non standart status text.
                }
            }
        }

        public IDictionary<string, string> Headers
        {
            get
            {
                if (headers == null)
                    headers = new NameValueDictionary(response.Headers);

                return headers;
            }
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
            get
            {
                if (outputContext == null)
                    outputContext = mediaTypes.FindAcceptTypeContext(request);

                return outputContext;
            }
        }

        public AspNetHttpResponse(HttpResponse response, IHttpRequest request, IMediaTypeCollection mediaTypes)
        {
            Guard.NotNull(response, "response");
            Guard.NotNull(request, "request");
            Guard.NotNull(mediaTypes, "mediaTypes");
            this.response = response;
            this.request = request;
            this.mediaTypes = mediaTypes;
        }
    }
}
