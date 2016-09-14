using ICSharpCode.NRefactory.TypeSystem;
using SharpKit.UnobtrusiveFeatures.ClassMetaExports;
using SharpKit.UnobtrusiveFeatures.Exports;
using SharpKit.UnobtrusiveFeatures.Expressions;
using SharpKit.Compiler;
using SharpKit.JavaScript.Ast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpKit.UnobtrusiveFeatures
{
    public class Plugin : ICompilerPlugin
    {
        private readonly bool debug = false;

        private ConfigurationHelper configuration;

        private ICompiler compiler;

        #region ExportExtension

        private ExportExtension export;

        private readonly string[] exportAdditionalNameFileNames;
        private readonly string exportAttributeName;
        private readonly string exportNamespaceAttributeName;

        #endregion

        #region ExpressionExtension

        private ExpressionExtension expression;
        
        #endregion

        #region CtorExportExtension

        private CtorExportExtension ctorExport;

        #endregion

        #region IsAbtractExportExtension

        private IsAbtractExportExtension isAbstract;

        #endregion

        public Plugin()
        {
            configuration = new ConfigurationHelper();
            debug = configuration.GetBool("Debug");

            exportAdditionalNameFileNames = configuration.GetStringArray("ExportAdditionalNameFileNames") ?? new string[0];
            //exportAttributeName = configuration.GetString("ExportAttributeName", "ExportAttribute");
            //exportNamespaceAttributeName = configuration.GetString("ExportNamespaceAttributeName", "ExportNamespaceAttribute");
        }

        public void Init(ICompiler compiler)
        {
            this.compiler = compiler;
            compiler.AfterParseCs += Compiler_AfterParseCs;
            compiler.AfterConvertCsToJsEntity += Compiler_AfterConvertCsToJsEntity;

            export = new ExportExtension(exportAdditionalNameFileNames, debug);
            //expression = new ExpressionExtension(configuration.GetString("ExpressionPrefixes", "Neptuo").Split(','), debug);
            ctorExport = new CtorExportExtension(debug);
            isAbstract = new IsAbtractExportExtension(debug);
        }

        private void Compiler_AfterParseCs()
        {
            export.Process(compiler.CsCompilation.Assemblies, compiler.CustomAttributeProvider);
            //expression.PrepareMethodCache(compiler.CsCompilation.Assemblies);
        }

        private void Compiler_AfterConvertCsToJsEntity(IEntity arg1, JsNode arg2)
        {
            //expression.Process(arg1, arg2);
            ctorExport.Process(arg1, arg2);
            isAbstract.Process(arg1, arg2);
        }
    }
}
