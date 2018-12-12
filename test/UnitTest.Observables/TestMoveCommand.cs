using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Observables.Collections;
using Neptuo.Observables.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables
{
    [TestClass]
    public class TestMoveCommand
    {
        private ObservableCollection<int> collection;

        [TestInitialize]
        public void Initialize()
        {
            collection = new ObservableCollection<int>();
            collection.AddRange(1, 2, 3, 4, 5, 6);
        }

        [TestMethod]
        public void Down_Missing()
            => ExecuteDown(collection, 7, false, 1, 2, 3, 4, 5, 6);

        [TestMethod]
        public void Down_Null()
            => ExecuteDown(new ObservableCollection<string>() { "One", "Two", "Three" }, null, false, "One", "Two", "Three");

        [TestMethod]
        public void Down_First()
            => ExecuteDown(collection, 1, true, 2, 1, 3, 4, 5, 6);

        [TestMethod]
        public void Down_Middle()
            => ExecuteDown(collection, 3, true, 1, 2, 4, 3, 5, 6);

        [TestMethod]
        public void Down_Last()
            => ExecuteDown(collection, 6, false, 1, 2, 3, 4, 5, 6);


        [TestMethod]
        public void Up_Missng()
            => ExecuteUp(collection, 7, false, 1, 2, 3, 4, 5, 6);

        [TestMethod]
        public void Up_Null()
            => ExecuteUp(new ObservableCollection<string>() { "One", "Two", "Three" }, null, false, "One", "Two", "Three");

        [TestMethod]
        public void Up_First()
            => ExecuteUp(collection, 1, false, 1, 2, 3, 4, 5, 6);

        [TestMethod]
        public void Up_Middle()
            => ExecuteUp(collection, 3, true, 1, 3, 2, 4, 5, 6);

        [TestMethod]
        public void Up_Last()
            => ExecuteUp(collection, 6, true, 1, 2, 3, 4, 6, 5);


        private void ExecuteDown<T>(ObservableCollection<T> collection, T item, bool canExecute, params T[] newItems)
            => Execute(collection, new MoveDownCommand<T>(collection), item, canExecute, newItems);

        private void ExecuteUp<T>(ObservableCollection<T> collection, T item, bool canExecute, params T[] newItems)
            => Execute(collection, new MoveUpCommand<T>(collection), item, canExecute, newItems);

        private void Execute<T>(ObservableCollection<T> collection, Command<T> command, T item, bool canExecute, params T[] newItems)
        {
            Assert.AreEqual(canExecute, command.CanExecute(item));
            command.Execute(item);

            Assert.AreEqual(newItems.Length, collection.Count, "Count of items in test collection and in the result collection must be equal.");

            for (int i = 0; i < newItems.Length; i++)
                Assert.AreEqual(newItems[i], collection[i]);
        }
    }
}
