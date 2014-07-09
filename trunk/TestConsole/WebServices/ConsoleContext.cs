using Neptuo;
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

        public HttpMethod Method { get; private set; }
        public Uri Url { get; private set; }

        public ConsoleContext(HttpMethod method, Uri url)
        {
            Guard.NotNull(method, "method");
            Guard.NotNull(url, "url");
            Method = method;
            Url = url;
        }

        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
}
