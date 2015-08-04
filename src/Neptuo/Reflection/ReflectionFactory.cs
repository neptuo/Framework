using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflection
{
    /// <summary>
    /// Factory for creating reflection services.
    /// </summary>
    public static class ReflectionFactory
    {
        /// <summary>
        /// Creates reflection service for <paramref name="appDomain"/>.
        /// </summary>
        /// <param name="appDomain">Application domain...</param>
        /// <returns>Created reflection service.</returns>
        public static IReflectionService FromAppDomain(AppDomain appDomain)
        {
            return new DefaultReflectionService(appDomain);
        }

        /// <summary>
        /// Creates reflection service for current app domain.
        /// </summary>
        /// <returns>Created reflection service.</returns>
        public static IReflectionService FromCurrentAppDomain()
        {
            return FromAppDomain(AppDomain.CurrentDomain);
        }
    }
}
