using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing
{
    public abstract class RequiredRouteParameter : IRouteParameter, IRequiredRouteParameter
    {
        public bool IsRequired { get; protected set; }

        public RequiredRouteParameter()
        {
            IsRequired = true;
        }

        public bool MatchUrl(IRouteParameterContext context)
        {
            if (IsRequired && String.IsNullOrEmpty(context.OriginalUrl))
                return false;

            return MatchRequiredUrl(context);
        }

        public abstract bool MatchRequiredUrl(IRouteParameterContext context);

    }
}
