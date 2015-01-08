using Neptuo;
using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
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
            OutputWriter = Console.Out;
        }

        public void Write(string content)
        {
            Console.WriteLine(content);
        }


        public IReadOnlyDictionary<string, string> Headers
        {
            get { throw new NotImplementedException(); }
        }

        public Stream Input
        {
            get { throw new NotImplementedException(); }
        }

        public IReadOnlyDictionary<string, string> QueryString
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<IHttpFile> Files
        {
            get { throw new NotImplementedException(); }
        }

        public HttpStatus Status { get; set; }

        public Stream Output { get; set; }

        public TextWriter OutputWriter { get; set; }


        public IMediaTypeContext OutputContext
        {
            get { throw new NotImplementedException(); }
        }


        public IMediaTypeContext InputContext
        {
            get { throw new NotImplementedException(); }
        }


        public string ResolveUrl(string appRelativeUrl)
        {
            throw new NotImplementedException();
        }


        public IDictionary<string, string> Values
        {
            get { throw new NotImplementedException(); }
        }


        public IReadOnlyDictionary<string, string> Form
        {
            get { throw new NotImplementedException(); }
        }


        IDictionary<string, string> IHttpResponse.Headers
        {
            get { throw new NotImplementedException(); }
        }
    }
}
