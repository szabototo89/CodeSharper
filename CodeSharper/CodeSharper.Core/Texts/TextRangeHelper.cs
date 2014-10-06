using System;

namespace CodeSharper.Core.Texts
{
    public static partial class TextRangeHelper
    {
        public static TextRange SubStringOfText(this TextRange textRange, Int32 start)
        {
            return textRange.SubStringOfText(start, textRange.Text.Length);
        }
    }
}