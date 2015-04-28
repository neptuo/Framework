using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections
{
    /// <summary>
    /// Common extensions for <see cref="EngineEvironment"/> for 
    /// </summary>
    public static class _EnvironmentExtensions
    {
        public static EngineEnvironment UseIsConcurrentApplication(this EngineEnvironment environment, bool isConcurrent)
        {
            Ensure.NotNull(environment, "environment");
            return environment.Use<bool>(isConcurrent, "IsConcurrentApplication");
        }

        public static bool WithIsConcurrentApplication(this EngineEnvironment environment)
        {
            Ensure.NotNull(environment, "environment");
            if (!environment.Has<bool>("IsConcurrentApplication"))
                UseIsConcurrentApplication(environment, true);

            return environment.With<bool>("IsConcurrentApplication");
        }
    }
}
