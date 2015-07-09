using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public class T_Activators_Simple
    {
        [TestClass]
        public class Registration : TestRegistration
        {
            protected override IDependencyContainer CreateContainer()
            {
                return new SimpleDependencyContainer();
            }
        }

        [TestClass]
        public class Resolve : TestResolve
        {
            protected override IDependencyContainer CreateContainer()
            {
                return new SimpleDependencyContainer();
            }
        }
    }
}
