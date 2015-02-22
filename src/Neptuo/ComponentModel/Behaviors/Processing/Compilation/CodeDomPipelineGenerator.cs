using Neptuo.CodeDom.Compiler;
using Neptuo.Linq.Expressions;
using Neptuo.ComponentModel.Behaviors;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Neptuo.Compilers;
using Neptuo.Reflection;

namespace Neptuo.ComponentModel.Behaviors.Processing.Compilation
{
    /// <summary>
    /// CodeDom generator for pipelines based on concrete handlert type.
    /// </summary>
    public class CodeDomPipelineGenerator
    {
        /// <summary>
        /// Name of variable containing behaviors in GetBehavior method of PipelineBase.
        /// </summary>
        private const string resultListName = "result";

        /// <summary>
        /// Target handler type.
        /// </summary>
        private Type handlerType;

        /// <summary>
        /// Behavior collection.
        /// </summary>
        private IBehaviorCollection behaviorCollection;

        /// <summary>
        /// Factory for code compilers.
        /// </summary>
        private CompilerFactory compilerFactory;

        /// <summary>
        /// Directory for storing temp compilation files.
        /// </summary>
        private readonly string tempDirectory;

        /// <summary>
        /// Base type generated pipeline.
        /// </summary>
        private readonly Type baseType;

        /// <summary>
        /// Creates new instance for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Target handler type.</param>
        /// <param name="behaviorCollection">Behavior collection.</param>
        public CodeDomPipelineGenerator(Type handlerType, IBehaviorCollection behaviorCollection, CodeDomPipelineConfiguration configuration)
        {
            Guard.NotNull(handlerType, "handlerType");
            Guard.NotNull(behaviorCollection, "behaviorCollection");
            Guard.NotNull(configuration, "configuration");
            this.handlerType = handlerType;
            this.behaviorCollection = behaviorCollection;
            this.compilerFactory = new CompilerFactory(configuration);
            this.tempDirectory = configuration.TempDirectory;
            this.baseType = configuration.BaseType;
        }

        /// <summary>
        /// Generates dynamicly pipeline type for handler.
        /// </summary>
        /// <returns></returns>
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

            Assembly assembly = CompileCodeUnit(unit);
            Type pipelineType = assembly.GetType(FormatPipelineTypeName());
            return pipelineType;
        }

        /// <summary>
        /// Creates code compile unit.
        /// </summary>
        /// <returns>Code compile unit.</returns>
        private CodeCompileUnit CreateUnit()
        {
            return new CodeCompileUnit();
        }

        /// <summary>
        /// Creates code namespace for pipeline type.
        /// </summary>
        /// <returns>Code namespace for pipeline type.</returns>
        private CodeNamespace CreateNamespace()
        {
            return new CodeNamespace();
        }

        /// <summary>
        /// Creates empty pipeline type base on <see cref="DefaultPipelineBase<>"/>.
        /// </summary>
        /// <returns>Empty pipeline type.</returns>
        private CodeTypeDeclaration CreateType()
        {
            CodeTypeDeclaration type = new CodeTypeDeclaration(FormatPipelineTypeName());

            if (handlerType.GetConstructor(new Type[0]) != null)
                type.BaseTypes.Add(baseType.MakeGenericType(handlerType));
            else
                throw new NotSupportedException("Currently supported only parameterless behavior constructors.");

            return type;
        }

        /// <summary>
        /// Creates empty GetBehaviors method of <see cref="PipelineBase<>"/>.
        /// </summary>
        /// <returns></returns>
        private CodeMemberMethod CreateBehaviorMethod()
        {
            CodeMemberMethod getBehaviorsMethod = new CodeMemberMethod();
            getBehaviorsMethod.Name = "GetBehaviors";
            getBehaviorsMethod.Attributes = MemberAttributes.Override | MemberAttributes.Family;
            getBehaviorsMethod.ReturnType = new CodeTypeReference(typeof(IEnumerable<>).MakeGenericType(typeof(IBehavior<>).MakeGenericType(handlerType)));
            return getBehaviorsMethod;
        }

        /// <summary>
        /// Fills GetBehaviors method body by creating collection of registered <see cref="IBehavior<>"/>.
        /// </summary>
        /// <param name="method">GetBehaviors empty method.</param>
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

        /// <summary>
        /// Compiles <paramref name="unit"/> and returns generated assembly.
        /// </summary>
        /// <param name="unit">Source code compile unit.</param>
        /// <returns>Generated assembly.</returns>
        private Assembly CompileCodeUnit(CodeCompileUnit unit)
        {
            IStaticCompiler compiler = compilerFactory.CreateStatic();

            string assemblyFilePath = Path.Combine(tempDirectory, FormatAssemblyFileName());
            ICompilerResult result = compiler.FromUnit(unit, assemblyFilePath);
            if (!result.IsSuccess)
            {
                // Save source code if compilation was not successfull.

                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                string sourceCodePath = Path.Combine(tempDirectory, FormatSourceCodeFileName());

                using (StreamWriter writer = new StreamWriter(sourceCodePath))
                {
                    provider.GenerateCodeFromCompileUnit(unit, writer, new CodeGeneratorOptions());
                }

                throw new PipelineFactoryException(String.Format("Error during compilation of generated pipeline, source code saved to '{0}'.", sourceCodePath));
            }

            // Load compiled assembly.
            return ReflectionFactory.FromCurrentAppDomain().LoadAssembly(assemblyFilePath);
        }

        /// <summary>
        /// Formats pipeline type based on name of handler type.
        /// </summary>
        /// <returns>Pipeline name.</returns>
        private string FormatPipelineTypeName()
        {
            return String.Format("{0}Pipeline", handlerType.Name);
        }

        /// <summary>
        /// Formats name for generated assembly (only file name with extension).
        /// </summary>
        /// <returns>Name for generated assembly.</returns>
        private string FormatAssemblyFileName()
        {
            return String.Format("{0}.dll", handlerType.FullName);
        }

        /// <summary>
        /// Formats name for generated source code (only file name with extension).
        /// </summary>
        /// <returns>Name for generated assesource codembly.</returns>
        private string FormatSourceCodeFileName()
        {
            return String.Format("{0}.cs", handlerType.FullName);
        }
    }
}
