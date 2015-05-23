using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Csv.SyntaxTrees
{
    public class CommaToken : CsvNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommaToken"/> class.
        /// </summary>
        public CommaToken(TextRange textRange)
            : base(textRange)
        { }
    }
}