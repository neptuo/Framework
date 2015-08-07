using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Compilers.Errors
{
    public class ErrorInfo : IErrorInfo
    {
        public int LineNumber { get; private set; }
        public int ColumnIndex { get; private set; }
        public string ErrorNumber { get; private set; }
        public string ErrorText { get; private set; }

        public ErrorInfo(int lineNumber, int columnIndex, string errorNumber, string errorText)
        {
            Ensure.PositiveOrZero(lineNumber, "lineNumber");
            Ensure.PositiveOrZero(columnIndex, "columnIndex");
            Ensure.NotNull(errorText, "errorText");

            LineNumber = lineNumber;
            ColumnIndex = columnIndex;
            ErrorNumber = errorNumber;
            ErrorText = errorText;
        }

        public ErrorInfo(int lineNumber, int columnIndex, string errorText)
            : this(lineNumber, columnIndex, null, errorText)
        { }
    }
}
