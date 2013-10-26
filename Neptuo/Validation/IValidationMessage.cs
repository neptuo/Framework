using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validation
{
    public interface IValidationMessage
    {
        string Key { get; }
        string Message { get; }
    }
}
