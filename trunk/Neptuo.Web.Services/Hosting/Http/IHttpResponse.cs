using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http
{
    public interface IHttpResponse
    {
        void Write(string content);
    }
}
