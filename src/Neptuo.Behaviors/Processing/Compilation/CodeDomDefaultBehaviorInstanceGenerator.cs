using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Default implementation of <see cref="ICodeDomBehaviorInstanceGenerator"/> for parameterless constructors.
    /// </summary>
    public class CodeDomDefaultBehaviorInstanceGenerator : ICodeDomBehaviorInstanceGenerator
    {
        public CodeExpression TryGenerate(ICodeDomContext context, Type behaviorType)
        {
            if (behaviorType.GetConstructor(new Type[0]) == null)
                return null;

            return new CodeObjectCreateExpression(behaviorType);
        }
    }
}
