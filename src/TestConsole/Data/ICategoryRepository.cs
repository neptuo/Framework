using Neptuo;
using Neptuo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data
{
    public interface ICategoryRepository : IRepository<Category>, IActivator<Category>
    { }
}
