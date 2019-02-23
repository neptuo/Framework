using Neptuo.Formatters.Generics;
using Neptuo.Net.Http.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Neptuo.Net.Http
{
    public class ObjectSender
    {
        private readonly HttpClient httpClient;
        private readonly IRouteProvider routes;

        public ObjectSender(HttpClient httpClient, IRouteProvider routes)
        {
            Ensure.NotNull(httpClient, "httpClient");
            Ensure.NotNull(routes, "routes");
            this.httpClient = httpClient;
            this.routes = routes;
        }

        public async Task<object> SendAsync(object message, CancellationToken cancellationToken)
        {
            Ensure.NotNull(message, "message");

            Type messageType = message.GetType();
            if (routes.TryGet(messageType, out RouteDefinition route))
            {
                HttpRequestMessage httpRequest = await CreateRequestAsync(message, route);
                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest, cancellationToken);
                httpResponse.EnsureSuccessStatusCode();

                if (route.ResponseDeserializer != null)
                {
                    using (Stream httpResponseContent = await httpResponse.Content.ReadAsStreamAsync())
                    {
                        var responseDeserializerContext = new DefaultDeserializerContext();
                        if (await route.ResponseDeserializer.TryDeserializeAsync(httpResponseContent, responseDeserializerContext))
                        {
                            return responseDeserializerContext.Output;
                        }
                        else
                        {
                            // TODO: Throw.
                        }
                    }
                }
            }

            // Throw.
            throw Ensure.Exception.NotImplemented();
        }

        private async Task<HttpRequestMessage> CreateRequestAsync(object message, RouteDefinition route)
        {
            HttpMethod httpMethod = HttpMethod.Post;
            string httpUrl = route.Url;

            switch (route.Method)
            {
                case RouteMethod.Post:
                    httpMethod = HttpMethod.Post;
                    break;
                case RouteMethod.Put:
                    httpMethod = HttpMethod.Put;
                    break;
                case RouteMethod.Delete:
                    httpMethod = HttpMethod.Delete;
                    break;
                case RouteMethod.Get:
                    httpMethod = HttpMethod.Get;
                    break;
                default:
                    throw Ensure.Exception.NotSupported(route.Method);
            }

            StringContent httpContent = new StringContent(await route.RequestSerializer.SerializeAsync(message));

            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Content = httpContent,
                Method = httpMethod,
                RequestUri = new Uri(httpUrl, UriKind.Absolute)
            };

            httpRequest.Headers.Add("Content-Type", route.ContentType);
            httpRequest.Headers.Accept.Clear();
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(route.ContentType));

            // TODO: Additional headers like authentication.

            return httpRequest;
        }
    }
}
