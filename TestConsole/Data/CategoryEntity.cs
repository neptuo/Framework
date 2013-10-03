using Neptuo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data
{
    public class CategoryEntity : Category
    {
        [Key]
        public int ID
        {
            get { return Key == null ? 0 : Key.ID; }
            set { Key = new Key(value, Version, typeof(Category)); }
        }
        [Timestamp, ConcurrencyCheck]
        public override byte[] Version
        {
            get { return Key == null ? null : Key.Version; }
            set { Key = new Key(ID, value, typeof(Category)); }
        }

        [NotMapped]
        public override Key Key { get; set; }
    }
}
