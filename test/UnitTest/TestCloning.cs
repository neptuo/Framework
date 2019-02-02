using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    [TestClass]
    public class TestCloning
    {
        [TestMethod]
        public void Memberwise()
        {
            Person p1 = new Person("John", "Doe", 22);
            p1.Address = new Address("Main street", 50, "L.A.");
            Person p2 = p1.Clone();

            AssertEqual(p1, p2);
        }

        [TestMethod]
        public void Generic()
        {
            Person p1 = new Person("John", "Doe", 22);
            p1.Address = new Address("Main street", 50, "L.A.");
            Person p2 = p1.Clone();

            ICloneable<object> cloneable = p1 as ICloneable<object>;
            Assert.IsNotNull(cloneable);

            AssertEqual(p1, p2);
        }

        private void AssertEqual(Person p1, Person p2)
        {
            Assert.AreEqual(p1.Name, p2.Name);
            Assert.AreEqual(p1.Surname, p2.Surname);
            Assert.AreEqual(p1.GetAge(), p2.GetAge());
            
            if (p1.Address != null)
            {
                if (p2.Address == null)
                    Assert.Fail("p1 has adress, p2 is missing address.");

                Assert.AreEqual(p1.Address.Street, p2.Address.Street);
                Assert.AreEqual(p1.Address.City, p2.Address.City);
                Assert.AreEqual(p1.Address.HouseNumber, p2.Address.HouseNumber);
            }
        }


        class Person : ICloneable<Person>, ICloneable<object>
        {
            private int age;

            public string Name { get; set; }
            public string Surname { get; set; }
            public Address Address { get; set; }

            public Person(string name, string surname, int age)
            {
                Name = name;
                Surname = surname;
                this.age = age;
            }

            public int GetAge() => age;

            public Person Clone()
            {
                return (Person)MemberwiseClone();
            }

            public override string ToString()
            {
                return String.Format("{0} {1} ({2}) [{3}]", Name, Surname, age, Address);
            }

            object ICloneable<object>.Clone()
            {
                return (Person)MemberwiseClone();
            }
        }

        class Address
        {
            public string Street { get; set; }
            public int HouseNumber { get; set; }
            public string City { get; set; }

            public Address(string street, int houseNumber, string city)
            {
                Street = street;
                HouseNumber = houseNumber;
                City = city;
            }

            public override string ToString()
            {
                return String.Format("{0} {1}, {2}", Street, HouseNumber, City);
            }
        }
    }
}
