using Neptuo.Bootstrap.Hierarchies.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Bootstrap.Hierarchies
{
    /// <summary>
    /// Builder for <see cref="BootstrapperS"/>.
    /// </summary>
    public class BootstrapperBuilder
    {
        private ISortInputProvider inputSorter;
        private ISortOutputProvider outputSorter;
        private List<Type> defaultDependencies;

        public BootstrapperBuilder AddInputSorter(ISortInputProvider inputSorter)
        {
            Ensure.NotNull(inputSorter, "inputSorter");
            this.inputSorter = inputSorter;
            return this;
        }

        public BootstrapperBuilder AddOutputSorter(ISortOutputProvider outputSorter)
        {
            Ensure.NotNull(outputSorter, "outputSorter");
            this.outputSorter = outputSorter;
            return this;
        }

        /// <summary>
        /// Adds <paramref name="type"/> to be known dependency by default.
        /// </summary>
        /// <param name="type">Known dependency.</param>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddDefaultDependency(Type type)
        {
            Ensure.NotNull(type, "type");
            if (defaultDependencies == null)
                defaultDependencies = new List<Type>();

            defaultDependencies.Add(type);
            return this;
        }

        /// <summary>
        /// Adds <typeparamref name="T" /> to be known dependency by default.
        /// </summary>
        /// <typeparam name="T">Type of known dependency.</typeparam>
        /// <returns>Self (for fluency).</returns>
        public BootstrapperBuilder AddDefaultDependency<T>()
        {
            return AddDefaultDependency(typeof(T));
        }

        public Bootstrapper ToBootstrapper()
        {
            return new Bootstrapper(
                inputSorter ?? new PropertyImportExportProvider(),
                outputSorter ?? new PropertyImportExportProvider(),
                defaultDependencies
            );
        }
    }
}
