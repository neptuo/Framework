using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Collections.ObjectModel
{
    [Obsolete]
    public interface IITemsSource : IEnumerable
    {
        int Count { get; }
        void RemoveAt(int index);
    }
}
