using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    /// <summary>
    /// Container for holding singleton data.
    /// </summary>
    public static class Engine
    {
        /// <summary>
        /// Lock object for creating environment.
        /// </summary>
        private static object environmentLock = new object();

        private static EngineEnvironment environment;

        /// <summary>
        /// Holds root dependency container for application.
        /// </summary>
        public static IDependencyContainer RootContainer
        {
            get { return Environment.With<IDependencyContainer>(); }
        }

        /// <summary>
        /// Holds enviroment services.
        /// </summary>
        public static EngineEnvironment Environment
        {
            get
            {
                if (environment == null)
                {
                    lock (environment)
                    {
                        if (environment == null)
                            environment = new EngineEnvironment();
                    }
                }
                return environment;
            }
        }
    }
}
