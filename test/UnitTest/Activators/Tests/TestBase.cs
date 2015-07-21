using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Activators.Tests
{
    public abstract class TestBase
    {
        protected abstract IDependencyContainer CreateContainer();

        protected void TryCatchUnResolvable<T>(IDependencyProvider provider)
        {
            bool isResolved = false;
            try
            {
                T service = provider.Resolve<T>();
                isResolved = true;
            }
            catch (DependencyResolutionFailedException)
            { }

            if (isResolved)
                Assert.Fail("Should not be resolvable.");
        }
    }
}
