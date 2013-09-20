using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neptuo.Tokens
{
    public class Token
    {
        public string Prefix { get; set; }
        public string Name { get; set; }

        public string Fullname
        {
            get
            {
                if (String.IsNullOrEmpty(Prefix))
                    return Name;

                return Prefix + ":" + Name;
            }
            set
            {
                int index = value.IndexOf(':');
                if (index != -1)
                {
                    Prefix = value.Substring(0, index);
                    Name = value.Substring(index + 1);
                }
                else
                {
                    Name = value;
                }
            }
        }

        public List<TokenAttribute> Attributes { get; set; }
        public string DefaultAttributeValue { get; set; }

        public Token()
        {
            Attributes = new List<TokenAttribute>();
        }
    }
}
