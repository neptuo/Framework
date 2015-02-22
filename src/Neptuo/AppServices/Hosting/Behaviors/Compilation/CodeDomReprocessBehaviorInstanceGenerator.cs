using Neptuo.ComponentModel.Behaviors.Processing.Compilation;
using Neptuo.AppServices.Behaviors;
using Neptuo.AppServices.Behaviors.Hosting;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.AppServices.Hosting.Behaviors.Compilation
{
    public class CodeDomReprocessBehaviorInstanceGenerator : ICodeDomBehaviorInstanceGenerator
    {
        public CodeExpression TryGenerate(ICodeDomContext context, Type behaviorType)
        {
            if (behaviorType != typeof(ReprocessBehavior))
                return null;

            ReprocessAttribute attribute = context.HandlerType.GetCustomAttribute<ReprocessAttribute>();
            if(attribute == null)
                return null;

            return new CodeObjectCreateExpression(
                new CodeTypeReference(typeof(ReprocessBehavior)),
                new CodePrimitiveExpression(attribute.Count)
            );
        }
    }
}
