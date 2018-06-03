using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Observables
{
    [TestClass]
    public class TestPropertyChangedObserver
    {
        [TestMethod]
        public void Base()
        {
            int firstNameCounter = 0;
            int lastNameCounter = 0;
            int fullNameCounter = 0;

            Action<MainModel> firstNameHandler = m => firstNameCounter++;

            MainModel model = new MainModel();
            PropertyChangedObserver<MainModel> modelObserver = new PropertyChangedObserver<MainModel>(model);

            // 1) All observers
            modelObserver.Add(m => m.FirstName, firstNameHandler);
            modelObserver.Add(m => m.LastName, m => lastNameCounter++);
            modelObserver.Add(m => m.FullName, m => fullNameCounter++);

            model.FirstName = "John";
            model.LastName = "Doe";

            Assert.AreEqual(1, firstNameCounter);
            Assert.AreEqual(1, lastNameCounter);
            Assert.AreEqual(2, fullNameCounter);

            // 2) Remove single observer.
            modelObserver.Remove(m => m.FirstName, firstNameHandler);

            model.FirstName = String.Empty;

            Assert.AreEqual(1, firstNameCounter);
            Assert.AreEqual(1, lastNameCounter);
            Assert.AreEqual(3, fullNameCounter);

            // 3) Dispose observer.
            modelObserver.Dispose();

            model.FirstName = String.Empty;
            model.LastName = String.Empty;

            Assert.AreEqual(1, firstNameCounter);
            Assert.AreEqual(1, lastNameCounter);
            Assert.AreEqual(3, fullNameCounter);
        }
    }
}
