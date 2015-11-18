using Neptuo.Services.HttpUtilities;
using Neptuo.Services.HttpUtilities.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Commands
{
    /// <summary>
    /// Implementation of <see cref="ICommandDispatcher"/> that transfers commands over HTTP.
    /// </summary>
    public class HttpCommandDispatcher : ICommandDispatcher
    {
        private readonly IRouteTable routeTable;
        private readonly HttpClientAdapter httpAdapter;

        /// <summary>
        /// Creates new instance with absolute URLs defined in <paramref name="routeTable"/>.
        /// </summary>
        /// <param name="routeTable">Route table with absolute URLs.</param>
        public HttpCommandDispatcher(IRouteTable routeTable)
        {
            Ensure.NotNull(routeTable, "routeTable");
            this.routeTable = routeTable;
            this.httpAdapter = new HttpClientAdapter(routeTable);
        }

        public async Task HandleAsync<TCommand>(TCommand command)
        {
            Ensure.NotNull(command, "command");
            Type commandType = command.GetType();
            RouteDefinition route;
            if (routeTable.TryGet(commandType, out route))
            {
                using (HttpClient httpClient = httpAdapter.PrepareHttpClient(route))
                {
                    // Prepare content and send request.
                    ObjectContent objectContent = httpAdapter.PrepareObjectContent(command, route);
                    HttpResponseMessage response = await httpAdapter.Execute(httpClient, objectContent, route);

                    //TODO: Process HTTP status errors.
                    return;
                }
            }

            throw Ensure.Exception.InvalidOperation("Unnable to preces command without registered URL route, command type is '{0}'.", commandType.FullName);
        }
    }
}
