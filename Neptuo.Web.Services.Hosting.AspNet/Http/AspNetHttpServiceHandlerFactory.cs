using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Manages instances of <see cref="AspNetHttpServiceHandler"/>.
    /// </summary>
    public class AspNetHttpServiceHandlerFactory : IHttpHandlerFactory
    {
        /// <summary>
        /// Creates new instance of <see cref="AspNetHttpServiceHandler"/>.
        /// </summary>
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            return new AspNetHttpServiceHandler(ServiceEnvironment.RouteTable, ServiceEnvironment.MediaTypes);
        }

        /// <summary>
        /// Disposes instance of <see cref="AspNetHttpServiceHandler"/>.
        /// </summary>
        public void ReleaseHandler(IHttpHandler handler)
        { }
    }
}
