using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Constants
{
    public abstract class ConstantSyntax : JsonNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantSyntax"/> class.
        /// </summary>
        protected ConstantSyntax(TextRange textRange) : base(textRange) { }
    }
}
