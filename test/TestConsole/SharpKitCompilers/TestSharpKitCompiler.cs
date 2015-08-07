using Neptuo.Compilers;
using Neptuo.Compilers.Errors;
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
            //TestOld();
            TestNew();
        }

        static void TestNew()
        {
            CompilerFactory compilerFactory = new CompilerFactory(
                new CompilerConfiguration()
                    .AddTempDirectory(@"C:\Temp\SharpKit")
                    .Plugins(
                        new SharpKitPluginCollection()
                            .Add("Neptuo.SharpKit.Exugin.ExuginPlugin, Neptuo.SharpKit.Exugin")
                    )
                    .AddReferences(
                        new CompilerReferenceCollection()
                            .AddDirectory(Environment.CurrentDirectory)
                            .AddAssembly("SharpKit.JavaScript.dll")
                            .AddAssembly("SharpKit.Html.dll")
                            .AddAssembly("SharpKit.jQuery.dll")
                    )
            );

            ISharpKitCompiler compiler = compilerFactory.CreateSharpKit();


            string javascriptFilePath = Path.Combine(Environment.CurrentDirectory, "TestClass.js");
            ICompilerResult result = compiler.FromSourceFile(@"D:\Development\Neptuo\Common\test\TestConsole\SharpKitCompilers\TestClass.cs", javascriptFilePath);
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
