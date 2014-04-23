using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    [Flags]
    public enum DateTimeSearchType
    {
        Before = 1, 
        Exactly = 2, 
        After = 4
    }
}
