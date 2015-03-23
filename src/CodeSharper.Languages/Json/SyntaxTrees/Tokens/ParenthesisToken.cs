using System;
using CodeSharper.Core.Texts;

namespace CodeSharper.Languages.Json.SyntaxTrees.Tokens
{
    public abstract class ParenthesisToken : JsonSyntaxToken
    {
        /// <summary>
        /// Gets or sets the type of the parenthesis.
        /// </summary>
        public ParenthesisType ParenthesisType { get; protected set; }

        /// <summary>
        /// Gets a value indicating whether this instance is left.
        /// </summary>
        public Boolean IsLeft { get { return ParenthesisType.HasFlag(ParenthesisType.Left); } }

        /// <summary>
        /// Gets a value indicating whether this instance is right.
        /// </summary>
        public Boolean IsRight { get { return ParenthesisType.HasFlag(ParenthesisType.Right); } }

        /// <summary>
        /// Gets a value indicating whether this instance is array type.
        /// </summary>
        public Boolean IsArrayType { get { return ParenthesisType.HasFlag(ParenthesisType.ArrayType); } }

        /// <summary>
        /// Gets a value indicating whether this instance is object type.
        /// </summary>
        public Boolean IsObjectType { get { return ParenthesisType.HasFlag(ParenthesisType.ObjectType); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParenthesisToken"/> class.
        /// </summary>
        protected ParenthesisToken(ParenthesisType parenthesisType, TextRange textRange) : base(textRange)
        {
            ParenthesisType = parenthesisType;
        }
    }
}