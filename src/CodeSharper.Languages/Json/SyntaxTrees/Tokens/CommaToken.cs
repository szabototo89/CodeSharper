using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Tokens
{
    public class CommaToken : JsonSyntaxToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommaToken"/> class.
        /// </summary>
        public CommaToken(TextRange textRange)
            : base(textRange)
        {
        }
    }
}