using Neptuo;
using Neptuo.Behaviors;
using Neptuo.Behaviors.Processing;
using Neptuo.Behaviors.Processing.Reflection;
using Neptuo.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing
{
    /// <summary>
    /// Creates behaviors using <see cref="Activator"/>.
    /// Handler must have parameterless construtor.
    /// </summary>
    /// <typeparam name="T">Type of handler.</typeparam>
    public class ReflectionDefaultPipeline<T> : DefaultPipelineBase<T>
        where T : new()
    {
        private readonly IBehaviorProvider behaviors;
        private readonly IReflectionBehaviorFactory behaviorFactory;
        
        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="behaviors">Behavior provider.</param>
        /// <param name="behaviorFactory">Behavior instance provider.</param>
        public ReflectionDefaultPipeline(IBehaviorProvider behaviors, IReflectionBehaviorFactory behaviorFactory)
        {
            Ensure.NotNull(behaviors, "behaviors");
            Ensure.NotNull(behaviorFactory, "behaviorFactory");
            this.behaviors = behaviors;
            this.behaviorFactory = behaviorFactory;
        }

        /// <summary>
        /// Creates behaviors using <see cref="Activator"/>.
        /// Returns enumeration of haviors for <typeparamref name="T"/>.
        /// </summary>
        /// <returns>Enumeration of haviors for <typeparamref name="T"/>.</returns>
        protected override IEnumerable<IBehavior<T>> GetBehaviors()
        {
            IReflectionContext context = new DefaultReflectionContext(typeof(T));
            IEnumerable<Type> behaviorTypes = behaviors.GetBehaviors(typeof(T));
            foreach (Type behaviorType in behaviorTypes)
                yield return (IBehavior<T>)behaviorFactory.TryCreate(context, behaviorType);
        }
    }
}
