using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public static class CsvTokens
    {
        public static CommaToken Comma(TextRange range)
        {
            Assume.NotNull(range, "range");
            return new CommaToken(range);
        }
    }
}