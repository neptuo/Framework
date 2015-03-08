using Neptuo;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing;
using Neptuo.ComponentModel.Behaviors.Processing.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing
{
    /// <summary>
    /// Creates behaviors using <see cref="Activator"/>.
    /// Handler must have parameterless construtor.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public class DefaultPipeline<T> : DefaultPipelineBase<T>
        where T : new()
    {
        private readonly IBehaviorCollection collection;
        private readonly IReflectionBehaviorInstanceProvider behaviorInstance;
        
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="collection">Behavior collection.</param>
        /// <param name="behaviorInstance">Behavior instance provider.</param>
        public DefaultPipeline(IBehaviorCollection collection, IReflectionBehaviorInstanceProvider behaviorInstance)
        {
            Ensure.NotNull(collection, "collection");
            Ensure.NotNull(behaviorInstance, "behaviorInstance");
            this.collection = collection;
            this.behaviorInstance = behaviorInstance;
        }

        /// <summary>
        /// Creates behaviors using <see cref="Activator"/>.
        /// Returns enumeration of haviors for <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Enumeration of haviors for <typeparamref name="T"/>.</returns>
        protected override IEnumerable<IBehavior<T>> GetBehaviors()
        {
            IReflectionContext context = new DefaultReflectionContext(typeof(T));
            IEnumerable<Type> behaviorTypes = collection.GetBehaviors(typeof(T));
            foreach (Type behaviorType in behaviorTypes)
                yield return (IBehavior<T>)behaviorInstance.TryProvide(context, behaviorType);
        }
    }
}
