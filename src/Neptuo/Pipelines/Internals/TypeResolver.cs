using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Pipelines.Internals
{
    internal class TypeResolver
    {
        private readonly Type contextType;

        public TypeResolver(Type contextType)
        {
            Guard.NotNull(contextType, "contextType");
            this.contextType = contextType;
        }

        public TypeResolverResult Resolve(Type targetType)
        {
            Guard.NotNull(targetType, "targetType");
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
            Guard.NotNull(contextType, "contextType");
            Guard.NotNull(targetType, "targetType");
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
