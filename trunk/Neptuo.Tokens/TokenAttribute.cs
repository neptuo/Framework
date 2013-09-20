using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    public class TokenAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public TokenAttribute(string name, string value = null)
        {
            Name = name;
            Value = value;
        }
    }
}
