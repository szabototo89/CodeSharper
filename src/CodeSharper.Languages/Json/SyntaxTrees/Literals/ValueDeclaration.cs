using System;
using System.Collections.Generic;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Json.SyntaxTrees.Constants;

namespace CodeSharper.Languages.Json.SyntaxTrees.Literals
{
    public class ValueDeclaration : JsonNode, IEquatable<ValueDeclaration>
    {
        private JsonNode _value;

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public JsonNode Value
        {
            get { return _value; }
            protected set
            {
                ReplaceChildWith(_value, value);
                _value = value;
            }
        }

        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public override IEnumerable<Node> Children { get { return new[] { Value }; } }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is constant.
        /// </summary>
        public Boolean IsConstant
        {
            get { return Value is ConstantSyntax; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is literal.
        /// </summary>
        public Boolean IsLiteral
        {
            get { return Value is LiteralSyntax; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueDeclaration"/> class.
        /// </summary>
        public ValueDeclaration(ConstantSyntax value, TextRange textRange)
            : base(textRange)
        {
            Assume.NotNull(value, "Value");
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueDeclaration"/> class.
        /// </summary>
        public ValueDeclaration(LiteralSyntax value, TextRange textRange)
            : base(textRange)
        {
            Assume.NotNull(value, "value");

            Value = value;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(ValueDeclaration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Value, other.Value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ValueDeclaration)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        #endregion

    }
}