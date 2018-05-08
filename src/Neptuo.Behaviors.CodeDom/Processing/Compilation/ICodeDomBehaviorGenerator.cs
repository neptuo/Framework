using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Generates code for creating behavior instance.
    /// </summary>
    public interface ICodeDomBehaviorGenerator
    {
        /// <summary>
        /// Tries to generate expression for creating instance of <paramref name="behaviorType"/>.
        /// </summary>
        /// <param name="context">Code dom context.</param>
        /// <param name="behaviorType">Behavior type to create instance of.</param>
        /// <returns>Expression which returns instance of <paramref name="behaviorType"/>; <c>null</c> to execute next generator.</returns>
        CodeExpression TryGenerate(ICodeDomContext context, Type behaviorType);
    }
}
