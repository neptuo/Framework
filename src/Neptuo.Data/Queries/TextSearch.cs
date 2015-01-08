using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data.Queries
{
    public class TextSearch : IQuerySearch
    {
        public string Text { get; protected set; }
        public TextSearchType Type { get; protected set; }
        public bool CaseSensitive { get; protected set; }

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

        public static TextSearch Match(string text, bool caseSensitive = true)
        {
            return new TextSearch(text, TextSearchType.Match, caseSensitive);
        }

        public static TextSearch Contains(string text, bool caseSensitive = true)
        {
            return new TextSearch(text, TextSearchType.Contains, caseSensitive);
        }

        public static TextSearch StartsWith(string text, bool caseSensitive = true)
        {
            return new TextSearch(text, TextSearchType.StartsWith, caseSensitive);
        }

        public static TextSearch EndsWith(string text, bool caseSensitive = true)
        {
            return new TextSearch(text, TextSearchType.EndsWith, caseSensitive);
        }
    }
}
