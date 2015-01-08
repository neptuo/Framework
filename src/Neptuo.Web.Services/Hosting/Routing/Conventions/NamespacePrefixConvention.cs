using Neptuo.Web.Services.Hosting.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Routing.Conventions
{
    /// <summary>
    /// Converts full name: <c>PrefixToRemove.Prefix.Accounts.GetLogin</c> into <c>GET ~/prefix/accounts/login</c>.
    /// </summary>
    public class NamespacePrefixConvention : IConvention
    {
        /// <summary>
        /// Namespace prefix to remove from url.
        /// </summary>
        private string namespacePrefix;

        /// <summary>
        /// Creates new instance with <paramref name="namespacePrefix"/> as namespace prefix to remove from url.
        /// </summary>
        /// <param name="namespacePrefix">Namespace prefix to remove from url.</param>
        public NamespacePrefixConvention(string namespacePrefix)
        {
            Guard.NotNullOrEmpty(namespacePrefix, "namespacePrefix");
            this.namespacePrefix = namespacePrefix;
        }

        public bool TryGetRoute(Type typeDefinition, out IRoute route)
        {
            if (typeDefinition.FullName.StartsWith(namespacePrefix))
            {
                string typeName = typeDefinition.Name.ToLowerInvariant();
                foreach (HttpMethod knownMethod in HttpMethod.KnownMethods)
                {
                    if (typeName.StartsWith(knownMethod.Name.ToLowerInvariant()))
                    {
                        string nameUrl = typeName.Substring(knownMethod.Name.Length);
                        string namespaceUrl = typeDefinition.Namespace.Substring(namespacePrefix.Length);
                        namespaceUrl = namespaceUrl.ToLowerInvariant().Replace(".", "/");

                        if (!namespaceUrl.StartsWith("/"))
                            namespaceUrl = "/" + namespaceUrl;

                        if (!namespaceUrl.EndsWith("/"))
                            namespaceUrl += "/";

                        string url = String.Format("~{0}{1}", namespaceUrl, nameUrl);
                        route = new UriMethodRoute(url, UriKind.Relative, knownMethod);
                        return true;
                    }
                }
            }

            route = null;
            return false;
        }
    }
}
