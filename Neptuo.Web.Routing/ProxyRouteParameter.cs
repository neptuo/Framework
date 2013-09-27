using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing
{
    public class ProxyRouteParameter : IRouteParameter
    {
        public static readonly char[] Separators = new char[] { '/', '.' };

        public string Name { get; private set; }

        public ProxyRouteParameter(string name)
        {
            Name = name;
        }

        public bool MatchUrl(IRouteParameterMatchContext context)
        {
            foreach (char separator in Separators)
            {
                int index = context.OriginalUrl.IndexOf(separator);
                if (index > 0)
                {
                    context.Values[Name] = context.OriginalUrl.Substring(0, index);
                    context.RemainingUrl = context.OriginalUrl.Substring(index);
                    return true;
                }
            }

            context.Values[Name] = context.OriginalUrl;
            context.RemainingUrl = null;
            return true;
        }
    }
}
