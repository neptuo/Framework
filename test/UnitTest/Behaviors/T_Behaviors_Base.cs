using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptuo.Behaviors.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors
{
    [TestClass]
    public class T_Behaviors_Base
    {
        [TestMethod]
        public void AttributeBehavior()
        {
            AttributeBehaviorCollection collection = new AttributeBehaviorCollection()
                .Add<Test1Attribute, Target1, Test1Behavior>()
                .Add<Test2Attribute, Target1, Test2Behavior>();

            IEnumerable<Type> types1 = collection.GetBehaviors(typeof(Target1));
            Assert.AreEqual(2, types1.Count());

            IEnumerable<Type> types2 = collection.GetBehaviors(typeof(Target2));
            Assert.AreEqual(1, types2.Count());
        }
    }
}
