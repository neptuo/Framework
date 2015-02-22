using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Registry for <see cref="ICodeDomBehaviorInstanceGenerator"/> by behavior type.
    /// </summary>
    public class CodeDomBehaviorInstanceRegistry : ICodeDomBehaviorInstanceGenerator
    {
        private readonly Dictionary<Type, ICodeDomBehaviorInstanceGenerator> storage = new Dictionary<Type, ICodeDomBehaviorInstanceGenerator>();
        private readonly FuncList<Type, ICodeDomBehaviorInstanceGenerator> onSearchBuilder = new FuncList<Type, ICodeDomBehaviorInstanceGenerator>(o => new CodeDomDefaultBehaviorInstanceGenerator());

        /// <summary>
        /// Maps <paramref name="behaviorType"/> to be processed by <paramref name="generator" />
        /// </summary>
        public CodeDomBehaviorInstanceRegistry AddGenerator(Type behaviorType, ICodeDomBehaviorInstanceGenerator generator)
        {
            Guard.NotNull(behaviorType, "behaviorType");
            Guard.NotNull(generator, "generator");
            storage[behaviorType] = generator;
            return this;
        }

        /// <summary>
        /// Adds <paramref name="searchHandler"/> to be executed when generator was not found.
        /// (Last registered is executed the first).
        /// </summary>
        /// <param name="searchHandler">Generator provider method.</param>
        public CodeDomBehaviorInstanceRegistry AddSearchHandler(Func<Type, ICodeDomBehaviorInstanceGenerator> searchHandler)
        {
            Guard.NotNull(searchHandler, "searchHandler");
            onSearchBuilder.Add(searchHandler);
            return this;
        }

        public CodeExpression TryGenerate(ICodeDomContext context, Type behaviorType)
        {
            ICodeDomBehaviorInstanceGenerator generator;
            if (!storage.TryGetValue(behaviorType, out generator))
                generator = onSearchBuilder.Execute(behaviorType);

            return generator.TryGenerate(context, behaviorType);
        }
    }
}
