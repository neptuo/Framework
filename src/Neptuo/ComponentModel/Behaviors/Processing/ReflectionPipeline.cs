using Neptuo;
using Neptuo.ComponentModel.Behaviors;
using Neptuo.ComponentModel.Behaviors.Processing;
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
        /// <returns>Enumeration of haviors for <typeparamref name="T"/>.</returns>
        protected override IEnumerable<IBehavior<T>> GetBehaviors()
        {
            IEnumerable<Type> behaviorTypes = collection.GetBehaviors(typeof(T));
            foreach (Type behaviorType in behaviorTypes)
                yield return (IBehavior<T>)Activator.CreateInstance(behaviorType);
        }
    }
}
