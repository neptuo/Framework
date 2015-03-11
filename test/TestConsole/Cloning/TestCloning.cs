using Neptuo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Cloning
{
    class TestCloning
    {
        public static void Test()
        {
            //TestMemberwise();
            TestGeneric();
        }

        static void TestMemberwise()
        {
            Person p1 = new Person("John", "Doe", 22);
            p1.Address = new Address("Main street", 50, "L.A.");
            Person p2 = p1.Clone();

            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine("---");

            p2.Name = "Melisa";
            p2.Address.Street = "Support street";

            Console.WriteLine(p1);
            Console.WriteLine(p2);
            Console.WriteLine("---");
        }

        static void TestGeneric()
        {
            Person p1 = new Person("John", "Doe", 22);
            p1.Address = new Address("Main street", 50, "L.A.");
            Person p2 = p1.Clone();

            ICloneable<object> cloneable = p1 as ICloneable<object>;
            if(cloneable != null)
                Console.WriteLine(cloneable.Clone());


            Console.WriteLine(p1);
            Console.WriteLine(p2);
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
