using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Uses parameter-less constructor to create instances of behaviors.
    /// Never returns <c>null</c>.
    /// </summary>
    public class DefaultCodeDomBehaviorInstanceGenerator : ICodeDomBehaviorInstanceGenerator
    {
        private readonly ICodeDomBehaviorInstanceGenerator generator;

        /// <summary>
        /// Creates new instance that always calls default (parameter-less) constructor.
        /// </summary>
        public DefaultCodeDomBehaviorInstanceGenerator()
        { }

        /// <summary>
        /// Creates new instance that first checks <paramref name="generator"/> and if it returns <c>null</c>,
        /// uses default (parameter-less) constructor.
        /// </summary>
        /// <param name="generator">Inner generator used first.</param>
        public DefaultCodeDomBehaviorInstanceGenerator(ICodeDomBehaviorInstanceGenerator generator)
        {
            Ensure.NotNull(generator, "generator");
            this.generator = generator;
        }

        public CodeExpression TryGenerate(ICodeDomContext context, Type behaviorType)
        {
            if (generator != null)
            {
                CodeExpression result = generator.TryGenerate(context, behaviorType);
                if (result != null)
                    return result;
            }

            return new CodeObjectCreateExpression(behaviorType);
        }
    }
}
