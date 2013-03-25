using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class MigrationAttribute : Attribute
    {
        public DateTime Timestamp { get; private set; }

        public MigrationAttribute(long timestamp)
        {
            Timestamp = new DateTime(timestamp);
        }
    }
}
