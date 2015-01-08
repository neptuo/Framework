using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting
{
    public class DefaultHandlerFactory<T> : IHandlerFactory<T>
        where T : new()
    {
        public T Create(IHttpContext context)
        {
            return new T();
        }
    }
}
