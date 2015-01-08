using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Routing.Segments
{
    public class ParamRouteSegment : RouteSegment
    {
        protected bool IsRequired { get; private set; }
        protected IRouteParameter Parameter { get; private set; }

        public ParamRouteSegment(IRouteParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            Parameter = parameter;

            IRequiredRouteParameter requiredParameter = parameter as IRequiredRouteParameter;
            IsRequired = requiredParameter != null && requiredParameter.IsRequired;
        }

        public override bool MatchUrl(RouteSegmentContext context)
        {
            if (IsRequired && String.IsNullOrEmpty(context.OriginalUrl))
                return false;

            return Parameter.MatchUrl(context);
        }
    }
}
