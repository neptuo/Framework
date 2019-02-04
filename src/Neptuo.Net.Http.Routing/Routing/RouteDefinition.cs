﻿using Neptuo.Formatters.Generics;
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
        public ISerializer Serializer { get; }

        /// <summary>
        /// Gets a content type for the route.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="url">A route URL.</param>
        /// <param name="method">A HTTP method used for the route.</param>
        /// <param name="serializer">A serializer for the route.</param>
        /// <param name="contentType">A content type for the route.</param>
        public RouteDefinition(string url, RouteMethod method, ISerializer serializer, string contentType)
        {
            Url = url;
            Method = method;
            Serializer = serializer;
            ContentType = contentType;
        }
    }
}
