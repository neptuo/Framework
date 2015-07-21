using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators.Executors
{
    /// <summary>
    /// Default implementation of <see cref="ITypeDelegateCollection{TContext}"/>.
    /// </summary>
    /// <typeparam name="TContext">Type of context information.</typeparam>
    public class FilterTypeExecutor<TContext> : ITypeDelegateCollection<TContext>, ITypeExecutor<TContext>
    {
        private readonly List<Func<Type, TContext, bool>> filters = new List<Func<Type, TContext, bool>>();
        private readonly List<Action<Type, TContext>> handlers = new List<Action<Type, TContext>>();

        public ITypeDelegateCollection<TContext> AddFilter(Func<Type, TContext, bool> filter)
        {
            Ensure.NotNull(filter, "filter");
            filters.Add(filter);
            return this;
        }

        public ITypeDelegateCollection<TContext> AddHandler(Action<Type, TContext> handler)
        {
            Ensure.NotNull(handler, "handler");
            handlers.Add(handler);
            return this;
        }

        public bool IsMatched(Type type, TContext context)
        {
            Ensure.NotNull(type, "type");
            Ensure.NotNull(context, "context");
            return filters.All(f => f(type, context));
        }

        public virtual void Handle(Type type, TContext context)
        {
            if (IsMatched(type, context))
            {
                foreach (Action<Type, TContext> handler in handlers)
                    handler(type, context);
            }
        }
    }

    /// <summary>
    /// Default implementation of <see cref="ITypeDelegateCollection"/>.
    /// </summary>
    public class FilterTypeExecutor : ITypeDelegateCollection, ITypeExecutor
    {
        private readonly List<Func<Type, bool>> filters = new List<Func<Type, bool>>();
        private readonly List<Action<Type>> handlers = new List<Action<Type>>();

        public ITypeDelegateCollection AddFilter(Func<Type, bool> filter)
        {
            Ensure.NotNull(filter, "filter");
            filters.Add(filter);
            return this;
        }

        public ITypeDelegateCollection AddHandler(Action<Type> handler)
        {
            Ensure.NotNull(handler, "handler");
            handlers.Add(handler);
            return this;
        }

        public bool IsMatched(Type type)
        {
            Ensure.NotNull(type, "type");
            return filters.All(f => f(type));
        }

        public virtual void Handle(Type type)
        {
            if (IsMatched(type))
            {
                foreach (Action<Type> handler in handlers)
                    handler(type);
            }
        }
    }
}
