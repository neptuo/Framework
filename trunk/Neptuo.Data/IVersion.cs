using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    public interface IVersion
    {
        byte[] Version { get; }
    }
}
