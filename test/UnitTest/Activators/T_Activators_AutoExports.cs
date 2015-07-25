using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators.AutoExports;
using Neptuo.Activators.Tests;
using Neptuo.Reflections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    [TestClass]
    public class T_Activators_AutoExports
    {
        [TestMethod]
        public void Base()
        {
            IDependencyContainer root = new SimpleDependencyContainer();

            IReflectionService reflectionService = ReflectionFactory.FromCurrentAppDomain();
            using (ITypeExecutorService executorService = reflectionService.PrepareTypeExecutors())
            {
                executorService.AddDependencies(root);
            }

            root.Resolve<IOutputWriter>();

            using (IDependencyProvider s1 = root.Scope("S1"))
            {
                s1.Resolve<Counter>();
                s1.Resolve<Counter>();
                s1.Resolve<Counter>();

                Assert.AreEqual(1, Counter.count);
            }
        }
    }
}
