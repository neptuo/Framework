using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.Specialized
{
    [TestClass]
    public class DisposableCollectionTest
    {
        [TestMethod]
        public void Basic()
        {
            Disposable d1 = new Disposable();
            Disposable d2 = new Disposable();

            using (DisposableCollection collection = new DisposableCollection())
                collection.AddRange(d1, d2);

            Assert.IsTrue(d1.IsDisposed);
            Assert.IsTrue(d2.IsDisposed);
        }
    }

    public class Disposable : DisposableBase
    {
        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();
        }
    }
}
