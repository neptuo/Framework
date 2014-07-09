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
    /// Describes Http response.
    /// </summary>
    public interface IHttpResponse
    {
        /// <summary>
        /// Http response status.
        /// </summary>
        HttpStatus Status { get; set; }

        /// <summary>
        /// Http response headers.
        /// </summary>
        NameValueCollection Headers { get; }

        /// <summary>
        /// Response stream
        /// </summary>
        Stream Output { get; }

        /// <summary>
        /// Response text writer.
        /// </summary>
        TextWriter OutputWriter { get; }
    }
}
