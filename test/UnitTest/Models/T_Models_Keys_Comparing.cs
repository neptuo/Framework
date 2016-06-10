using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Models
{
    [TestClass]
    public class T_Models_Keys_Comparing
    {
        [TestMethod]
        public void Int16Key_Equal()
        {
            Int16Key key1 = Int16Key.Create(1, "Product");
            Int16Key key2 = Int16Key.Create(1, "Product");
            Assert.AreEqual(key1, key2);

            key1 = Int16Key.Create(1, "Product");
            key2 = Int16Key.Create(2, "Product");
            Assert.AreNotEqual(key1, key2);

            key1 = Int16Key.Create(1, "Product");
            key2 = Int16Key.Create(1, "Term");
            Assert.AreNotEqual(key1, key2);
        }

        [TestMethod]
        public void Int32Key_Equal()
        {
            Int32Key key1 = Int32Key.Create(1, "Product");
            Int32Key key2 = Int32Key.Create(1, "Product");
            Assert.AreEqual(key1, key2);

            key1 = Int32Key.Create(1, "Product");
            key2 = Int32Key.Create(2, "Product");
            Assert.AreNotEqual(key1, key2);

            key1 = Int32Key.Create(1, "Product");
            key2 = Int32Key.Create(1, "Term");
            Assert.AreNotEqual(key1, key2);
        }

        [TestMethod]
        public void Int64Key_Equal()
        {
            Int64Key key1 = Int64Key.Create(1, "Product");
            Int64Key key2 = Int64Key.Create(1, "Product");
            Assert.AreEqual(key1, key2);

            key1 = Int64Key.Create(1, "Product");
            key2 = Int64Key.Create(2, "Product");
            Assert.AreNotEqual(key1, key2);

            key1 = Int64Key.Create(1, "Product");
            key2 = Int64Key.Create(1, "Term");
            Assert.AreNotEqual(key1, key2);
        }

        [TestMethod]
        public void StringKey_Equal()
        {
            StringKey key1 = StringKey.Create("1", "Product");
            StringKey key2 = StringKey.Create("1", "Product");
            Assert.AreEqual(key1, key2);

            key1 = StringKey.Create("1", "Product");
            key2 = StringKey.Create("2", "Product");
            Assert.AreNotEqual(key1, key2);

            key1 = StringKey.Create("1", "Product");
            key2 = StringKey.Create("1", "Term");
            Assert.AreNotEqual(key1, key2);
        }

        [TestMethod]
        public void GuidKey_Equal()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            GuidKey key1 = GuidKey.Create(guid1, "Product");
            GuidKey key2 = GuidKey.Create(guid1, "Product");
            Assert.AreEqual(key1, key2);

            key1 = GuidKey.Create(guid1, "Product");
            key2 = GuidKey.Create(guid2, "Product");
            Assert.AreNotEqual(key1, key2);

            key1 = GuidKey.Create(guid1, "Product");
            key2 = GuidKey.Create(guid1, "Term");
            Assert.AreNotEqual(key1, key2);
        }

        [TestMethod]
        public void Int16Key_Ordering()
        {
            Int16Key key1 = Int16Key.Create(1, "Product");
            Int16Key key2 = Int16Key.Create(2, "Product");
            Int16Key key3 = Int16Key.Create(3, "Product");
            Int16Key key4 = Int16Key.Create(4, "Product");

            Int16Key key5 = Int16Key.Create(1, "Term");
            Int16Key key6 = Int16Key.Create(2, "Term");
            Int16Key key7 = Int16Key.Create(3, "Term");
            Int16Key key8 = Int16Key.Create(4, "Term");

            List<Int16Key> list = new List<Int16Key>() { key5, key4, key3, key7, key6, key8, key1, key2 };
            list.Sort();

            Assert.AreEqual(list[0], Int16Key.Create(1, "Product"));
            Assert.AreEqual(list[1], Int16Key.Create(2, "Product"));
            Assert.AreEqual(list[2], Int16Key.Create(3, "Product"));
            Assert.AreEqual(list[3], Int16Key.Create(4, "Product"));
            Assert.AreEqual(list[4], Int16Key.Create(1, "Term"));
            Assert.AreEqual(list[5], Int16Key.Create(2, "Term"));
            Assert.AreEqual(list[6], Int16Key.Create(3, "Term"));
            Assert.AreEqual(list[7], Int16Key.Create(4, "Term"));
        }

        [TestMethod]
        public void Int32Key_Ordering()
        {
            Int32Key key1 = Int32Key.Create(1, "Product");
            Int32Key key2 = Int32Key.Create(2, "Product");
            Int32Key key3 = Int32Key.Create(3, "Product");
            Int32Key key4 = Int32Key.Create(4, "Product");

            Int32Key key5 = Int32Key.Create(1, "Term");
            Int32Key key6 = Int32Key.Create(2, "Term");
            Int32Key key7 = Int32Key.Create(3, "Term");
            Int32Key key8 = Int32Key.Create(4, "Term");

            List<Int32Key> list = new List<Int32Key>() { key5, key4, key3, key7, key6, key8, key1, key2 };
            list.Sort();

            Assert.AreEqual(list[0], Int32Key.Create(1, "Product"));
            Assert.AreEqual(list[1], Int32Key.Create(2, "Product"));
            Assert.AreEqual(list[2], Int32Key.Create(3, "Product"));
            Assert.AreEqual(list[3], Int32Key.Create(4, "Product"));
            Assert.AreEqual(list[4], Int32Key.Create(1, "Term"));
            Assert.AreEqual(list[5], Int32Key.Create(2, "Term"));
            Assert.AreEqual(list[6], Int32Key.Create(3, "Term"));
            Assert.AreEqual(list[7], Int32Key.Create(4, "Term"));
        }

        [TestMethod]
        public void Int64Key_Ordering()
        {
            Int64Key key1 = Int64Key.Create(1, "Product");
            Int64Key key2 = Int64Key.Create(2, "Product");
            Int64Key key3 = Int64Key.Create(3, "Product");
            Int64Key key4 = Int64Key.Create(4, "Product");

            Int64Key key5 = Int64Key.Create(1, "Term");
            Int64Key key6 = Int64Key.Create(2, "Term");
            Int64Key key7 = Int64Key.Create(3, "Term");
            Int64Key key8 = Int64Key.Create(4, "Term");

            List<Int64Key> list = new List<Int64Key>() { key5, key4, key3, key7, key6, key8, key1, key2 };
            list.Sort();

            Assert.AreEqual(list[0], Int64Key.Create(1, "Product"));
            Assert.AreEqual(list[1], Int64Key.Create(2, "Product"));
            Assert.AreEqual(list[2], Int64Key.Create(3, "Product"));
            Assert.AreEqual(list[3], Int64Key.Create(4, "Product"));
            Assert.AreEqual(list[4], Int64Key.Create(1, "Term"));
            Assert.AreEqual(list[5], Int64Key.Create(2, "Term"));
            Assert.AreEqual(list[6], Int64Key.Create(3, "Term"));
            Assert.AreEqual(list[7], Int64Key.Create(4, "Term"));
        }
    }
}
