using Neptuo.Compilers;
using Neptuo.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.SharpKitCompilers
{
    class TestSharpKitCompiler
    {
        public static void Test()
        {
            CompilerFactory compilerFactory = new CompilerFactory();

            ISharpKitCompiler compiler = compilerFactory.CreateSharpKit();
            compiler.TempDirectory = @"C:\Temp\SharpKit";
            compiler.References.AddDirectory(Environment.CurrentDirectory);
            compiler.References.AddAssembly("SharpKit.JavaScript.dll");
            compiler.References.AddAssembly("SharpKit.Html.dll");
            compiler.References.AddAssembly("SharpKit.jQuery.dll");
            //compiler.Plugins.Add("Neptuo.SharpKit.Exugin.ExuginPlugin, Neptuo.SharpKit.Exugin");

            string javascriptFilePath = Path.Combine(Environment.CurrentDirectory, "TestClass.js");
            ICompilerResult result = compiler.FromSourceFile(@"C:\Users\marek.fisera\Projects\Neptuo\Common\test\TestConsole\SharpKitCompilers\TestClass.cs", javascriptFilePath);
            if (result.IsSuccess)
            {
                Console.WriteLine("Successfully compiled to javascript...");
                Console.WriteLine(File.ReadAllText(javascriptFilePath));
            }
            else
            {
                Console.WriteLine("Error compiling to javascript...");

                foreach (IErrorInfo errorInfo in result.Errors)
                {
                    Console.WriteLine(
                        "{0}:{1} -> {2}", 
                        errorInfo.LineNumber, 
                        errorInfo.ColumnIndex, 
                        errorInfo.ErrorText
                    ); 
                }
            }
        }
    }
}
