using Neptuo.Net.Http.Clients.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Net.Http.Clients
{
    /// <summary>
    /// Adapter between service input object and <see cref="HttpClient"/>.
    /// </summary>
    public class HttpClientAdapter
    {
        private const string headerContentType = "Content-type";

        private const string xmlContentType = "text/xml";
        private const string jsonContentType = "text/json";

        private readonly IRouteTable routeTable;

        /// <summary>
        /// Creates new instance with absolute URLs defined in <paramref name="routeTable"/>.
        /// </summary>
        /// <param name="routeTable">Route table with absolute URLs.</param>
        public HttpClientAdapter(IRouteTable routeTable)
        {
            Ensure.NotNull(routeTable, "routeTable");
            this.routeTable = routeTable;
        }

        /// <summary>
        /// Prepeares instance of <see cref="HttpClient"/>, sets comunication headers.
        /// </summary>
        /// <param name="route">Route definition.</param>
        /// <returns>Prepared HTTP clienct.</returns>
        public virtual HttpClient PrepareHttpClient(RouteDefinition route)
        {
            HttpClient httpClient = new HttpClient();

            switch (route.Serialization)
            {
                case RouteSerialization.Xml:
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(xmlContentType));
                    if (route.Method != RouteMethod.Get)
                        httpClient.DefaultRequestHeaders.Add(headerContentType, xmlContentType);

                    break;
                case RouteSerialization.Json:
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonContentType));
                    if (route.Method != RouteMethod.Get)
                        httpClient.DefaultRequestHeaders.Add(headerContentType, jsonContentType);

                    break;
                default:
                    throw Ensure.Exception.NotSupportedSerialization(route.Serialization);
            }

            return httpClient;
        }

        /// <summary>
        /// Creates <see cref="ObjectContent"/> for content in <paramref name="data"/> with routed to <paramref name="route"/>.
        /// </summary>
        /// <param name="data">Input parameters.</param>
        /// <param name="route">Route definition for <paramref name="data"/>.</param>
        /// <returns>Instance of <see cref="ObjectContent"/>.</returns>
        public virtual ObjectContent PrepareObjectContent(object data, RouteDefinition route)
        {
            switch (route.Serialization)
            {
                case RouteSerialization.Xml:
                    return new ObjectContent(data.GetType(), data, new XmlMediaTypeFormatter());
                case RouteSerialization.Json:
                    return new ObjectContent(data.GetType(), data, new JsonMediaTypeFormatter());
                default:
                    throw Ensure.Exception.NotSupportedSerialization(route.Serialization);
            }
        }

        /// <summary>
        /// Executes HTTP request for <paramref name="content"/> routed to <paramref name="route"/>.
        /// </summary>
        /// <param name="httpClient">Prepared HTTP client.</param>
        /// <param name="content">HTTP request body content.</param>
        /// <param name="route">Route definition for <paramref name="data"/>.</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> Execute(HttpClient httpClient, ObjectContent content, RouteDefinition route)
        {
            Uri url;
            if (!Uri.TryCreate(route.Url, UriKind.Absolute, out url))
                throw Ensure.Exception.InvalidOperation("Unnable to process input when route URL is '{0}'.", route.Url);

            switch (route.Method)
            {
                case RouteMethod.Post:
                    return httpClient.PostAsync(url, content);
                case RouteMethod.Put:
                    return httpClient.PutAsync(url, content);
                case RouteMethod.Get:
                    return httpClient.GetAsync(PrepareGetUrl(route.Url, content));
                default:
                    throw Ensure.Exception.NotSupportedMethod(route.Method);
            }
        }

        /// <summary>
        /// Serializes object in <paramref name="content"/> as query string parameters to <paramref name="url"/>.
        /// In default implementation reads public properties, values are serialized using <see cref="HttpClientAdapter.SerializeGetValue"/>.
        /// </summary>
        /// <param name="url">Base URL (may contain any parameters).</param>
        /// <param name="content">HTTP request body content.</param>
        /// <returns>Modified <paramref name="url"/> to contain parameters from <paramref name="content"/>.</returns>
        protected string PrepareGetUrl(string url, ObjectContent content)
        {
            StringBuilder result = new StringBuilder();
            result.Append(url);
            bool hasParameter = url.Contains("?");

            IEnumerable<PropertyInfo> properties = content.ObjectType.GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                object value = propertyInfo.GetValue(content.Value);
                if (value != null)
                {
                    result.Append(hasParameter ? "&" : "?");
                    hasParameter = true;

                    result.AppendFormat("{0}={1}", propertyInfo.Name, SerializeGetValue(value));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Serializes <paramref name="value"/> to query string.
        /// In default implementation, collection (<see cref="IEnumerable"/>) are serialized as ',' delimited list.
        /// Other values are 'ToString' and <see cref="Uri.EscapeUriString"/>.
        /// </summary>
        /// <param name="value">Value to serialize.</param>
        /// <returns>Serialized <paramref name="value"/>.</returns>
        protected virtual string SerializeGetValue(object value)
        {
            IEnumerable enumerable = value as IEnumerable;
            if (enumerable != null)
                return String.Join(",", enumerable);

            return Uri.EscapeUriString(value.ToString());
        }
    }
}
