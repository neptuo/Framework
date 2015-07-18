using Neptuo;
using Neptuo.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestConsole.AppServices;
using TestConsole.Behaviors;
using TestConsole.BootstrapTasks;
using TestConsole.Cloning;
using TestConsole.Collections;
using TestConsole.Commands;
using TestConsole.Compilers;
using TestConsole.Configuration;
using TestConsole.Data;
using TestConsole.Delegates;
using TestConsole.DependencyContainers;
using TestConsole.Events;
using TestConsole.Hashing;
using TestConsole.Logging;
using TestConsole.ObjectSizes;
using TestConsole.PresentationModels;
using TestConsole.SharpKitCompilers;
using TestConsole.Threading;
using TestConsole.Tokens;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestTokens.Test();
            //TestTokenWriter.Test();
            //TestEntity.Test();
            //TestPresentationModels.Test();
            //TestXmlModelDefinition.Test();
            //TestFieldMetadataValidatorKey.Test();
            //TestConfiguration.Test();
            //TestCommands.Test();
            //TestEvents.Test();
            //TestDelegates.Test();
            //TestCompiler.Test();
            //TestHash.Test();
            //TestBootstrap.Test();
            //TestKeyValue.Test();
            //TestMultiLockProvider.Test();
            //TestDependency.Test();
            //TestAppServices.Test();
            //TestCloning.Test();
            //TestBehaviors.Test();
            //TestObjectSize.Test();
            //TestSharpKitCompiler.Test();
            TestLog4net.Test();

            Console.ReadKey(true);
        }

        static void TestCollection()
        {
            List<int> list = new List<int>();
            list.Insert(0, 1);
            list.Insert(1, 2);

            Console.WriteLine(list[2]);
            list.Insert(2, 3);

            IKeyValueCollection collection = null;
            int intValue;
            collection.TryGet("Hello", out intValue);
        }
    }

    class TestClass
    {
        public static void Debug(string title, Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            action();

            sw.Stop();
            Console.WriteLine("{0}: {1}ms", title, sw.ElapsedMilliseconds);
        }

        public static void DebugIteration(string title, int count, Action action)
        {
            Debug(title, () =>
            {
                for (int i = 0; i < count; i++)
                    action();
            });
        }

        public static T Debug<T>(string title, Func<T> action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            T result = action();

            sw.Stop();
            Console.WriteLine("{0}: {1}ms", title, sw.ElapsedMilliseconds);
            return result;
        }

        public static List<T> DebugIteration<T>(string title, int count, Func<T> action)
        {
            return Debug(title, () =>
            {
                List<T> result = new List<T>();
                for (int i = 0; i < count; i++)
                    action();
                
                return result;
            });
        }
    }
}
