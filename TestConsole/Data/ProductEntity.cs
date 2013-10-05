using Neptuo.Data;
using Neptuo.Linq;
using Neptuo.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data
{
    public class ProductEntity : Product
    {
        private static readonly CompiledExpression<ProductEntity, Key> keyExpression
            = DefaultTranslationOf<ProductEntity>.Property(p => p.Key, p => new Key { ID = (p as ProductEntity).ID, Version = (p as ProductEntity).Version });

        [Key]
        public int ID
        {
            get { return Key == null ? 0 : Key.ID; }
            set { Key = new Key(value, Version, typeof(Product)); }
        }
        [Timestamp, ConcurrencyCheck]
        public override byte[] Version
        {
            get { return Key == null ? null : Key.Version; }
            set { Key = new Key(ID, value, typeof(Product)); }
        }

        [NotMapped]
        public override Key Key { get; set; }

        private CategoryEntity categoryEntity;

        [NotMapped]
        public override Category Category
        {
            get { return CategoryEntity; }
            set { CategoryEntity = (CategoryEntity)value; }
        }

        public CategoryEntity CategoryEntity
        {
            get { return categoryEntity; }
            set
            {
                categoryEntity = (CategoryEntity)value;
                if (value != null)
                    CategoryID = ((CategoryEntity)value).ID;
                else
                    CategoryID = null;
            }
        }

        [ForeignKey("CategoryEntity")]
        public int? CategoryID { get; set; }
    }
}
