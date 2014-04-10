using Neptuo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data
{
    public abstract class Category : IKey<int>, IVersion
    {
        public abstract int Key { get; set; }
        public abstract byte[] Version { get; set; }

        public string Name { get; set; }
    }
}
