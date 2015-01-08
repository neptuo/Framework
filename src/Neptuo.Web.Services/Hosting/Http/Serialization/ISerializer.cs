using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.Serialization
{
    /// <summary>
    /// Serializes objects to response.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Tries to serialize <paramref name="model"/> to <paramref name="response"/>.
        /// If is not able to serialize it, returns <c>false</c>; otherwise true.
        /// </summary>
        /// <typeparam name="T">Type of model to serialize.</typeparam>
        /// <param name="response">Current Http response.</param>
        /// <param name="model">Model to serialize.</param>
        /// <returns>If is not able to serialize it, returns <c>false</c>; otherwise true.</returns>
        bool TrySerialize<T>(IHttpResponse response, T model);
    }
}
