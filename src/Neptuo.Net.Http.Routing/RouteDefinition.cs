using Neptuo.Formatters.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Routing
{
    /// <summary>
    /// A route definition.
    /// </summary>
    public class RouteDefinition
    {
        /// <summary>
        /// Gets a route URL.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Gets a HTTP method used for the route.
        /// </summary>
        public RouteMethod Method { get; }

        /// <summary>
        /// Gets a serializer for the route.
        /// </summary>
        public ISerializer RequestSerializer { get; }

        /// <summary>
        /// Gets a deserializer for processing of the response.
        /// </summary>
        public IDeserializer ResponseDeserializer { get; }

        /// <summary>
        /// Gets a content type for the route.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="url">A route URL.</param>
        /// <param name="method">A HTTP method used for the route.</param>
        /// <param name="requestSerializer">A serializer for the route.</param>
        /// <param name="responseDeserializer">A deserializer for processing of the response.</param>
        /// <param name="contentType">A content type for the route.</param>
        public RouteDefinition(string url, RouteMethod method, ISerializer requestSerializer, IDeserializer responseDeserializer, string contentType)
        {
            Url = url;
            Method = method;
            RequestSerializer = requestSerializer;
            ResponseDeserializer = responseDeserializer;
            ContentType = contentType;
        }
    }
}
