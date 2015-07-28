using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors
{
    /// <summary>
    /// Default implementation of <see cref="IBehaviorContext"/>.
    /// </summary>
    /// <typeparam name="T">Type of handler for behaviors to operate over.</typeparam>
    public class DefaultBehaviorContext<T> : IBehaviorContext
    {
        private readonly IEnumerator<IBehavior<T>> behaviorEnumerator;

        public IKeyValueCollection CustomValues { get; private set; }

        /// <summary>
        /// Target handler.
        /// </summary>
        protected T Handler { get; private set; }

        /// <summary>
        /// Enumeration of current behaviors.
        /// </summary>
        protected IEnumerable<IBehavior<T>> Behaviors { get; private set; }

        /// <summary>
        /// Index of next behavior.
        /// </summary>
        protected int NextBehaviorIndex { get; private set; }

        /// <summary>
        /// Optional delegate, called when no more behaviors are available and NextAsync is called.
        /// </summary>
        protected Func<Task> OnNextAsyncWhenNoMoreBehaviors { get; private set; }

        public DefaultBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, IKeyValueCollection customValues)
            : this(behaviors, handler, customValues, 0)
        { }

        public DefaultBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, IKeyValueCollection customValues, int behaviorStartOffset)
        {
            Ensure.NotNull(behaviors, "behaviors");
            Ensure.NotNull(handler, "handler");
            Ensure.NotNull(customValues, "customValues");
            Ensure.PositiveOrZero(behaviorStartOffset, "behaviorStartOffset");
            Handler = handler;
            Behaviors = behaviors;
            CustomValues = customValues;
            this.behaviorEnumerator = behaviors.GetEnumerator();

            NextBehaviorIndex = behaviorStartOffset + 1;
            if (behaviorStartOffset > 0)
            {
                for (int i = 0; i < behaviorStartOffset; i++)
                {
                    if (!behaviorEnumerator.MoveNext())
                        break;
                }
            }
        }

        /// <summary>
        /// Sets optional delegate, called when no more behaviors are available and NextAsync is called.
        /// </summary>
        /// <param name="nextAsyncWhenNoMoreBehaviors">Delegate to be execute when no more behaviors are available.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultBehaviorContext<T> SetNextAsyncWhenNoMoreBehaviors(Func<Task> nextAsyncWhenNoMoreBehaviors)
        {
            OnNextAsyncWhenNoMoreBehaviors = nextAsyncWhenNoMoreBehaviors;
            return this;
        }

        public Task NextAsync()
        {
            // Move to next behavior.
            NextBehaviorIndex++;

            // Try to call next behavior in pipeline.
            if (behaviorEnumerator.MoveNext())
                return behaviorEnumerator.Current.ExecuteAsync(Handler, this);

            // No more behaviors.
            return NextAsyncWhenNoMoreBehaviors();
        }

        /// <summary>
        /// Called when <see cref="IBehaviorContext.NextAsync"/> is called, but no more behaviors are available.
        /// </summary>
        protected virtual Task NextAsyncWhenNoMoreBehaviors()
        {
            if (OnNextAsyncWhenNoMoreBehaviors != null)
                return OnNextAsyncWhenNoMoreBehaviors();

            return Task.FromResult(false);
        }

        public virtual IBehaviorContext Clone()
        {
            return new DefaultBehaviorContext<T>(
                Behaviors.ToList(), 
                Handler, 
                CustomValues, 
                NextBehaviorIndex - 1
            );
        }
    }
}
