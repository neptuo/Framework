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
        private readonly IEnumerator<IBehavior<T>> behaviorEnumerator;
        private IKeyValueCollection customValues;

        public IKeyValueCollection CustomValues
        {
            get
            {
                if (customValues == null)
                    customValues = new KeyValueCollection();

                return customValues;
            }
            set { customValues = value; }
        }

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

        public DefaultBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler)
            : this(behaviors, handler, 0)
        { }

        public DefaultBehaviorContext(IEnumerable<IBehavior<T>> behaviors, T handler, int behaviorStartOffset)
        {
            Guard.NotNull(behaviors, "behaviors");
            Guard.NotNull(handler, "handler");
            Guard.PositiveOrZero(behaviorStartOffset, "behaviorStartOffset");
            Handler = handler;
            Behaviors = behaviors;
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
        /// Sets collection of custom values.
        /// If <paramref name="customValues"/> is <c>null</c>, new (empty) collection is created.
        /// </summary>
        /// <param name="customValues">New custom values collection.</param>
        /// <returns>Self (for fluency).</returns>
        public DefaultBehaviorContext<T> SetCustomValues(IKeyValueCollection customValues)
        {
            this.customValues = customValues;
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
        /// Called when <see cref="IBehaviorContext.NextAsync"/> is called, but non behavior exists.
        /// </summary>
        protected virtual Task NextAsyncWhenNoMoreBehaviors()
        {
            return Task.FromResult(false);
        }

        public virtual IBehaviorContext Clone()
        {
            return new DefaultBehaviorContext<T>(Behaviors.ToList(), Handler, NextBehaviorIndex - 1);
        }
    }
}
