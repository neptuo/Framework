using Neptuo.Data.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Data.Commands
{
    public interface IProductFilter
    {
        IQuerySearch Key { set; }
        IQuerySearch Name { set; }
        IQuerySearch Category { set; }
        IQuerySearch Price { set; }
    }
}
