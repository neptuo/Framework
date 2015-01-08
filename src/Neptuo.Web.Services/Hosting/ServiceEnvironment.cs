using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http.MediaTypes;
using Neptuo.Web.Services.Hosting.Pipelines;
using Neptuo.Web.Services.Hosting.Pipelines.Compilation;
using Neptuo.Web.Services.Hosting.Routing;
using System;
using System.Collections.Generic;
using System.IO;
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
        #region Support objects

        private static object behaviorsLock = new object();
        private static IBehaviorCollection behaviors;

        private static object routeTableLock = new object();
        private static IRouteTable routeTable;

        private static object mediaTypesLock = new object();
        private static IMediaTypeCollection mediaTypes;

        private static object codeDomLock = new object();
        private static CodeDomPipelineConfiguration codeDom;

        #endregion

        #region Singletons

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
                        if (behaviors == null)
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
                        if (routeTable == null)
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
                    lock (mediaTypesLock)
                    {
                        if (mediaTypes == null)
                            mediaTypes = new MediaTypeCollectionBase();
                    }
                }
                return mediaTypes;
            }
        }

        /// <summary>
        /// Contains configuration for code dom pipeline factories.
        /// </summary>
        public static CodeDomPipelineConfiguration CodeDom
        {
            get
            {
                if (codeDom == null)
                {
                    lock (codeDomLock)
                    {
                        if (codeDom == null)
                            codeDom = new CodeDomPipelineConfiguration(Path.GetTempPath());
                    }
                }
                return codeDom;
            }
        }

        #endregion

        #region Set methods

        /// <summary>
        /// Sets <see cref="ServiceEnvironment.Behaviors"/>.
        /// Must be called before any code acesses this collection.
        /// </summary>
        /// <param name="behaviors">New value for <see cref="ServiceEnvironment.Behaviors"/>.</param>
        public static void WithBehaviors(IBehaviorCollection behaviors)
        {
            Guard.NotNull(behaviors, "behaviors");
            if (ServiceEnvironment.behaviors != null)
                throw new InvalidOperationException("Behaviors collection must be provided before auto-singleton is created.");

            lock (behaviorsLock)
            {
                ServiceEnvironment.behaviors = behaviors;
            }
        }

        /// <summary>
        /// Sets <see cref="ServiceEnvironment.RouteTable"/>.
        /// Must be called before any code acesses this collection.
        /// </summary>
        /// <param name="routeTable">New value for <see cref="ServiceEnvironment.RouteTable"/>.</param>
        public static void WithRouteTable(IRouteTable routeTable)
        {
            Guard.NotNull(routeTable, "routeTable");
            if (ServiceEnvironment.routeTable != null)
                throw new InvalidOperationException("RouteTable collection must be provided before auto-singleton is created.");

            lock (routeTableLock)
            {
                ServiceEnvironment.routeTable = routeTable;
            }
        }

        /// <summary>
        /// Sets <see cref="ServiceEnvironment.MediaTypes"/>.
        /// Must be called before any code acesses this collection.
        /// </summary>
        /// <param name="mediaTypes">New value for <see cref="ServiceEnvironment.MediaTypes"/>.</param>
        public static void WithMediaTypes(IMediaTypeCollection mediaTypes)
        {
            Guard.NotNull(mediaTypes, "mediaTypes");
            if (ServiceEnvironment.mediaTypes != null)
                throw new InvalidOperationException("MediaTypes collection must be provided before auto-singleton is created.");

            lock (mediaTypesLock)
            {
                ServiceEnvironment.mediaTypes = mediaTypes;
            }
        }

        /// <summary>
        /// Sets <see cref="ServiceEnvironment.CodeDom"/>.
        /// Must be called before any code acesses this collection.
        /// </summary>
        /// <param name="codeDom">New value for <see cref="ServiceEnvironment.CodeDom"/>.</param>
        public static void WithCodeDom(CodeDomPipelineConfiguration codeDom)
        {
            Guard.NotNull(codeDom, "codeDom");
            if (ServiceEnvironment.codeDom != null)
                throw new InvalidOperationException("codeDom collection must be provided before auto-singleton is created.");

            lock (codeDomLock)
            {
                ServiceEnvironment.codeDom = codeDom;
            }
        }

        #endregion
    }
}