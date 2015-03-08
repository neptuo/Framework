using Neptuo.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data
{
    public class CategoryRepository : MappingEntityRepository<Category, CategoryEntity, DataContext>, ICategoryRepository
    {
        public CategoryRepository(DataContext dbContext)
            : base(dbContext)
        { }

        public Category Create()
        {
            return new CategoryEntity();
        }
    }
}
