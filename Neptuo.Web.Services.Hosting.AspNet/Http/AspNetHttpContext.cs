using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    public class AspNetHttpContext : IHttpContext
    {
        public IHttpRequest Request
        {
            get { throw new NotImplementedException(); }
        }

        public IHttpResponse Response
        {
            get { throw new NotImplementedException(); }
        }
    }
}
