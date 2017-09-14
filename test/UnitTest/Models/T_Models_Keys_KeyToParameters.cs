using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Collections.Specialized;
using Neptuo.Converters;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models
{
    [TestClass]
    public class T_Models_Keys_KeyToParameters
    {
        private IKeyToParametersConverter CreateConverter()
        {
            return new KeyToParametersConverter().AddDefaultMapping();
        }

        #region Add key to parameters

        [TestMethod]
        public void AddVariousKeyClass()
        {
            Converts.Repository
                .AddStringTo<Guid>(Guid.TryParse);

            IKeyToParametersConverter converter = CreateConverter();

            KeyValueCollection parameters = new KeyValueCollection();
            converter.Add(parameters, Int32Key.Create(5, "Product"));

            Assert.AreEqual(2, parameters.Keys.Count());
            Assert.AreEqual(5, parameters.Get<int>("ID"));
            Assert.AreEqual("Product", parameters.Get<string>("Type"));

            parameters = new KeyValueCollection();
            converter.Add(parameters, StringKey.Create("abcdef", "Product"));

            Assert.AreEqual(2, parameters.Keys.Count());
            Assert.AreEqual("abcdef", parameters.Get<string>("Identifier"));
            Assert.AreEqual("Product", parameters.Get<string>("Type"));

            parameters = new KeyValueCollection();
            Guid guid = Guid.NewGuid();
            converter.Add(parameters, GuidKey.Create(guid, "Product"));

            Assert.AreEqual(2, parameters.Keys.Count());
            Assert.AreEqual(guid, parameters.Get<Guid>("Guid"));
            Assert.AreEqual("Product", parameters.Get<string>("Type"));
        }

        [TestMethod]
        public void AddPrefix()
        {
            IKeyToParametersConverter converter = CreateConverter();

            KeyValueCollection parameters = new KeyValueCollection();
            converter.Add(parameters, "Source", Int32Key.Create(5, "Product"));
            converter.Add(parameters, "Target", Int32Key.Create(8, "Product"));

            Assert.AreEqual(4, parameters.Keys.Count());
            Assert.AreEqual(5, parameters.Get<int>("SourceID"));
            Assert.AreEqual("Product", parameters.Get<string>("SourceType"));
            Assert.AreEqual(8, parameters.Get<int>("TargetID"));
            Assert.AreEqual("Product", parameters.Get<string>("TargetType"));
        }

        [TestMethod]
        public void AddWithoutType()
        {
            IKeyToParametersConverter converter = CreateConverter();

            KeyValueCollection parameters = new KeyValueCollection();
            converter.AddWithoutType(parameters, Int32Key.Create(5, "Product"));

            Assert.AreEqual(1, parameters.Keys.Count());
            Assert.AreEqual(5, parameters.Get<int>("ID"));
        }

        [TestMethod]
        public void AddPrefixWithoutType()
        {
            IKeyToParametersConverter converter = CreateConverter();

            KeyValueCollection parameters = new KeyValueCollection();
            converter.AddWithoutType(parameters, "Source", Int32Key.Create(5, "Product"));
            converter.AddWithoutType(parameters, "Target", Int32Key.Create(8, "Product"));

            Assert.AreEqual(2, parameters.Keys.Count());
            Assert.AreEqual(5, parameters.Get<int>("SourceID"));
            Assert.AreEqual(8, parameters.Get<int>("TargetID"));
        }

        #endregion

        #region Get key from parameters

        [TestMethod]
        public void TryGetVariousKeyClass()
        {
            IKeyToParametersConverter converter = CreateConverter();

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("ID", 5)
                .Add("Type", "Product");

            Assert.AreEqual(true, converter.TryGet(parameters, out Int32Key int32Key));
            Assert.AreEqual(5, int32Key.ID);
            Assert.AreEqual("Product", int32Key.Type);

            parameters = new KeyValueCollection()
                .Add("Identifier", "abcdef")
                .Add("Type", "Product");

            Assert.AreEqual(true, converter.TryGet(parameters, out StringKey stringKey));
            Assert.AreEqual("abcdef", stringKey.Identifier);
            Assert.AreEqual("Product", stringKey.Type);

            Guid guid = Guid.NewGuid();
            parameters = new KeyValueCollection()
                .Add("Guid", guid)
                .Add("Type", "Product");

            Assert.AreEqual(true, converter.TryGet(parameters, out GuidKey guidKey));
            Assert.AreEqual(guid, guidKey.Guid);
            Assert.AreEqual("Product", guidKey.Type);
        }

        [TestMethod]
        public void TryGetVariousKeyClassNonGeneric()
        {
            KeyToParametersConverter.MappingCollection definitions = new KeyToParametersConverter.MappingCollection()
                .AddParametersToInt32Key()
                .AddParametersToStringKey()
                .AddParametersToGuidKey()
                .AddKeyTypeToKeyClass("Product", typeof(Int32Key))
                .AddKeyTypeToKeyClass("Project", typeof(StringKey))
                .AddKeyTypeToKeyClass("Category", typeof(GuidKey));

            IKeyToParametersConverter converter = new KeyToParametersConverter(definitions);

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("ID", 5)
                .Add("Type", "Product");

            Assert.AreEqual(true, converter.TryGet(parameters, out IKey key));
            if (key is Int32Key int32Key)
            {
                Assert.AreEqual(5, int32Key.ID);
                Assert.AreEqual("Product", int32Key.Type);
            }
            else
            {
                Assert.Fail("Not Int32Key");
            }

            parameters = new KeyValueCollection()
                .Add("Identifier", "abcdef")
                .Add("Type", "Project");

            Assert.AreEqual(true, converter.TryGet(parameters, out key));
            if (key is StringKey stringKey)
            {
                Assert.AreEqual("abcdef", stringKey.Identifier);
                Assert.AreEqual("Project", stringKey.Type);
            }
            else
            {
                Assert.Fail("Not StringKey");
            }

            Guid guid = Guid.NewGuid();
            parameters = new KeyValueCollection()
                .Add("Guid", guid)
                .Add("Type", "Category");

            Assert.AreEqual(true, converter.TryGet(parameters, out key));
            if (key is GuidKey guidKey)
            {
                Assert.AreEqual(guid, guidKey.Guid);
                Assert.AreEqual("Category", guidKey.Type);
            }
            else
            {
                Assert.Fail("Not GuidKey");
            }
        }

        [TestMethod]
        public void TryGetPrefix()
        {
            IKeyToParametersConverter converter = CreateConverter();

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("SourceID", 5)
                .Add("SourceType", "Product")
                .Add("TargetID", 6)
                .Add("TargetType", "Category");

            Assert.AreEqual(true, converter.TryGet(parameters, "Source", out Int32Key sourceKey));
            Assert.AreEqual(5, sourceKey.ID);
            Assert.AreEqual("Product", sourceKey.Type);
            Assert.AreEqual(true, converter.TryGet(parameters, "Target", out Int32Key targetKey));
            Assert.AreEqual(6, targetKey.ID);
            Assert.AreEqual("Category", targetKey.Type);
        }

        [TestMethod]
        public void TryGetPrefixNonGeneric()
        {
            KeyToParametersConverter.MappingCollection definitions = new KeyToParametersConverter.MappingCollection()
                .AddParametersToInt32Key()
                .AddKeyTypeToKeyClass("Product", typeof(Int32Key))
                .AddKeyTypeToKeyClass("Category", typeof(Int32Key));

            IKeyToParametersConverter converter = new KeyToParametersConverter(definitions);

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("SourceID", 5)
                .Add("SourceType", "Product")
                .Add("TargetID", 6)
                .Add("TargetType", "Category");

            Assert.AreEqual(true, converter.TryGet(parameters, "Source", out IKey key));
            if (key is Int32Key sourceKey)
            {
                Assert.AreEqual(5, sourceKey.ID);
                Assert.AreEqual("Product", sourceKey.Type);
            }
            else
            {
                Assert.Fail("Not Int32Key");
            }

            Assert.AreEqual(true, converter.TryGet(parameters, "Target", out key));
            if (key is Int32Key targetKey)
            {
                Assert.AreEqual(6, targetKey.ID);
                Assert.AreEqual("Category", targetKey.Type);
            }
            else
            {
                Assert.Fail("Not Int32Key");
            }
        }

        [TestMethod]
        public void TryGetWithoutType()
        {
            IKeyToParametersConverter converter = CreateConverter();

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("ID", 5);

            Assert.AreEqual(true, converter.TryGetWithoutType(parameters, "Product", out Int32Key int32Key));
            Assert.AreEqual(5, int32Key.ID);
            Assert.AreEqual("Product", int32Key.Type);
        }

        [TestMethod]
        public void TryGetWithoutTypeNonGeneric()
        {
            KeyToParametersConverter.MappingCollection definitions = new KeyToParametersConverter.MappingCollection()
                .AddParametersToInt32Key()
                .AddKeyTypeToKeyClass("Product", typeof(Int32Key));

            IKeyToParametersConverter converter = new KeyToParametersConverter(definitions);

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("ID", 5);

            Assert.AreEqual(true, converter.TryGetWithoutType(parameters, "Product", out IKey key));
            if (key is Int32Key int32Key)
            {
                Assert.AreEqual(5, int32Key.ID);
                Assert.AreEqual("Product", int32Key.Type);
            }
            else
            {
                Assert.Fail("Not Int32Key");
            }
        }

        [TestMethod]
        public void TryGetPrefixWithoutType()
        {
            IKeyToParametersConverter converter = CreateConverter();

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("SourceID", 5)
                .Add("TargetID", 6);

            Assert.AreEqual(true, converter.TryGetWithoutType(parameters, "Product", "Source", out Int32Key sourceKey));
            Assert.AreEqual(5, sourceKey.ID);
            Assert.AreEqual("Product", sourceKey.Type);
            Assert.AreEqual(true, converter.TryGetWithoutType(parameters, "Category", "Target", out Int32Key targetKey));
            Assert.AreEqual(6, targetKey.ID);
            Assert.AreEqual("Category", targetKey.Type);
        }

        [TestMethod]
        public void TryGetPrefixWithoutTypeNonGeneric()
        {
            KeyToParametersConverter.MappingCollection definitions = new KeyToParametersConverter.MappingCollection()
                .AddParametersToInt32Key()
                .AddKeyTypeToKeyClass("Product", typeof(Int32Key))
                .AddKeyTypeToKeyClass("Category", typeof(Int32Key));

            IKeyToParametersConverter converter = new KeyToParametersConverter(definitions);

            IKeyValueCollection parameters = new KeyValueCollection()
                .Add("SourceID", 5)
                .Add("TargetID", 6);

            Assert.AreEqual(true, converter.TryGetWithoutType(parameters, "Product", "Source", out IKey key));
            if (key is Int32Key sourceKey)
            {
                Assert.AreEqual(5, sourceKey.ID);
                Assert.AreEqual("Product", sourceKey.Type);
            }
            else
            {
                Assert.Fail("Not Int32Key");
            }

            Assert.AreEqual(true, converter.TryGetWithoutType(parameters, "Category", "Target", out key));
            if (key is Int32Key targetKey)
            {
                Assert.AreEqual(6, targetKey.ID);
                Assert.AreEqual("Category", targetKey.Type);
            }
            else
            {
                Assert.Fail("Not Int32Key");
            }
        }

        #endregion

        #region Throwing exceptions

        [TestMethod]
        [ExpectedException(typeof(MissingKeyClassToParametersMappingException))]
        public void ThrowMissingKeyClassToParametersMappingException()
        {
            IKeyToParametersConverter converter = new KeyToParametersConverter(new KeyToParametersConverter.MappingCollection());
            converter.Add(new KeyValueCollection(), Int32Key.Create(5, "Product"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ThrowAddKeyTypeToKeyClassMapping()
        {
            IKeyToParametersConverter converter = new KeyToParametersConverter(new KeyToParametersConverter.MappingCollection().AddKeyTypeToKeyClass("Product", typeof(int)));
        }

        [TestMethod]
        [ExpectedException(typeof(MissingParametersToKeyClassMappingException))]
        public void ThrowMissingParametersToKeyClassMappingException()
        {
            IKeyToParametersConverter converter = new KeyToParametersConverter(new KeyToParametersConverter.MappingCollection());
            converter.TryGet(new KeyValueCollection(), out Int32Key int32Key);
        }

        [TestMethod]
        [ExpectedException(typeof(MissingKeyTypeToKeyClassMappingException))]
        public void ThrowMissingKeyTypeToKeyClassMappingException()
        {
            IKeyToParametersConverter converter = new KeyToParametersConverter(new KeyToParametersConverter.MappingCollection());
            converter.TryGet(new KeyValueCollection().Add("Type", "Product"), out IKey key);
        }

        #endregion
    }
}
