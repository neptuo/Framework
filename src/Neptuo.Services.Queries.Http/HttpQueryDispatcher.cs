using Neptuo.Services.Queries.Routing;
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

namespace Neptuo.Services.Queries
{
    /// <summary>
    /// Implementation of <see cref="IQueryDispatcher"/> that transfers queries over HTTP.
    /// </summary>
    public class HttpQueryDispatcher : IQueryDispatcher
    {
        private readonly IRouteTable routeTable;

        /// <summary>
        /// Creates new instance with absolute URLs defined in <paramref name="routeTable"/>.
        /// </summary>
        /// <param name="routeTable">Route table with absolute URLs.</param>
        public HttpQueryDispatcher(IRouteTable routeTable)
        {
            Ensure.NotNull(routeTable, "routeTable");
            this.routeTable = routeTable;
        }

        public async Task<TOutput> QueryAsync<TOutput>(IQuery<TOutput> query)
        {
            Ensure.NotNull(query, "query");
            Type queryType = query.GetType();
            RouteDefinition route;
            if (routeTable.TryGet(queryType, out route))
            {
                using (HttpClient httpClient = PrepareHttpClient(route))
                {
                    // Prepare content and send request.
                    ObjectContent objectContent = PrepareObjectContent(query, route);
                    HttpResponseMessage response = await ExecuteQuery(httpClient, objectContent, route);

                    // Parse output.
                    TOutput output = await response.Content.ReadAsAsync<TOutput>();
                    return output;
                }
            }

            throw Ensure.Exception.InvalidOperation("Unnable to preces query without registered URL route, query type is '{0}'.", queryType.FullName);
        }

        /// <summary>
        /// Prepeares instance of <see cref="HttpClient"/>, sets comunication headers.
        /// </summary>
        /// <param name="route">Route definition.</param>
        /// <returns>Prepared HTTP clienct.</returns>
        protected virtual HttpClient PrepareHttpClient(RouteDefinition route)
        {
            HttpClient httpClient = new HttpClient();

            switch (route.Serialization)
            {
                case RouteSerialization.Xml:
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                    if (route.Method != RouteMethod.Get)
                        httpClient.DefaultRequestHeaders.Add("Content-type", "text/xml");

                    break;
                case RouteSerialization.Json:
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
                    if (route.Method != RouteMethod.Get)
                        httpClient.DefaultRequestHeaders.Add("Content-type", "text/json");

                    break;
                default:
                    throw Ensure.Exception.NotSupported("Not supported serialization type '{0}'.", route.Serialization);
            }

            return httpClient;
        }

        /// <summary>
        /// Creates <see cref="ObjectContent"/> for content in <paramref name="query"/> with routed to <paramref name="route"/>.
        /// </summary>
        /// <typeparam name="TOutput">Type query output.</typeparam>
        /// <param name="query">Input query parameters.</param>
        /// <param name="route">Query route.</param>
        /// <returns>Instance of <see cref="ObjectContent"/>.</returns>
        protected virtual ObjectContent PrepareObjectContent<TOutput>(IQuery<TOutput> query, RouteDefinition route)
        {
            switch (route.Serialization)
            {
                case RouteSerialization.Xml:
                    return new ObjectContent(query.GetType(), query, new XmlMediaTypeFormatter());
                case RouteSerialization.Json:
                    return new ObjectContent(query.GetType(), query, new JsonMediaTypeFormatter());
                default:
                    throw Ensure.Exception.NotSupported("Not supported serialization type '{0}'.", route.Serialization);
            }
        }

        /// <summary>
        /// Executes HTTP request for <paramref name="content"/> routed to <paramref name="route"/>.
        /// </summary>
        /// <param name="httpClient">Prepared HTTP client.</param>
        /// <param name="content">HTTP request body content.</param>
        /// <param name="route">Query route.</param>
        /// <returns></returns>
        protected Task<HttpResponseMessage> ExecuteQuery(HttpClient httpClient, ObjectContent content, RouteDefinition route)
        {
            Uri url;
            if (!Uri.TryCreate(route.Url, UriKind.Absolute, out url)) 
                throw Ensure.Exception.InvalidOperation("Unnable to process query when route URL is '{0}'.", route.Url);

            switch (route.Method)
            {
                case RouteMethod.Post:
                    return httpClient.PostAsync(url, content);
                case RouteMethod.Put:
                    return httpClient.PutAsync(url, content);
                case RouteMethod.Get:
                    return httpClient.GetAsync(PrepareGetUrl(route.Url, content));
                default:
                    throw Ensure.Exception.NotSupported("Not supported route method '{0}'.", route.Method);
            }
        }

        /// <summary>
        /// Serializes object in <paramref name="content"/> as query string parameters to <paramref name="url"/>.
        /// In default implementation reads public properties, values are serialized using <see cref="HttpQueryDispatcher.SerializeGetValue"/>.
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
