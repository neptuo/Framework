using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Http request.
    /// </summary>
    public interface IHttpRequest
    {
        /// <summary>
        /// Http method.
        /// </summary>
        HttpMethod Method { get; }

        /// <summary>
        /// Requested url.
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// Http request headers.
        /// </summary>
        NameValueCollection Headers { get; }

        /// <summary>
        /// Input stream
        /// </summary>
        Stream Input { get; }

        /// <summary>
        /// Input stream type and other related settings.
        /// </summary>
        IMediaTypeContext OutputContext { get; }

        /// <summary>
        /// Input query string.
        /// </summary>
        NameValueCollection QueryString { get; }

        /// <summary>
        /// Posted files.
        /// </summary>
        IEnumerable<IHttpFile> Files { get; }
    }
}
