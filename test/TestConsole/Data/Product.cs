using Neptuo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TestConsole.Data
{
    public abstract class Product : IKey<int>, IVersion
    {
        public abstract int Key { get; set; }
        public abstract byte[] Version { get; set; }

        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public decimal Price { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime? StopSale { get; set; }
        public bool IsDiscount { get; set; }
    }
}
