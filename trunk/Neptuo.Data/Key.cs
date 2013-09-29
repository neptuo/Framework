using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    public class Key
    {
        public int ID { get; protected set; }
        public int Version { get; protected set; }
        public string EntityUri { get; protected set; }

        public Key(int id, int version, string entityUri = null)
        {
            ID = id;
            Version = version;
            EntityUri = entityUri;
        }
    }
}
