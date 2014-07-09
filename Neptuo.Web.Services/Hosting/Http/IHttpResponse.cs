using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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


    }
}
