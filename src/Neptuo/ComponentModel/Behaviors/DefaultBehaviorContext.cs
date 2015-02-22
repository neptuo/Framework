using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors
{
    /// <summary>
    /// Default implementation of <see cref="IBehaviorContext"/>.
    /// </summary>
    /// <typeparam name="T">Type of handler for behaviors to operate over.</typeparam>
    public class DefaultBehaviorContext<T> : IBehaviorContext
    {
        private readonly IEnumerable<IBehavior<T>> behaviors;
        private readonly IEnumerator<IBehavior<T>> behaviorEnumerator;
        private readonly T handler;
        private IKeyValueCollection customValues;

        public IKeyValueCollection CustomValues
        {
            get
            {
                if (customValues != null)
                    customValues = new KeyValueCollection();

                return customValues;
            }
        }

        public DefaultBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler)
            : this(behaviors, handler, 0)
        { }

        public DefaultBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, int behaviorStartOffset)
        {
            Guard.NotNull(behaviors, "behaviors");
            Guard.NotNull(handler, "handler");
            Guard.PositiveOrZero(behaviorStartOffset, "behaviorStartOffset");
            this.behaviors = behaviors;
            this.behaviorEnumerator = behaviors.GetEnumerator();
            this.handler = handler;

            if (behaviorStartOffset > 0)
            {
                for (int i = 0; i < behaviorStartOffset; i++)
                {
                    if (!behaviorEnumerator.MoveNext())
                        break;
                }
            }
        }

        public Task NextAsync()
        {
            // Try to call next behavior in pipeline.
            if (behaviorEnumerator.MoveNext())
                return behaviorEnumerator.Current.ExecuteAsync(handler, this);

            // No more behaviors equal to inability process request this way.
            return Task.FromResult(false);
        }

        public IBehaviorContext Clone()
        {
            return new DefaultBehaviorContext<T>(new List<IBehavior<T>>(behaviors), handler);
        }
    }
}
