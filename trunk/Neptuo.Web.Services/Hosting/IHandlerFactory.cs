using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting
{
    /// <summary>
    /// Factory for creating instances of handlers.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public interface IHandlerFactory<T>
    {
        /// <summary>
        /// Creates instance of handler.
        /// </summary>
        /// <param name="context">Current Http context.</param>
        /// <returns>Instance of handler.</returns>
        T Create(IHttpContext context);
    }
}
