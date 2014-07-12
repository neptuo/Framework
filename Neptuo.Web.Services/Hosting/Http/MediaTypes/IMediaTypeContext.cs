using Neptuo.Web.Services.Hosting.Http.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.MediaTypes
{
    /// <summary>
    /// Describes context of media type.
    /// </summary>
    public interface IMediaTypeContext
    {
        /// <summary>
        /// Media type definition.
        /// </summary>
        HttpMediaType MediaType { get; }

        /// <summary>
        /// Serializer for this media type.
        /// </summary>
        ISerializer Serializer { get; }

        /// <summary>
        /// Deserializer for this media type.
        /// </summary>
        IDeserializer Deserializer { get; }

        /// <summary>
        /// Returns <c>true</c> if <paramref name="request"/> content type matches this content type; false otherwise.
        /// </summary>
        /// <param name="request">Current Http request.</param>
        /// <returns><c>true</c> if <paramref name="request"/> matches this content type; false otherwise.</returns>
        bool IsContentTypeMatch(IHttpRequest request);

        /// <summary>
        /// Returns <c>true</c> if <paramref name="request"/> access type matches this content type; false otherwise.
        /// </summary>
        /// <param name="request">Current Http request.</param>
        /// <returns><c>true</c> if <paramref name="request"/> matches this content type; false otherwise.</returns>
        bool IsAccessTypeMatch(IHttpRequest request);
    }
}
