using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class TextQuerySearch : IQuerySearch
    {
        public string Text { get; set; }

        public TextQuerySearch(string text)
        {
            Text = text;
        }

        public static TextQuerySearch Create(string text)
        {
            return new TextQuerySearch(text);
        }
    }
}
