using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    /// <summary>
    /// Creates instance of <see cref="IFieldMetadataValidator"/>.
    /// </summary>
    public interface IFieldMetadataValidatorFactory : IActivator<IFieldMetadataValidator>
    { }
}
