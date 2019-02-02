using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    [TestClass]
    public class T_KeyFactory
    {
        [TestMethod]
        public void SetGuidKeyWithTypeFullNameAndAssembly()
        {
            KeyFactory.SetGuidKeyWithTypeFullNameAndAssembly();
            GuidKey key = KeyFactory.Create(typeof(KeyFactory)).AsGuidKey();

            Assert.AreEqual("Neptuo.KeyFactory, Neptuo.EventSourcing", key.Type);

            Type keyFactoryType = Type.GetType(key.Type);
            Assert.IsNotNull(keyFactoryType);
            Assert.AreEqual(typeof(KeyFactory), keyFactoryType);
        }
    }
}
