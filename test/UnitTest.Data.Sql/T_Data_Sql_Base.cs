using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Data.Sql;

namespace UnitTest.Data.Sql
{
    [TestClass]
    public class T_Data_Sql_Base
    {
        [TestMethod]
        public void ReadingValues()
        {
            IDataReaderItem dataItem = null;

            TermTable termTable = new TermTable();
            int? productId = dataItem.GetValue(termTable.Product.Id);
        }

        [TestMethod]
        public void InsertingValues()
        {
            IDataInserterItem dataItem = null;

            TermTable termTable = new TermTable();
            dataItem.SetValue(termTable.Product.Id, 5);
        }
    }
}
