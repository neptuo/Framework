using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Validators
{
    public interface IValidationMessage
    {
        string Key { get; }
        string Message { get; }
    }
}
