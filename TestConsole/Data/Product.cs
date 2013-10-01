using Neptuo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TestConsole.Data
{
    public abstract class Product : IKey<Key>, IVersion
    {
        public abstract Key Key { get; set; }
        public abstract byte[] Version { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
