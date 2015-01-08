using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using Neptuo.Web.Services.Hosting.Pipelines;
using Neptuo.Web.Services.Hosting.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Neptuo.Web.Services.Hosting.Http
{
    /// <summary>
    /// Web endpoint for Neptuo.Web.Services.
    /// Handler requests and executes registered pipelines.
    /// </summary>
    public class AspNetHttpServiceHandler : IHttpHandler
    {
        private readonly IRouteTable routeTable;
        private readonly IMediaTypeCollection mediaTypes;

        public bool IsReusable
        {
            get { return true; }
        }

        public AspNetHttpServiceHandler(IRouteTable routeTable, IMediaTypeCollection mediaTypes)
        {
            Guard.NotNull(routeTable, "routeTable");
            Guard.NotNull(mediaTypes, "mediaTypes");
            this.routeTable = routeTable;
            this.mediaTypes = mediaTypes;
        }

        public void ProcessRequest(HttpContext context)
        {
            IHttpContext httpContext = new AspNetHttpContext(context, mediaTypes);
            ProcessRequest(httpContext, routeTable);
        }

        protected void ProcessRequest(IHttpContext context, IRouteTable routeTable)
        {
            IPipelineFactory pipelineFactory;
            if(routeTable.TryGet(context.Request, out pipelineFactory))
            {
                IPipeline pipeline = pipelineFactory.Create();
                pipeline.Invoke(context);
            }
            else
            {
                context.Response.Status = HttpStatus.NotFound;
            }
        }
    }
}
