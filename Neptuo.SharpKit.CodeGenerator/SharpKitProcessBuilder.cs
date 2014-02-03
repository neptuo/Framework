using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.SharpKit.CodeGenerator
{
    public class SharpKitProcessBuilder
    {
        private string executable;
        private StringBuilder arguments = new StringBuilder();

        public SharpKitProcessBuilder Executable(string executable)
        {
            this.executable = executable;
            return this;
        }

        public SharpKitProcessBuilder AddReference(params string[] referencePath)
        {
            foreach (string reference in referencePath)
                arguments.AppendFormat("/reference:\"{0}\" ", reference);

            return this;
        }

        public SharpKitProcessBuilder AddPlugin(params string[] plugins)
        {
            foreach (string plugin in plugins)
                arguments.AppendFormat("/plugin:\"{0}\" ", plugin);

            return this;
        }

        public SharpKitProcessBuilder Rebuild()
        {
            arguments.Append("/rebuild ");
            return this;
        }

        public SharpKitProcessBuilder OutputGeneratedJsFile(string jsPath)
        {
            arguments.AppendFormat("/OutputGeneratedJsFile:\"{0}\" ", jsPath);
            return this;
        }

        public SharpKitProcessBuilder ManifestFile(string manifestPath)
        {
            arguments.AppendFormat("/ManifestFile:\"{0}\" ", manifestPath);
            return this;
        }

        public SharpKitProcessBuilder AddSourceFile(params string[] sourceCodePath)
        {
            foreach (string sourceCode in sourceCodePath)
                arguments.AppendFormat("\"{0}\" ", sourceCode);

            return this;
        }

        public SharpKitProcessBuilder OutDll(string dllPath)
        {
            arguments.AppendFormat("/out:{0} ", dllPath);
            return this;
        }

        public string ToCommandLine()
        {
            return String.Format("{0} {1}", Executable(), Arguments());
        }

        public string Executable()
        {
            return executable;
        }

        public string Arguments()
        {
            return arguments.ToString();
        }
    }
}
