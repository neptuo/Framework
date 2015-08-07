using Neptuo.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Services.Events.Internals
{
    internal class TypeResolver
    {
        private readonly Type contextType;

        public TypeResolver(Type contextType)
        {
            Ensure.NotNull(contextType, "contextType");
            this.contextType = contextType;
        }

        public TypeResolverResult Resolve(Type targetType)
        {
            Ensure.NotNull(targetType, "targetType");
            return new TypeResolverResult(contextType, targetType);
        }
    }

    internal class TypeResolverResult
    {
        private readonly Type contextType;
        private readonly Type targetType;

        public bool IsContext { get; private set; }
        public bool IsEnvelope { get; private set; }

        public Type DataType { get; private set; }

        public TypeResolverResult(Type contextType, Type targetType)
        {
            Ensure.NotNull(contextType, "contextType");
            Ensure.NotNull(targetType, "targetType");
            this.contextType = contextType;
            this.targetType = targetType;

            if (targetType.IsGenericType)
            {
                Type genericType = targetType.GetGenericTypeDefinition();
                IsContext = contextType.IsAssignableFrom(genericType);
                IsEnvelope = typeof(Envelope<>).IsAssignableFrom(genericType);
                DataType = targetType.GetGenericArguments().First();
            }
            else
            {
                DataType = targetType;
            }
        }
    }
}
