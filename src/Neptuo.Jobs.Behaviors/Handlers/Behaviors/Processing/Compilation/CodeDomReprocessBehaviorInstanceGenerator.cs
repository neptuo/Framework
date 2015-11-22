using Neptuo.Behaviors.Processing.Compilation;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Jobs.Handlers.Behaviors.Processing.Compilation
{
    /// <summary>
    /// Code generator for <see cref="ReprocessAttribute"/> and <see cref="ReprocessBehavior"/>.
    /// </summary>
    public class CodeDomReprocessBehaviorInstanceGenerator : ICodeDomBehaviorGenerator
    {
        public CodeExpression TryGenerate(ICodeDomContext context, Type behaviorType)
        {
            if (behaviorType != typeof(ReprocessBehavior))
                return null;

            ReprocessAttribute attribute = context.HandlerType.GetCustomAttribute<ReprocessAttribute>();
            if(attribute == null)
                return null;

            double delay = attribute.DelayBeforeReprocess.TotalMilliseconds;
            if (delay > 0)
            {
                return new CodeObjectCreateExpression(
                    new CodeTypeReference(typeof(ReprocessBehavior)),
                    new CodePrimitiveExpression(attribute.Count),
                    new CodeMethodInvokeExpression(
                        new CodeTypeReferenceExpression(typeof(TimeSpan)),
                        "FromMilliseconds",
                        new CodePrimitiveExpression(delay)
                    )
                );
            }

            return new CodeObjectCreateExpression(
                new CodeTypeReference(typeof(ReprocessBehavior)),
                new CodePrimitiveExpression(attribute.Count)
            );
        }
    }
}
