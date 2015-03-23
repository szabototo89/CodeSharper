using System.Collections;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees
{
    public abstract class LiteralSyntax : JsonNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteralSyntax"/> class.
        /// </summary>
        protected LiteralSyntax(TextRange textRange) : base(textRange)
        {
        }
    }
}