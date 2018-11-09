using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Converters
{
    [TestClass]
    public class T_Converters_ConverterRepository : TestConvertersBase
    {
        [TestMethod]
        public void ToString()
        {
            Converts.Repository
                .AddToString<DateTime>("yyy-MM-dd")
                .AddToStringSearchHandler();

            string value;
            Assert.AreEqual(true, Converts.Try(5, out value));
            Assert.AreEqual("5", value);

            Assert.AreEqual(true, Converts.Try(new DateTime(2015, 9, 1), out value));
            Assert.AreEqual("2015-09-01", value);
        }

        [TestMethod]
        public void StringToEnum()
        {
            Converts.Repository
                .AddEnumSearchHandler(false);

            Gender gender1;
            Assert.AreEqual(true, Converts.Try("male", out gender1));
            Assert.AreEqual(gender1, Gender.Male);
            Assert.AreEqual(false, Converts.Try("xmale", out gender1));

            Gender? gender2;
            Assert.AreEqual(true, Converts.Try(String.Empty, out gender2));
            Assert.AreEqual(true, Converts.Try("   ", out gender2));
            Assert.AreEqual(true, Converts.Try("female", out gender2));
            Assert.AreEqual(gender2, Gender.Female);
        }

        [TestMethod]
        public void StringToNullable()
        {
            Converts.Repository
                .AddStringTo<int>(Int32.TryParse);

            int int1;
            Assert.AreEqual(true, Converts.Try("1", out int1));
            Assert.AreEqual(false, Converts.Try(" ", out int1));
            Assert.AreEqual(false, Converts.Try(String.Empty, out int1));

            int? int2;
            Assert.AreEqual(true, Converts.Try("1", out int2));
            Assert.AreEqual(true, Converts.Try(" ", out int2));
            Assert.AreEqual(true, Converts.Try(String.Empty, out int2));
        }

        [TestMethod]
        public void StringToCollection()
        {
            Converts.Repository
                .AddStringToCollection<int>(Int32.TryParse);

            List<int> values1;
            Assert.AreEqual(true, Converts.Try("1,2,3", out values1));
            Assert.AreEqual(true, Converts.Try("1, 2, 3", out values1));

            IList<int> values2;
            Assert.AreEqual(true, Converts.Try("1,2,3", out values2));
            Assert.AreEqual(true, Converts.Try("1, 2, 3", out values2));

            ICollection<int> values3;
            Assert.AreEqual(true, Converts.Try("1,2,3", out values3));
            Assert.AreEqual(true, Converts.Try("1, 2, 3", out values3));

            IEnumerable<int> values4;
            Assert.AreEqual(true, Converts.Try("1,2,3", out values4));
            Assert.AreEqual(true, Converts.Try("1, 2, 3", out values4));
        }

        [TestMethod]
        public void TwoWayConverter()
        {
            Converts.Repository
                .Add(new TimeSpanConveter());

            TimeSpan timeSpan;
            Assert.AreEqual(true, Converts.Try("03:00:00", out timeSpan));
            Assert.AreEqual(TimeSpan.FromHours(3), timeSpan);

            string value;
            Assert.AreEqual(true, Converts.Try(timeSpan, out value));
            Assert.AreEqual("03:00:00", value);
        }

        [TestMethod]
        public void Context()
        {
            Converts.Repository
                .Add<string, int>(Int32.TryParse)
                .Add(new StringToListConverter<int>());

            List<int> list;
            Assert.AreEqual(true, Converts.Try("2,34,1", out list));
            AssertList(list);

            object rawList;
            Assert.AreEqual(true, Converts.Try(typeof(string), typeof(List<int>), "2,34,1", out rawList));
            Assert.IsInstanceOfType(rawList, typeof(List<int>));
            AssertList((List<int>)rawList);

            Func<string, List<int>> converter = Converts.Repository.GetConverter<string, List<int>>();
            list = converter("2,34,1");
            AssertList(list);

            OutFunc<string, List<int>, bool> tryConverter = Converts.Repository.GetTryConverter<string, List<int>>();
            Assert.AreEqual(true, tryConverter("2,34,1", out list));
            AssertList(list);
        }

        private void AssertList(List<int> list)
        {
            Assert.IsNotNull(list);
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(2, list[0]);
            Assert.AreEqual(34, list[1]);
            Assert.AreEqual(1, list[2]);
        }
    }
}
