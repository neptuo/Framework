using Neptuo;
using Neptuo.Web.Services.Hosting;
using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using Neptuo.Web.Services.Hosting.Pipelines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines
{
    /// <summary>
    /// Creates behaviors using <see cref="Activator"/>.
    /// Handler must have parameterless construtor.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public class ReflectionPipeline<T> : DefaultPipelineBase<T>
        where T : new()
    {
        /// <summary>
        /// Behavior collection.
        /// </summary>
        private IBehaviorCollection collection;

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="collection">Behavior collection.</param>
        public ReflectionPipeline(IBehaviorCollection collection)
        {
            Guard.NotNull(collection, "collection");
            this.collection = collection;
        }

        /// <summary>
        /// Creates behaviors using <see cref="Activator"/>.
        /// Returns enumeration of haviors for <typeparamref name="T"/>.
        /// </summary>
        /// <param name="context">Current Http context.</param>
        /// <returns>Enumeration of haviors for <typeparamref name="T"/>.</returns>
        protected override IEnumerable<IBehavior<T>> GetBehaviors(IHttpContext context)
        {
            IEnumerable<Type> behaviorTypes = collection.GetBehaviors(typeof(T));
            foreach (Type behaviorType in behaviorTypes)
                yield return (IBehavior<T>)Activator.CreateInstance(behaviorType);
        }
    }
}
