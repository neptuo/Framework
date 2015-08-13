using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Localization.Readers
{
    public class EmptyTranslationReader : ITranslationReader
    {
        /// <summary>
        /// Returns instance that doesn't contain any translations.
        /// </summary>
        public static ITranslationReader Empty
        {
            get { return new EmptyTranslationReader(false); }
        }

        /// <summary>
        /// Returns instance that knows all texts and traslated text is always equal to source.
        /// </summary>
        public static ITranslationReader SourceAsTranslated
        {
            get { return new EmptyTranslationReader(true); }
        }

        private bool isOriginalEqualToTarget;

        private EmptyTranslationReader(bool isOriginalEqualToTarget)
        {
            this.isOriginalEqualToTarget = isOriginalEqualToTarget;
        }

        public bool TryGet(string originalText, out string translatedText)
        {
            if(isOriginalEqualToTarget)
            {
                translatedText = originalText;
                return true;
            }
            else
            {
                translatedText = null;
                return false;
            }
        }
    }
}
