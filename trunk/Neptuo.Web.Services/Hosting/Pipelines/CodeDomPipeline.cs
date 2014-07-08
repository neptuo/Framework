using Microsoft.CSharp;
using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines
{
    public class CodeDomPipeline<T> : IPipeline
    {
        private IPipeline generatedPipeline;

        private void GeneratePipeline(IHttpContext context)
        {
            Type handlerType = typeof(T);

            CodeTypeDeclaration type = new CodeTypeDeclaration(String.Format("{0}Pipeline", handlerType.Name));

            if (handlerType.GetConstructor(new Type[0]) != null)
                type.BaseTypes.Add(typeof(DefaultPipelineBase<>).MakeGenericType(handlerType));
            else
                throw new NotSupportedException("Currently suport only parameterless constructors.");



            CodeMemberMethod getBehaviorsMethod = new CodeMemberMethod();
            getBehaviorsMethod.Name = "GetBehaviors";
            getBehaviorsMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IHttpContext), "context"));
            getBehaviorsMethod.ReturnType = new CodeTypeReference(typeof(IEnumerable<>).MakeGenericType(typeof(IBehavior<>).MakeGenericType(handlerType)));
            type.Members.Add(getBehaviorsMethod);

            //CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            //provider.CompileAssemblyFromDom(new CompilerParameters(), new CodeCompileUnit {  })
        }

        private void EnsurePipeline(IHttpContext context)
        {
            if (generatedPipeline == null)
                GeneratePipeline(context);
        }

        public void Invoke(IHttpContext context)
        {
            EnsurePipeline(context);
            generatedPipeline.Invoke(context);
        }
    }
}
