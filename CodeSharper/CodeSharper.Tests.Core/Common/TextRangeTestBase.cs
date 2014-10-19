using System;
using CodeSharper.Core.Texts;

namespace CodeSharper.Tests.Core.Common
{
    internal class TextRangeTestBase
    {
        protected static TextRange TextRange(String text)
        {
            return new TextDocument(text).TextRange;
        }
    }
}