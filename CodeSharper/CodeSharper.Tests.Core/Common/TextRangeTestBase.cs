using System;
using CodeSharper.Core.Texts;
using CodeSharper.Tests.Core.TestHelpers;

namespace CodeSharper.Tests.Core.Common
{
    internal class TextRangeTestBase : TestFixtureBase
    {
        protected static TextRange TextRange(String text)
        {
            return new TextDocument(text).TextRange;
        }
    }
}