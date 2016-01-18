using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Formatters.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTest.Formatters.Composite;

namespace UnitTest.Formatters.Composite
{
    [TestClass]
    public class T_Formatters_CompositeModel
    {
        [TestMethod]
        public void Base()
        {
            ReflectionCompositeTypeProvider provider = new ReflectionCompositeTypeProvider(new ReflectionCompositeDelegateFactory());
            CompositeType compositeType;
            Assert.AreEqual(true, provider.TryGet(typeof(UserModel), out compositeType));
            Assert.IsNotNull(compositeType);

            Assert.AreEqual("Test.UserModel", compositeType.Name);
            Assert.AreEqual(typeof(UserModel), compositeType.Type);

            Assert.IsNotNull(compositeType.VersionProperty);
            Assert.IsNull(compositeType.VersionProperty.Setter);

            Assert.AreEqual(2, compositeType.Versions.Count());

            IEnumerator<CompositeVersion> versionEnumerator = compositeType.Versions.GetEnumerator();

            Assert.AreEqual(true, versionEnumerator.MoveNext());
            CompositeVersion version = versionEnumerator.Current;
            Assert.AreEqual(1, version.Version);
            Assert.AreEqual(2, version.Properties.Count());

            Assert.AreEqual(true, versionEnumerator.MoveNext());
            version = versionEnumerator.Current;
            Assert.AreEqual(2, version.Version);
            Assert.AreEqual(3, version.Properties.Count());
        }
    }
}
