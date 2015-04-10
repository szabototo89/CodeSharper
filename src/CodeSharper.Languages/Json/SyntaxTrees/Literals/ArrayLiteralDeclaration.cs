using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Literals
{
    public class ArrayLiteralDeclaration : LiteralSyntax
    {
        /// <summary>
        /// Gets or sets elements of this type
        /// </summary>
        public IEnumerable<JsonNode> Elements { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArrayLiteralDeclaration"/> class.
        /// </summary>
        public ArrayLiteralDeclaration(IEnumerable<JsonNode> elements, TextRange textRange) : base(textRange)
        {
            Assume.NotNull(elements, "elements");

            Elements = elements.ToArray();
        }
    }
}