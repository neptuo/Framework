using Neptuo.Behaviors.Providers;
using Neptuo.Compilers;
using Neptuo.Linq.Expressions;
using Neptuo.Reflections;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation.Internals
{
    /// <summary>
    /// CodeDom generator for pipelines based on concrete handlert type.
    /// </summary>
    internal class CodeDomPipelineGenerator
    {
        /// <summary>
        /// Name of variable containing behaviors in GetBehavior method of <see cref="PipelineBase{T}"/>.
        /// </summary>
        private const string resultListName = "result";

        /// <summary>
        /// Name of method from <see cref="PipelineBase{T}"/> to create behavior instances.
        /// </summary>
        private static readonly string createBehaviorInstancesMethodName = "CreateBehaviorsInternal";
        
        private readonly Type handlerType;
        private readonly IBehaviorProvider behaviorProvider;
        private readonly CompilerFactory compilerFactory;
        private readonly ICompilerConfiguration configuration;
        private readonly CodeDomNameFormatter nameFormatter;

        /// <summary>
        /// Creates new instance for <paramref name="handlerType"/>.
        /// </summary>
        /// <param name="handlerType">Target handler type.</param>
        /// <param name="behaviorProvider">Behavior collection.</param>
        /// <param name="configuration">Generator configuration.</param>
        public CodeDomPipelineGenerator(Type handlerType, IBehaviorProvider behaviorProvider, ICompilerConfiguration configuration)
        {
            Ensure.NotNull(handlerType, "handlerType");
            Ensure.NotNull(behaviorProvider, "behaviorProvider");
            Ensure.NotNull(configuration, "configuration");
            this.handlerType = handlerType;
            this.behaviorProvider = behaviorProvider;
            this.compilerFactory = new CompilerFactory(configuration);
            this.configuration = configuration;
            this.nameFormatter = new CodeDomNameFormatter(handlerType);
        }

        /// <summary>
        /// Generates pipeline type for inner handler.
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
            Type pipelineType = assembly.GetType(nameFormatter.FormatPipelineTypeName());
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
        /// Creates empty pipeline type based on <see cref="DefaultPipelineBase{T}"/>.
        /// </summary>
        /// <returns>Empty pipeline type.</returns>
        private CodeTypeDeclaration CreateType()
        {
            CodeTypeDeclaration type = new CodeTypeDeclaration(nameFormatter.FormatPipelineTypeName());
            type.BaseTypes.Add(typeof(PipelineBase<>).MakeGenericType(handlerType));
            return type;
        }

        /// <summary>
        /// Creates empty GetBehaviors method of <see cref="PipelineBase<>"/>.
        /// </summary>
        /// <returns></returns>
        private CodeMemberMethod CreateBehaviorMethod()
        {
            CodeMemberMethod getBehaviorsMethod = new CodeMemberMethod();
            getBehaviorsMethod.Name = createBehaviorInstancesMethodName;
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

            IEnumerable<Type> behaviorTypes = behaviorProvider.GetBehaviors(handlerType);
            ICodeDomContext context = new DefaultCodeDomContext(configuration, handlerType);
            ICodeDomBehaviorGenerator behaviorGenerator = GetBehaviorInstanceGenerator();

            foreach (Type behaviorType in behaviorTypes)
            {
                method.Statements.Add(new CodeMethodInvokeExpression(
                    new CodeVariableReferenceExpression(resultListName),
                    TypeHelper.MethodName<IList<object>, object>(l => l.Add),
                    behaviorGenerator.TryGenerate(context, behaviorType)
                ));
            }

            method.Statements.Add(new CodeMethodReturnStatement(
                new CodeVariableReferenceExpression(resultListName)
            ));
        }

        /// <summary>
        /// Returns behavior instance generator.
        /// </summary>
        /// <returns>Behavior instance generator.</returns>
        private ICodeDomBehaviorGenerator GetBehaviorInstanceGenerator()
        {
            ICodeDomBehaviorGenerator behaviorGenerator = configuration.AddBehaviorGenerator();
            if (behaviorGenerator == null)
                behaviorGenerator = new DefaultCodeDomBehaviorGenerator();
            else
                behaviorGenerator = new DefaultCodeDomBehaviorGenerator(behaviorGenerator);

            return behaviorGenerator;
        }

        /// <summary>
        /// Compiles <paramref name="unit"/> and returns generated assembly.
        /// </summary>
        /// <param name="unit">Source code compile unit.</param>
        /// <returns>Generated assembly.</returns>
        private Assembly CompileCodeUnit(CodeCompileUnit unit)
        {
            IStaticCompiler compiler = compilerFactory.CreateStatic();

            string assemblyFilePath = Path.Combine(configuration.TempDirectory(), nameFormatter.FormatAssemblyFileName());
            ICompilerResult result = compiler.FromUnit(unit, assemblyFilePath);
            if (!result.IsSuccess)
            {
                // Save source code if compilation was not successfull.

                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                string sourceCodePath = Path.Combine(configuration.TempDirectory(), nameFormatter.FormatSourceCodeFileName());

                using (StreamWriter writer = new StreamWriter(sourceCodePath))
                {
                    provider.GenerateCodeFromCompileUnit(unit, writer, new CodeGeneratorOptions());
                }

                Ensure.Exception.UnCompilableSource(sourceCodePath);
            }

            // Load compiled assembly.
            return ReflectionFactory.FromCurrentAppDomain().LoadAssembly(assemblyFilePath);
        }
    }
}
