using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.FileSystems.Features.Searching
{
    /// <summary>
    /// Describes text searching options.
    /// </summary>
    public class TextSearch
    {
        /// <summary>
        /// Searched text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Search type.
        /// </summary>
        public TextSearchType Type { get; private set; }

        /// <summary>
        /// Whether is search case sensitive.
        /// </summary>
        public bool IsCaseSensitive { get; private set; }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="text">Searched text.</param>
        /// <param name="type">Search type.</param>
        /// <param name="isCaseSensitive">Whether is search case sensitive.</param>
        /// <returns>Instance of <see cref="TextSearch"/>.</returns>
        public static TextSearch Create(string text, TextSearchType type, bool isCaseSensitive)
        {
            return new TextSearch
            {
                Text = text,
                Type = type,
                IsCaseSensitive = isCaseSensitive
            };
        }

        /// <summary>
        /// Creates new instance that matches all items.
        /// </summary>
        /// <returns></returns>
        public static TextSearch CreateEmpty()
        {
            return Create(null, TextSearchType.Contained, true);
        }

        /// <summary>
        /// Cretes new instance that uses prefix search.
        /// </summary>
        /// <param name="text">Searched text.</param>
        /// <param name="isCaseSensitive">Whether is search case sensitive.</param>
        /// <returns>Instance of <see cref="TextSearch"/>.</returns>
        public static TextSearch CreatePrefixed(string text, bool isCaseSensitive = true)
        {
            return Create(text, TextSearchType.Prefixed, isCaseSensitive);
        }

        /// <summary>
        /// Cretes new instance that uses suffix search.
        /// </summary>
        /// <param name="text">Searched text.</param>
        /// <param name="isCaseSensitive">Whether is search case sensitive.</param>
        /// <returns>Instance of <see cref="TextSearch"/>.</returns>
        public static TextSearch CreateSuffixed(string text, bool isCaseSensitive = true)
        {
            return Create(text, TextSearchType.Suffixed, isCaseSensitive);
        }

        /// <summary>
        /// Cretes new instance that uses match search.
        /// </summary>
        /// <param name="text">Searched text.</param>
        /// <param name="isCaseSensitive">Whether is search case sensitive.</param>
        /// <returns>Instance of <see cref="TextSearch"/>.</returns>
        public static TextSearch CreateMatched(string text, bool isCaseSensitive = true)
        {
            return Create(text, TextSearchType.Matched, isCaseSensitive);
        }

        /// <summary>
        /// Cretes new instance that uses contains search.
        /// </summary>
        /// <param name="text">Searched text.</param>
        /// <param name="isCaseSensitive">Whether is search case sensitive.</param>
        /// <returns>Instance of <see cref="TextSearch"/>.</returns>
        public static TextSearch CreateContained(string text, bool isCaseSensitive = true)
        {
            return Create(text, TextSearchType.Contained, isCaseSensitive);
        }
    }
}
