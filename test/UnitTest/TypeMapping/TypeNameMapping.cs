using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.TypeMapping
{
    [TestClass]
    public class TypeNameMapping
    {
        private void Test(ITypeNameMapper provider, Type type, string keyType)
            => Assert.AreEqual(provider.Get(type), keyType);

        private void Test(ITypeNameMapper provider, string keyType, Type type)
            => Assert.AreEqual(provider.Get(keyType), type);

        [TestMethod]
        public void AssemblyQualified()
        {
            ITypeNameMapper provider = new AssemblyQualifiedNameMapper();
            Test(provider, typeof(Model).AssemblyQualifiedName, typeof(Model));
            Test(provider, typeof(Model), typeof(Model).AssemblyQualifiedName);
        }

        [TestMethod]
        public void TypeFullName()
        {
            ITypeNameMapper provider = new TypeFullNameMapper();
            Test(provider, typeof(Model).FullName, typeof(Model));
            Test(provider, typeof(Model), typeof(Model).FullName);
        }

        [TestMethod]
        public void TypeFullNameWithAssembly()
        {
            ITypeNameMapper provider = new TypeFullNameWithAssemblyMapper();
            Test(provider, typeof(Model).FullName + ", " + typeof(Model).Assembly.GetName().Name, typeof(Model));
            Test(provider, typeof(Model), typeof(Model).FullName + ", " + typeof(Model).Assembly.GetName().Name);
        }

        [TestMethod]
        public void Manual()
        {
            ITypeNameMapper provider = new TypeNameCollection().AddDual("Model", typeof(Model));
            Test(provider, "Model", typeof(Model));
            Test(provider, typeof(Model), "Model");
        }
    }

    public class Model
    { }
}
