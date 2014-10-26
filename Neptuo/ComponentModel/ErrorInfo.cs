using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.ComponentModel
{
    public class ErrorInfo : IErrorInfo
    {
        public int LineNumber { get; private set; }
        public int ColumnIndex { get; private set; }
        public string ErrorNumber { get; private set; }
        public string ErrorText { get; private set; }

        public ErrorInfo(int lineNumber, int columnIndex, string errorNumber, string errorText)
        {
            Guard.PositiveOrZero(lineNumber, "lineNumber");
            Guard.PositiveOrZero(columnIndex, "columnIndex");
            Guard.NotNull(errorText, "errorText");

            LineNumber = lineNumber;
            ColumnIndex = columnIndex;
            ErrorNumber = errorNumber;
            ErrorText = errorText;
        }
    }
}
