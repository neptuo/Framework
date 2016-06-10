using Neptuo.Events;
using Neptuo.Models.Domains;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    /// <summary>
    /// Reflection based implementation of <see cref="IAggregateRootFactory{T}"/>.
    /// The <typeparamref name="T"/> must have constructor with <see cref="IKey"/> and <see cref="IEnumerable{IEvent}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the aggregate root.</typeparam>
    public class ReflectionAggregateRootFactory<T> : IAggregateRootFactory<T>
        where T : AggregateRoot
    {
        private readonly ConstructorInfo constructorInfo;
        private readonly List<ReflectionAggregateRootFactoryBuilder.Parameter> parameters;

        /// <summary>
        /// Creates new instance and checks constructor.
        /// </summary>
        public ReflectionAggregateRootFactory()
            : this(new ReflectionAggregateRootFactoryBuilder().AddKey().AddHistory())
        { }

        /// <summary>
        /// Creates new instance with explicitly defined parameters.
        /// </summary>
        /// <param name="builder">The constructor parameters builder.</param>
        public ReflectionAggregateRootFactory(ReflectionAggregateRootFactoryBuilder builder)
        {
            Ensure.NotNull(builder, "builder");
            parameters = builder.Parameters;

            List<Type> internalParameters = new List<Type>()
            {
                ReflectionAggregateRootFactoryBuilder.Parameter.KeyType,
                ReflectionAggregateRootFactoryBuilder.Parameter.HistoryType
            };

            foreach (ReflectionAggregateRootFactoryBuilder.Parameter parameter in parameters)
            {
                if (parameter.IsInternalParameter)
                    internalParameters.Remove(parameter.Type);
            }

            if(internalParameters.Count > 0)
                throw Ensure.Exception.InvalidOperation("Missing these required parameters of these types {0}.", String.Join(", ", internalParameters.Select(t => "'" + t.FullName + "'")));
            
            Type[] parameterTypes = builder.Parameters.Select(p => p.Type).ToArray();

            constructorInfo = typeof(T).GetConstructor(parameterTypes);
            if (constructorInfo == null)
                throw Ensure.Exception.InvalidOperation("Missing the constructor in '{0}' with parameters {1}.", typeof(T).FullName, String.Join(", ", parameterTypes.Select(t => "'" + t.FullName + "'")));
        }

        public T Create(IKey aggregateKey, IEnumerable<object> events)
        {
            Ensure.Condition.NotEmptyKey(aggregateKey, "aggregateKey");
            Ensure.NotNull(events, "events");

            object[] parameters = new object[this.parameters.Count];
            for (int i = 0; i < this.parameters.Count; i++)
			{
                ReflectionAggregateRootFactoryBuilder.Parameter parameter = this.parameters[i];
                if (parameter.IsInternalParameter)
                {
                    if (parameter.Type == ReflectionAggregateRootFactoryBuilder.Parameter.KeyType)
                        parameters[i] = aggregateKey;
                    else if (parameter.Type == ReflectionAggregateRootFactoryBuilder.Parameter.HistoryType)
                        parameters[i] = events;
                    else
                        throw Ensure.Exception.NotSupported("Not supported internal parameter of type '{0}'.", parameter.Type.FullName);
                }
                else
                {
                    parameters[i] = parameter.Value;
                }
            }

            return (T)constructorInfo.Invoke(parameters);
        }
    }
}
