using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.WebServices
{
    public class GetHelloFactory : IHandlerFactory<GetHello>
    {
        public GetHello Create(IHttpContext context)
        {
            return new GetHello();
        }
    }
}
