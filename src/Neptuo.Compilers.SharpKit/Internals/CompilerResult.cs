﻿using Neptuo.Compilers.Errors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Internals
{
    /// <summary>
    /// Internal implementation of <see cref="ICompilerResult"/>.
    /// </summary>
    internal class CompilerResult : ICompilerResult
    {
        public bool IsSuccess { get; private set; }
        public IEnumerable<IErrorInfo> Errors { get; private set; }
        public StringCollection Output { get; private set; }

        public CompilerResult()
        {
            IsSuccess = true;
            Errors = Enumerable.Empty<IErrorInfo>();
        }

        /// <summary>
        /// Creates success instance.
        /// </summary>
        /// <param name="output">Raw compiler output.</param>
        public CompilerResult(StringCollection output)
            : this(Enumerable.Empty<IErrorInfo>(), output)
        { }

        /// <summary>
        /// If <paramref name="errors"/> is empty, than success. Otherwise not-success.
        /// </summary>
        /// <param name="errors">Enumeration of compilation errors.</param>
        /// <param name="output">Raw compiler output.</param>
        public CompilerResult(IEnumerable<IErrorInfo> errors, StringCollection output)
        {
            Ensure.NotNull(errors, "errors");
            Ensure.NotNull(output, "output");
            Errors = new List<IErrorInfo>(errors);
            IsSuccess = !Errors.Any();
            Output = output;
        }
    }
}
