using Neptuo.Globalization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Localization
{
    class TestLocalization
    {
        public static void Test()
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
