using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Data.Sql;

namespace UnitTest.Data.Sql
{
    [TestClass]
    public class Base
    {
        [TestMethod]
        public void TestMethod1()
        {
            IDataReaderItem dataItem = null;

            TermTable termTable = new TermTable();
            int? productId = dataItem.GetValue(termTable.Product.Id);

        }
    }
}
