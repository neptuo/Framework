using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Constraints
{
    public interface IBootstrapConstraint
    {
        bool Satisfies();
    }
}
