using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using Neptuo.Web.Services.Hosting.Pipelines;
using Neptuo.Web.Services.Hosting.Routing;
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

        private static object routeTableLock = new object();
        private static IRouteTable routeTable;

        private static object mediaTypesLock = new object();
        private static IMediaTypeCollection mediaTypes;

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
        /// Maps requests to pipelines.
        /// </summary>
        public static IRouteTable RouteTable
        {
            get
            {
                if (routeTable == null)
                {
                    lock (routeTableLock)
                    {
                        routeTable = new RouteTableBase();
                    }
                }
                return routeTable;
            }
        }

        /// <summary>
        /// Contains registered media type serializers and deserializers.
        /// </summary>
        public static IMediaTypeCollection MediaTypes
        {
            get
            {
                if(mediaTypes == null)
                {
                    lock (mediaTypes)
                    {
                        mediaTypes = new MediaTypeCollectionBase();
                    }
                }
                return mediaTypes;
            }
        }
    }
}