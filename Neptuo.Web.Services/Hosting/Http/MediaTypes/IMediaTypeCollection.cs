using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.MediaTypes
{
    /// <summary>
    /// Contains registered media type serializers and deserializers.
    /// </summary>
    public interface IMediaTypeCollection
    {
        /// <summary>
        /// Registers <paramref name="context"/>.
        /// </summary>
        /// <param name="context">Media type context to register.</param>
        void Add(IMediaTypeContext context);

        /// <summary>
        /// Finds media type context for <paramref name="request"/> specified in Http header Content-Type.
        /// </summary>
        /// <param name="request">Current Http context.</param>
        /// <returns>Media type context for <paramref name="request"/>.</returns>
        IMediaTypeContext FindContentTypeContext(IHttpRequest request);

        /// <summary>
        /// Finds media type context for <paramref name="request"/> specified in Http header Accept.
        /// </summary>
        /// <param name="request">Current Http context.</param>
        /// <returns>Media type context for response of <paramref name="request"/>.</returns>
        IEnumerable<IMediaTypeCollection> FindAccessTypeContext(IHttpRequest request);
    }
}
