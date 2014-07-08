using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.WebServices
{
    public class ConsoleContext : IHttpContext, IHttpRequest, IHttpResponse
    {
        public IHttpRequest Request { get { return this; } }
        public IHttpResponse Response { get { return this; } }

        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
}
