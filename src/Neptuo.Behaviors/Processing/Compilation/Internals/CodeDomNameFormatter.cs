using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation.Internals
{
    /// <summary>
    /// Formats names for generated pipeline.
    /// </summary>
    internal class CodeDomNameFormatter
    {
        private readonly Type handlerType;

        public CodeDomNameFormatter(Type handlerType)
        {
            Ensure.NotNull(handlerType, "handlerType");
            this.handlerType = handlerType;
        }

        /// <summary>
        /// Formats pipeline type based on name of handler type.
        /// </summary>
        /// <returns>Pipeline name.</returns>
        public string FormatPipelineTypeName()
        {
            return String.Format("{0}Pipeline", handlerType.Name);
        }

        /// <summary>
        /// Formats name for generated assembly (only file name with extension).
        /// </summary>
        /// <returns>Name for generated assembly.</returns>
        public string FormatAssemblyFileName()
        {
            return String.Format("{0}.dll", handlerType.FullName);
        }

        /// <summary>
        /// Formats name for generated source code (only file name with extension).
        /// </summary>
        /// <returns>Name for generated assesource codembly.</returns>
        public string FormatSourceCodeFileName()
        {
            return String.Format("{0}.cs", handlerType.FullName);
        }
    }
}
