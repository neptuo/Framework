using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo
{
    public class SequenceGuidProvider : IGuidProvider
    {
        private string prefix;
        private int offset;

        public SequenceGuidProvider(string prefix = null, int offset = 0)
        {
            this.prefix = prefix;
            this.offset = offset;
        }

        public string Next()
        {
            return prefix + (++offset);
        }
    }
}
