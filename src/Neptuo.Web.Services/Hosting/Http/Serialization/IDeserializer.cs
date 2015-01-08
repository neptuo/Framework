using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.Serialization
{
    /// <summary>
    /// Deserializes objects from request.
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Tries to deserialize <paramref name="model"/> from <paramref name="request"/>.
        /// If is not able to deserialize it, returns <c>false</c>; otherwise true.
        /// </summary>
        /// <typeparam name="T">Type of model to serialize.</typeparam>
        /// <param name="request">Current Http request.</param>
        /// <param name="model">Model to serialize.</param>
        /// <returns>If is not able to deserialize it, returns <c>false</c>; otherwise true.</returns>
        bool TryDeserialize<T>(IHttpRequest request, out T model);
    }
}
