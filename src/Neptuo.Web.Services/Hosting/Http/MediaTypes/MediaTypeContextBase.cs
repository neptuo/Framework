using Neptuo.Web.Services.Hosting.Http.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Http.MediaTypes
{
    /// <summary>
    /// Base implementation of <see cref="IMediaTypeContext"/>.
    /// </summary>
    public class MediaTypeContextBase : IMediaTypeContext
    {
        public HttpMediaType MediaType { get; private set; }
        public ISerializer Serializer { get; private set; }
        public IDeserializer Deserializer { get; private set; }

        public MediaTypeContextBase(HttpMediaType mediaType, ISerializer serializer, IDeserializer deserializer)
        {
            Guard.NotNull(mediaType, "mediaType");
            Guard.NotNull(serializer, "serializer");
            Guard.NotNull(deserializer, "deserializer");
            Initialize(mediaType, serializer, deserializer);
        }

        protected void Initialize(HttpMediaType mediaType, ISerializer serializer, IDeserializer deserializer)
        {
            MediaType = mediaType;
            Serializer = serializer;
            Deserializer = deserializer;
        }

        public bool IsContentTypeMatch(IHttpRequest request)
        {
            throw new NotImplementedException();
        }

        public bool IsAccessTypeMatch(IHttpRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
