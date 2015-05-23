using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Languages.Json.SyntaxTrees.Tokens;

namespace CodeSharper.Languages.Json.SyntaxTrees.Literals
{
    public class ObjectLiteralDeclaration : LiteralSyntax, IEquatable<ObjectLiteralDeclaration>
    {
        private IEnumerable<KeyValueDeclaration> _elements;

        /// <summary>
        /// Gets or sets the elements.
        /// </summary>
        public IEnumerable<KeyValueDeclaration> Elements
        {
            get { return _elements; }
            protected set
            {
                ReplaceChildrenWith(_elements, value);
                _elements = value;
            }
        }

        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public override IEnumerable<Node> Children { get { return Elements; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectLiteralDeclaration"/> class.
        /// </summary>
        public ObjectLiteralDeclaration(IEnumerable<KeyValueDeclaration> elements, TextRange textRange)
            : base(textRange)
        {
            Assume.NotNull(elements, "elements");
            Elements = elements;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(ObjectLiteralDeclaration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Enumerable.SequenceEqual(Elements, other.Elements);
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
            return Equals((ObjectLiteralDeclaration)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            return (Elements != null ? Elements.GetHashCode() : 0);
        }

        #endregion

    }
}