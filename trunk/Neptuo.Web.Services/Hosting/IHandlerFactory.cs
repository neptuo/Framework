using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting
{
    public interface IHandlerFactory<T>
    {
        T Create(IHttpContext context);
    }
}
