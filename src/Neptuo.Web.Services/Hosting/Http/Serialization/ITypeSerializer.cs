using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.Serialization
{
    public interface ITypeSerializer<T>
    {
        bool TrySerialize(IHttpResponse response, T model);
    }

    public interface ITypeDeserializer<T>
    {
        bool TryDeserialize(IHttpRequest request, out T model);
    }
}
