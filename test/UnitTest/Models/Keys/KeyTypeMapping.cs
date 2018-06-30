using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    [TestClass]
    public class KeyTypeMapping
    {
        private void Test(IKeyTypeProvider provider, Type type, string keyType)
            => Assert.AreEqual(provider.Get(type), keyType);

        private void Test(IKeyTypeProvider provider, string keyType, Type type)
            => Assert.AreEqual(provider.Get(keyType), type);

        [TestMethod]
        public void AssemblyQualified()
        {
            IKeyTypeProvider provider = new AssemblyQualifiedKeyTypeProvider();
            Test(provider, typeof(Model).AssemblyQualifiedName, typeof(Model));
            Test(provider, typeof(Model), typeof(Model).AssemblyQualifiedName);
        }

        [TestMethod]
        public void TypeFullName()
        {
            IKeyTypeProvider provider = new TypeFullNameKeyTypeProvider();
            Test(provider, typeof(Model).FullName, typeof(Model));
            Test(provider, typeof(Model), typeof(Model).FullName);
        }

        [TestMethod]
        public void TypeFullNameWithAssembly()
        {
            IKeyTypeProvider provider = new TypeFullNameWithAssemblyKeyTypeProvider();
            Test(provider, typeof(Model).FullName + ", " + typeof(Model).Assembly.GetName().Name, typeof(Model));
            Test(provider, typeof(Model), typeof(Model).FullName + ", " + typeof(Model).Assembly.GetName().Name);
        }

        [TestMethod]
        public void Manual()
        {
            IKeyTypeProvider provider = new KeyTypeCollection().AddDual("Model", typeof(Model));
            Test(provider, "Model", typeof(Model));
            Test(provider, typeof(Model), "Model");
        }
    }

    public class Model
    { }
}
