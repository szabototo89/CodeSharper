using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Literals
{
    public class KeyValueDeclaration : JsonNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueDeclaration"/> class.
        /// </summary>
        public KeyValueDeclaration(TextRange textRange)
            : base(textRange)
        {
        }
    }
}