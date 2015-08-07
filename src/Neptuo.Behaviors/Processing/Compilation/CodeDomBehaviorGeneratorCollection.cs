using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Collection of <see cref="ICodeDomBehaviorGenerator"/> by behavior type.
    /// </summary>
    public class CodeDomBehaviorGeneratorCollection : ICodeDomBehaviorGenerator
    {
        private readonly object storageLock = new object();
        private readonly object searchHandlerLock = new object();
        private readonly Dictionary<Type, ICodeDomBehaviorGenerator> storage = new Dictionary<Type, ICodeDomBehaviorGenerator>();
        private readonly OutFuncCollection<Type, ICodeDomBehaviorGenerator, bool> onSearchGenerator = new OutFuncCollection<Type, ICodeDomBehaviorGenerator, bool>(TryGetDefaultGenerator);

        /// <summary>
        /// Maps <paramref name="behaviorType"/> to be processed by <paramref name="generator" />
        /// </summary>
        public CodeDomBehaviorGeneratorCollection Add(Type behaviorType, ICodeDomBehaviorGenerator generator)
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
        public CodeDomBehaviorGeneratorCollection AddSearchHandler(OutFunc<Type, ICodeDomBehaviorGenerator, bool> searchHandler)
        {
            Ensure.NotNull(searchHandler, "searchHandler");

            lock (searchHandlerLock)
                onSearchGenerator.Add(searchHandler);

            return this;
        }

        public CodeExpression TryGenerate(ICodeDomContext context, Type behaviorType)
        {
            ICodeDomBehaviorGenerator generator;
            if (!storage.TryGetValue(behaviorType, out generator))
                onSearchGenerator.TryExecute(behaviorType, out generator);

            return generator.TryGenerate(context, behaviorType);
        }

        private static bool TryGetDefaultGenerator(Type behaviorType, out ICodeDomBehaviorGenerator generator)
        {
            generator = new DefaultCodeDomBehaviorGenerator();
            return true;
        }
    }
}
