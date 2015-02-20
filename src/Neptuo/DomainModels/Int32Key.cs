using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.DomainModels
{
    public class Int32Key : KeyBase
    {
        public static Int32Key Create(int id, string type)
        {
            Guard.Positive(id, "id");
            Guard.NotNullOrEmpty(type, "type");
            return new Int32Key(id, type);
        }

        public static Int32Key Empty(string type)
        {
            Guard.NotNullOrEmpty(type, "type");
            return new Int32Key(type);
        }

        public int ID { get; private set; }

        protected Int32Key(string type)
            : base(type, true)
        { }

        protected Int32Key(int id, string type)
            : base(type, false)
        {
            ID = id;
        }

        protected override bool Equals(KeyBase other)
        {
            Int32Key key;
            if (Converts.Try<IKey, Int32Key>(other, out key))
                return false;

            return ID == key.ID;
        }

        protected override int CompareValueTo(KeyBase other)
        {
            Int32Key key;
            if (Converts.Try<IKey, Int32Key>(other, out key))
                return 1;

            return ID.CompareTo(key.ID);
        }

        protected override int GetValueHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
