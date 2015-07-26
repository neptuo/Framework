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
    public class CodeDomBehaviorInstanceGeneratorCollection : ICodeDomBehaviorInstanceGenerator
    {
        private readonly object storageLock = new object();
        private readonly object searchHandlerLock = new object();
        private readonly Dictionary<Type, ICodeDomBehaviorInstanceGenerator> storage = new Dictionary<Type, ICodeDomBehaviorInstanceGenerator>();
        private readonly FuncList<Type, ICodeDomBehaviorInstanceGenerator> onSearchBuilder = new FuncList<Type, ICodeDomBehaviorInstanceGenerator>(o => new CodeDomDefaultBehaviorInstanceGenerator());

        /// <summary>
        /// Maps <paramref name="behaviorType"/> to be processed by <paramref name="generator" />
        /// </summary>
        public CodeDomBehaviorInstanceGeneratorCollection Add(Type behaviorType, ICodeDomBehaviorInstanceGenerator generator)
        {
            Ensure.NotNull(behaviorType, "behaviorType");
            Ensure.NotNull(generator, "generator");

            lock (storageLock)
                storage[behaviorType] = generator;

            return this;
        }

        /// <summary>
        /// Adds <paramref name="searchHandler"/> to be executed when generator was not found.
        /// (Last registered is executed the first).
        /// </summary>
        /// <param name="searchHandler">Generator provider method.</param>
        public CodeDomBehaviorInstanceGeneratorCollection AddSearchHandler(Func<Type, ICodeDomBehaviorInstanceGenerator> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");

            lock (searchHandlerLock)
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
