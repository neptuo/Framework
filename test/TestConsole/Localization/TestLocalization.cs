using Neptuo.Globalization;
using Neptuo.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Localization
{
    class TestLocalization : TestClass
    {
        public static void Test()
        {
            //TestParentCultures();
            TestCallingAssembly();
        }

        static void TestCallingAssembly()
        {
            DebugIteration("ToLAndBack", 100000, () =>
            {
                string text = (L)"Hello, World!";
            });
            DebugIteration("GetCallingAssembly", 100000, () => Assembly.GetCallingAssembly());

            Console.WriteLine(Assembly.GetCallingAssembly().FullName);

            Translate.SetHandler((assembly, text) =>
            {
                Console.WriteLine(assembly.FullName);
                return text;
            });

            Console.WriteLine((L)"Hello, World!");
        }

        static void TestParentCultures()
        {
            CultureInfo cultureInfo;
            if (CultureInfoParser.TryParse("cs-cz", out cultureInfo))
            {
                List<CultureInfo> cultureInfos = new List<CultureInfo>() { cultureInfo };
                while (cultureInfo.Parent != CultureInfo.InvariantCulture)
                {
                    cultureInfo = cultureInfo.Parent;
                    cultureInfos.Add(cultureInfo);
                }

                foreach (CultureInfo foundCulture in cultureInfos)
                {
                    Console.WriteLine(foundCulture);
                }

                return;
            }

            Console.WriteLine("Unnable to parse...");
        }
    }
}
