using Neptuo.ComponentModel.TextOffsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    /// <summary>
    /// Common extensions of <see cref="IErrorModel"/>.
    /// </summary>
    public static class _ErrorModelExtensions
    {
        /// <summary>
        /// Tries to <see cref="ILineInfo"/> from <paramref name="errorModel"/>.
        /// </summary>
        /// <param name="errorModel">Source error model.</param>
        /// <param name="lineInfo">Target output line info.</param>
        /// <returns><c>true</c> if <paramref name="errorModel"/> contains line info.</returns>
        public static bool TryWithLineInfo(this IErrorModel errorModel, out ILineInfo lineInfo)
        {
            Ensure.NotNull(errorModel, "errorModel");
            return errorModel.TryWith(out lineInfo);
        }

        /// <summary>
        /// Tries to <see cref="ILineRangeInfo"/> from <paramref name="errorModel"/>.
        /// </summary>
        /// <param name="errorModel">Source error model.</param>
        /// <param name="lineInfo">Target output line info.</param>
        /// <returns><c>true</c> if <paramref name="errorModel"/> contains line info.</returns>
        public static bool TryWithLineRangeInfo(this IErrorModel errorModel, out ILineRangeInfo lineInfo)
        {
            Ensure.NotNull(errorModel, "errorModel");
            return errorModel.TryWith(out lineInfo);
        }

        /// <summary>
        /// Tries to <see cref="IContentRangeInfo"/> from <paramref name="errorModel"/>.
        /// </summary>
        /// <param name="errorModel">Source error model.</param>
        /// <param name="contentInfo">Target output content info.</param>
        /// <returns><c>true</c> if <paramref name="errorModel"/> contains content info.</returns>
        public static bool TryWithContentInfo(this IErrorModel errorModel, out IContentRangeInfo contentInfo)
        {
            Ensure.NotNull(errorModel, "errorModel");
            return errorModel.TryWith(out contentInfo);
        }
    }
}
