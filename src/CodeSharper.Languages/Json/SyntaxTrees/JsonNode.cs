using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees
{
    public abstract class JsonNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonNode"/> class.
        /// </summary>
        protected JsonNode(TextRange textRange)
            : base(textRange)
        {
        }
    }
}