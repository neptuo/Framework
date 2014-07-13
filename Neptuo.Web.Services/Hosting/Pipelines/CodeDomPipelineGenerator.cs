using Neptuo.CodeDom.Compiler;
using Neptuo.Linq.Expressions;
using Neptuo.Web.Services.Hosting.Behaviors;
using Neptuo.Web.Services.Hosting.Http;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Web.Services.Hosting.Pipelines
{
    public class CodeDomPipelineGenerator
    {
        private const string resultListName = "result";

        private Type handlerType;

        /// <summary>
        /// Behavior collection.
        /// </summary>
        private IBehaviorCollection behaviorCollection;

        public CodeDomPipelineGenerator(Type handlerType, IBehaviorCollection behaviorCollection)
        {
            Guard.NotNull(handlerType, "handlerType");
            Guard.NotNull(behaviorCollection, "behaviorCollection");
            this.handlerType = handlerType;
            this.behaviorCollection = behaviorCollection;
        }

        public Type GeneratePipeline()
        {
            CodeCompileUnit unit = CreateUnit();
            CodeNamespace nameSpace = CreateNamespace();
            unit.Namespaces.Add(nameSpace);
            CodeTypeDeclaration type = CreateType();
            nameSpace.Types.Add(type);
            CodeMemberMethod method = CreateBehaviorMethod();
            type.Members.Add(method);
            GenerateBehaviorMethodBody(method);

            CsCodeDomCompiler compiler = new CsCodeDomCompiler();
            compiler.AddReferencedFolder(Environment.CurrentDirectory);
            CompilerResults result = compiler.CompileAssemblyFromUnit(unit, Path.Combine(Environment.CurrentDirectory, "xyz.dll"));
            Type pipelineType = result.CompiledAssembly.GetType(String.Format("{0}Pipeline", handlerType.Name));
            return pipelineType;

            //provider.CompileAssemblyFromDom(new CompilerParameters(), unit);

            //throw new NotImplementedException();
            //CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            //provider.CompileAssemblyFromDom(new CompilerParameters(), new CodeCompileUnit {  })
        }

        private CodeCompileUnit CreateUnit()
        {
            return new CodeCompileUnit();
        }

        private CodeNamespace CreateNamespace()
        {
            return new CodeNamespace();
        }

        private CodeTypeDeclaration CreateType()
        {
            CodeTypeDeclaration type = new CodeTypeDeclaration(String.Format("{0}Pipeline", handlerType.Name));

            if (handlerType.GetConstructor(new Type[0]) != null)
                type.BaseTypes.Add(typeof(DefaultPipelineBase<>).MakeGenericType(handlerType));
            else
                throw new NotSupportedException("Currently suport only parameterless constructors.");

            return type;
        }

        private CodeMemberMethod CreateBehaviorMethod()
        {
            CodeMemberMethod getBehaviorsMethod = new CodeMemberMethod();
            getBehaviorsMethod.Name = "GetBehaviors";
            getBehaviorsMethod.Attributes = MemberAttributes.Override | MemberAttributes.Family;
            getBehaviorsMethod.Parameters.Add(new CodeParameterDeclarationExpression(typeof(IHttpContext), "context"));
            getBehaviorsMethod.ReturnType = new CodeTypeReference(typeof(IEnumerable<>).MakeGenericType(typeof(IBehavior<>).MakeGenericType(handlerType)));
            return getBehaviorsMethod;
        }

        private void GenerateBehaviorMethodBody(CodeMemberMethod method)
        {
            Type resultListType = typeof(List<>).MakeGenericType(typeof(IBehavior<>).MakeGenericType(handlerType));
            method.Statements.Add(new CodeVariableDeclarationStatement(
                resultListType,
                resultListName, 
                new CodeObjectCreateExpression(resultListType)
            ));

            IEnumerable<Type> behaviorTypes = behaviorCollection.GetBehaviors(handlerType);
            foreach (Type behaviorType in behaviorTypes)
            {
                method.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression(resultListName),
                    TypeHelper.MethodName<IList<object>, object>(l => l.Add),
                    new CodeObjectCreateExpression(behaviorType)
                ));
            }

            method.Statements.Add(new CodeMethodReturnStatement(
                new CodeVariableReferenceExpression(resultListName)
            ));
        }
    }
}
