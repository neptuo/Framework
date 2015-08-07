using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Errors
{
    /// <summary>
    /// Describes error info.
    /// </summary>
    public interface IErrorInfo
    {
        /// <summary>
        /// Line where error occured.
        /// </summary>
        int LineNumber { get; }

        /// <summary>
        /// Column where error occured.
        /// </summary>
        int ColumnIndex { get; }

        /// <summary>
        /// Error index.
        /// </summary>
        string ErrorNumber { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        string ErrorText { get; }
    }
}
