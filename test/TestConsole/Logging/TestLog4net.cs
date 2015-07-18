using log4net.Config;
using Neptuo;
using Neptuo.Logging;
using Neptuo.Logging.Serialization;
using Neptuo.Logging.Serialization.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Logging
{
    class TestLog4net
    {
        public static void Test()
        {
            XmlConfigurator.Configure();

            Converts.Repository
                .Add(typeof(ExceptionModel), typeof(string), new ExceptionModelConverter());

            ILogFormatter formatter = new DefaultLogFormatter();

            ILogFactory logFactory = new DefaultLogFactory()
                .AddConsole(formatter)
                .AddLog4net();

            ILog root = logFactory.Scope("Root");
            root.Debug("Hello, World!");
            root.Fatal(Ensure.Exception.NotImplemented());
        }
    }
}
