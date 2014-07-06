using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.PresentationModels.Validation
{
    /// <summary>
    /// Creates instance of <typeparamref name="TValidator"/> from passed func method.
    /// </summary>
    /// <typeparam name="TValidator">Type of validator to create.</typeparam>
    public class FuncFieldMetadataValidatorFactory<TValidator> : IFieldMetadataValidatorFactory
        where TValidator : IFieldMetadataValidator
    {
        /// <summary>
        /// Function for creating instances of <typeparamref name="TValidator" />
        /// </summary>
        public Func<TValidator> Factory { get; private set; }

        /// <summary>
        /// Creaties new instance using <paramref name="factory"/> for creating instances of validator.
        /// </summary>
        /// <param name="factory">Function for creating instances of <typeparamref name="TValidator" /></param>
        public FuncFieldMetadataValidatorFactory(Func<TValidator> factory)
        {
            Guard.NotNull(factory, "factory");
            Factory = factory;
        }

        public IFieldMetadataValidator Create()
        {
            return Factory();
        }
    }

    /// <summary>
    /// Non-typed version of <see cref="FuncFieldMetadataValidatorFactory<>"/>.
    /// </summary>
    public class FuncFieldMetadataValidatorFactory : FuncFieldMetadataValidatorFactory<IFieldMetadataValidator>
    {
        public FuncFieldMetadataValidatorFactory(Func<IFieldMetadataValidator> factory)
            : base(factory)
        { }
    }
}
