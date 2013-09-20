using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    public class ExtensionEventArgs : EventArgs
    {
        public int StartPosition { get; set; }

        public int EndPosition { get; set; }

        public string OriginalContent { get; set; }

        public Token Extension { get; set; }
    }
}
