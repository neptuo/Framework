using Neptuo.Globalization;
using Neptuo.Web.Routing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TestMvc4.Models
{
    public class LocaleRouteParameter : IRouteParameter
    {
        private string defaultLocale;

        public LocaleRouteParameter(string defaultLocale)
        {
            this.defaultLocale = defaultLocale;
        }

        public bool MatchUrl(IRouteParameterMatchContext context)
        {
            if (String.IsNullOrEmpty(context.OriginalUrl))
            {
                if (!String.IsNullOrEmpty(defaultLocale))
                {
                    context.Values["locale"] = CultureInfo.GetCultureInfo(defaultLocale);
                    return true;
                }
                return false;

            }

            int length = 5;
            CultureInfo culfureInfo = null;
            if (context.OriginalUrl.Length >= 5)
            {
                if (!CultureInfoParser.TryParse(context.OriginalUrl.Substring(0, 5), out culfureInfo))
                {
                    CultureInfoParser.TryParse(context.OriginalUrl.Substring(0, 2), out culfureInfo);
                    length = 2;
                }
            }
            else if (context.OriginalUrl.Length >= 2)
            {
                CultureInfoParser.TryParse(context.OriginalUrl.Substring(0, 2), out culfureInfo);
                length = 2;
            }

            if (culfureInfo != null)
            {
                context.Values["locale"] = culfureInfo;
                context.RemainingUrl = context.OriginalUrl.Substring(length);
                return true;
            }

            if (!String.IsNullOrEmpty(defaultLocale))
            {
                context.Values["locale"] = CultureInfo.GetCultureInfo(defaultLocale);
                return true;
            }
            return false;
        }
    }
}