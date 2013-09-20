using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Web
{
    /// <summary>
    /// Only for Request/Response use!
    /// </summary>
    public class HttpContextBaseWrapper : HttpContextBase
    {
        /// <summary>
        /// Wrapped <see cref="HttpContextBase"/>.
        /// </summary>
        protected HttpContextBase HttpContextBase { get; private set; }

        /// <summary>
        /// Wrapped <see cref="HttpRequestBase"/>.
        /// </summary>
        protected HttpRequestBase HttpRequestBase { get; private set; }

        /// <summary>
        /// Wrapped <see cref="HttpResponseBase"/>.
        /// </summary>
        protected HttpResponseBase HttpResponseBase { get; private set; }

        /// <summary>
        /// Wrapped request.
        /// </summary>
        public override HttpRequestBase Request
        {
            get { return HttpRequestBase; }
        }

        /// <summary>
        /// Wrapped response.
        /// </summary>
        public override HttpResponseBase Response
        {
            get { return HttpResponseBase; }
        }

        public HttpContextBaseWrapper(HttpContextBase httpContextBase)
        {
            HttpContextBase = httpContextBase;
            HttpRequestBase = CreateRequestWrapper();
        }

        /// <summary>
        /// Creates wrapper for request.
        /// Can use <see cref="HttpContextBaseWrapper.HttpContextBase"/>.
        /// </summary>
        /// <returns>Wrapper for request.</returns>
        protected virtual HttpRequestBaseWrapper CreateRequestWrapper()
        {
            return new HttpRequestBaseWrapper(HttpContextBase.Request);
        }

        /// <summary>
        /// Creates wrapper for response.
        /// Can use <see cref="HttpContextBaseWrapper.HttpContextBase"/>.
        /// </summary>
        /// <returns>Wrapper for response.</returns>
        protected virtual HttpResponseBase CreateResponseWrapper()
        {
            return HttpContextBase.Response;
        }
    }
}
