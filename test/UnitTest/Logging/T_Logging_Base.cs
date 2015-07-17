using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Logging
{
    [TestClass]
    public class T_Logging_Base
    {
        [TestMethod]
        public void BaseComposition()
        {
            ILogFactory logFactory = new DefaultLogFactory();
            logFactory.AddConsoleWriter();

            ILog log = logFactory.Scope("Application");
            log.Debug("Hello, {0}!", "World");
        }
    }
}
