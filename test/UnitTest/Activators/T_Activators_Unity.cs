using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Activators.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators
{
    public class T_Activators_Unity
    {
        [TestClass]
        public class Registraion : TestRegistration
        {
            protected override IDependencyContainer CreateContainer()
            {
                return new UnityDependencyContainer();
            }
        }

        [TestClass]
        public class Resolve : TestResolve
        {
            protected override IDependencyContainer CreateContainer()
            {
                return new UnityDependencyContainer();
            }
        }
    }
}
