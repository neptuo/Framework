using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class TextSearch : IQuerySearch
    {
        public string Text { get; set; }
        public TextSearchType Type { get; set; }
        public bool CaseSensitive { get; set; }

        public TextSearch(string text, TextSearchType type = TextSearchType.Match, bool caseSensitive = true)
        {
            Text = text;
            Type = type;
            CaseSensitive = caseSensitive;
        }

        public static TextSearch Create(string text, TextSearchType type = TextSearchType.Match, bool caseSensitive = true)
        {
            return new TextSearch(text, type, caseSensitive);
        }
    }

    public enum TextSearchType
    {
        StartsWith,
        EndsWith,
        Contains,
        Match
    }
}
