using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Core.Utilities
{
    public static class TextRangeExtensions 
    {
        /// <summary>
        /// Changes the text of the specified text range
        /// </summary>
        public static TextRange ChangeText(this TextRange textRange, String replacedText)
        {
            Assume.NotNull(textRange,"textRange");
            Assume.NotNull(textRange.TextDocument, "textRange.TextDocument");

            textRange.TextDocument.ChangeText(textRange, replacedText);
            return textRange;
        }

        /// <summary>
        /// Gets the text of the specified text range
        /// </summary>
        public static String GetText(this TextRange textRange)
        {
            Assume.NotNull(textRange, "textRange");
            Assume.NotNull(textRange.TextDocument, "textRange.TextDocument");
         
            return textRange.TextDocument.GetText(textRange);
        }
    }
}
