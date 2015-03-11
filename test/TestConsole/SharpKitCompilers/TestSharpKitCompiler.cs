using Neptuo.Compilers;
using Neptuo.ComponentModel;
using Neptuo.SharpKit.CodeGenerator;
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
                    .TempDirectory(@"C:\Temp\SharpKit")
                    .Plugins(
                        new SharpKitPluginCollection()
                            .Add("Neptuo.SharpKit.Exugin.ExuginPlugin, Neptuo.SharpKit.Exugin")
                    )
                    .References(
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

        static void TestOld()
        {
            string replaceClassDefinition = "[SharpKit.JavaScript.JsType(SharpKit.JavaScript.JsMode.Clr)] public sealed class ";
            string replacedSourceCode = File.ReadAllText(@"D:\Development\Neptuo\Common\test\TestConsole\SharpKitCompilers\TestClass.cs").Replace("public sealed class ", replaceClassDefinition);

            StringWriter output = new StringWriter();
            StringReader input = new StringReader(replacedSourceCode);

            SharpKitCompiler sharpKitGenerator = new SharpKitCompiler();
            sharpKitGenerator.AddReference("mscorlib.dll");
            sharpKitGenerator.AddReference("SharpKit.JavaScript.dll", "SharpKit.Html.dll", "SharpKit.jQuery.dll");
            //sharpKitGenerator.AddPlugin("Neptuo.SharpKit.Exugin", "Neptuo.SharpKit.Exugin.ExuginPlugin");

            sharpKitGenerator.AddReferenceFolder(Environment.CurrentDirectory);

            sharpKitGenerator.RemoveReference("System.Web.dll");

            sharpKitGenerator.TempDirectory = Environment.CurrentDirectory;

            try
            {
                sharpKitGenerator.Generate(new SharpKitCompilerContext(input, output));
                Console.WriteLine(output.ToString());
            }
            catch (SharpKitCompilerException e)
            {
                Console.WriteLine("Error.");

                foreach (string line in e.ExecuteResult.Output)
                    Console.WriteLine(line);
            }
        }
    }
}
