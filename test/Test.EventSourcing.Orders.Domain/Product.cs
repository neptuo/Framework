using Neptuo;
using Neptuo.Events;
using Neptuo.Models;
using Neptuo.Models.Keys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Events;

namespace Orders
{
    public class Product : AggregateRoot
    {
        public string Name { get; private set; }

        public Product(string name)
        {
            Publish(new ProductCreated(name));
        }

        public Product(IKey key, IEnumerable<IEvent> events) 
            : base(key, events)
        { }

        private void Handle(ProductCreated payload)
        {
            Name = payload.Name;
        }

        public void ChangeName(string newName)
        {
            Ensure.NotNullOrEmpty(newName, "newName");
            Publish(new ProductNameChanged(newName));
        }

        private void Handle(ProductNameChanged payload)
        {
            Name = payload.Name;
        }
    }
}
