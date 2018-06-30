using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models.Keys
{
    [TestClass]
    public class KeyCollections
    {
        [TestMethod]
        public void EmptyKeyIsIgnored()
        {
            KeyCollection keys = new KeyCollection();
            keys.Add(Int32Key.Empty("Product"));
            Assert.AreEqual(0, keys.Count);
        }

        [TestMethod]
        public void AllSupportedKeyTypesAndClasses()
        {
            KeyCollection keys = new KeyCollection();
            keys.Add(Int32Key.Create(1, "Product"));
            keys.Add(GuidKey.Create(Guid.NewGuid(), "Category"));
        }

        [TestMethod]
        public void SupportedTypes()
        {
            KeyCollection keys = new KeyCollection(new string[] { "Product", "Category" });
            keys.Add(Int32Key.Create(1, "Product"));
            keys.Add(GuidKey.Create(Guid.NewGuid(), "Category"));
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredKeyOfTypeException))]
        public void NotSupportedTypes()
        {
            KeyCollection keys = new KeyCollection(new string[] { "Product", "Category" });
            keys.Add(Int32Key.Create(1, "PriceList"));
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredKeyOfTypeException))]
        public void NotSupportedTypesForEmptyKey()
        {
            KeyCollection keys = new KeyCollection(new string[] { "Product", "Category" });
            keys.Add(StringKey.Empty("PriceList"));
        }

        [TestMethod]
        public void SupportedClasses()
        {
            KeyCollection keys = new KeyCollection(null, new Type[] { typeof(Int32Key), typeof(GuidKey) });
            keys.Add(Int32Key.Create(1, "Product"));
            keys.Add(GuidKey.Create(Guid.NewGuid(), "Category"));
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredKeyOfClassException))]
        public void NotSupportedClasses()
        {
            KeyCollection keys = new KeyCollection(null, new Type[] { typeof(Int32Key), typeof(GuidKey) });
            keys.Add(StringKey.Create("1", "PriceList"));
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredKeyOfClassException))]
        public void NotSupportedClassesForEmptyKey()
        {
            KeyCollection keys = new KeyCollection(null, new Type[] { typeof(Int32Key), typeof(GuidKey) });
            keys.Add(StringKey.Empty("PriceList"));
        }
    }
}
