using Neptuo.Exceptions.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Behaviors.Processing.Compilation
{
    internal static class _EnsureExceptionExtensions
    {
        public static PipelineFactoryException UnCompilableSource(this EnsureExceptionHelper helper, string sourceCodePath)
        {
            return new PipelineFactoryException(String.Format("Error during compilation of generated pipeline, source code saved to '{0}'.", sourceCodePath));
        }
    }
}
