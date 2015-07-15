﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Reflections.Enumerators
{
    /// <summary>
    /// Default implementation of <see cref="ITypeEnumerator{TContext}"/>.
    /// </summary>
    /// <typeparam name="TContext">Type of context information.</typeparam>
    public class DefaultTypeEnumerator<TContext> : ITypeEnumerator<TContext>
    {
        private readonly List<Func<Type, TContext, bool>> filters = new List<Func<Type, TContext, bool>>();
        private readonly List<Action<Type, TContext>> handlers = new List<Action<Type, TContext>>();

        public ITypeEnumerator<TContext> AddFilter(Func<Type, TContext, bool> filter)
        {
            Ensure.NotNull(filter, "filter");
            filters.Add(filter);
            return this;
        }

        public ITypeEnumerator<TContext> AddHandler(Action<Type, TContext> handler)
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

        public void Handle(Type type, TContext context)
        {
            if(IsMatched(type, context))
            {
                foreach (Action<Type, TContext> handler in handlers)
                    handler(type, context);
            }
        }
    }
}
