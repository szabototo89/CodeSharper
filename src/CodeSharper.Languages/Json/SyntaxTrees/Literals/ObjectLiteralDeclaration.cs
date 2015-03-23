using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Json.SyntaxTrees.Tokens;

namespace CodeSharper.Languages.Json.SyntaxTrees
{
    public class ObjectLiteralDeclaration : LiteralSyntax
    {
        /// <summary>
        /// Gets or sets the left parenthesis.
        /// </summary>
        public ParenthesisToken LeftParenthesis { get; protected set; }

        /// <summary>
        /// Gets or sets the right parenthesis.
        /// </summary>
        public ParenthesisToken RightParenthesis { get; protected set; }

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public IEnumerable<KeyValueDeclaration> Elements { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectLiteralDeclaration"/> class.
        /// </summary>
        public ObjectLiteralDeclaration(ParenthesisToken leftParenthesis, ParenthesisToken rightParenthesis, IEnumerable<KeyValueDeclaration> elements, TextRange textRange)
            : base(textRange)
        {
            Assume.NotNull(elements, "elements");
            Assume.NotNull(leftParenthesis, "leftParenthesis");
            Assume.NotNull(rightParenthesis, "rightParenthesis");

            LeftParenthesis = leftParenthesis;
            RightParenthesis = rightParenthesis;
            Elements = elements;
        }
    }
}