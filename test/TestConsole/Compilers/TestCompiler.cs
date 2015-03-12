using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Compilers
{
    static class TestCompiler
    {
        public static void Test()
        {
            //CsCodeDomCompiler compiler = new CsCodeDomCompiler();
            //compiler.AddReferencedAssemblies("System.dll");
            //compiler.AddReferencedFolder(Environment.CurrentDirectory);
            //compiler.IsGeneratedInMemory = true;

            //CompilerResults result = compiler.CompileAssemblyFromFile(@"..\..\Compilers\ProgramTest.cs");

            //foreach (CompilerError error in result.Errors)
            //    Console.WriteLine(String.Format("{0}:{1} -> {2}", error.Line, error.Column, error.ErrorText));

            //Assembly assembly = result.CompiledAssembly;
            //using (FileStream stream = new FileStream("NewAssembly.dll", FileMode.Create))
            //{
            //    BinaryFormatter formatter = new BinaryFormatter();
            //    formatter.Serialize(stream, assembly);
            //}

            //Assembly newAssembly = Assembly.LoadFrom("NewAssembly.dll");
            //Type type = newAssembly.GetType("TestConsole.Program2");
            //Console.WriteLine(type.GetMethods().Length);

            AssemblyName name = AssemblyName.GetAssemblyName(@"D:\Projects\Neptuo\Neptuo.Web.Services.Hosting.AspNet\bin\Release\Neptuo.Web.Services.Hosting.AspNet.dll");
            Console.WriteLine(name);

            Assembly assembly = AppDomain.CurrentDomain.Load(name);
            Console.WriteLine(assembly.FullName);
        }
    }
}
