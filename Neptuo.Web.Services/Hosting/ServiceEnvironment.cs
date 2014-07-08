using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting
{
    /// <summary>
    /// Framework environment.
    /// </summary>
    public static class ServiceEnvironment
    {
        private static object behaviorsLock = new object();
        private static IBehaviorCollection behaviors;

        private static object pipelineFactoryLock = new object();
        private static IPipelineFactory pipelineFactory;

        /// <summary>
        /// Collection of supported behaviors.
        /// </summary>
        public static IBehaviorCollection Behaviors
        {
            get
            {
                if (behaviors == null)
                {
                    lock (behaviorsLock)
                    {
                        behaviors = new BehaviorCollectionBase();
                    }
                }
                return behaviors;
            }
        }

        /// <summary>
        /// Factory for mapping requests to pipelines.
        /// </summary>
        public static IPipelineFactory PipelineFactory
        {
            get
            {
                if (pipelineFactory == null)
                {
                    lock (pipelineFactoryLock)
                    {
                        //TODO: Use pipeline factory.
                        pipelineFactory = null;
                    }
                }
                return pipelineFactory;
            }
        }
    }
}
