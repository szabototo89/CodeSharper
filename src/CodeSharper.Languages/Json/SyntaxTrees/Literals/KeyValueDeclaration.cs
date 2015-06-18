using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.SyntaxTrees;
using CodeSharper.Core.Texts;
using CodeSharper.Core.Transformation;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Languages.Json.SyntaxTrees.Literals
{
    public class KeyValueDeclaration : JsonNode, IEquatable<KeyValueDeclaration>, ICanRemove
    {
        private readonly Node[] children;

        private KeyDeclaration key;
        private ValueDeclaration value;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public KeyDeclaration Key
        {
            get { return key; }
            protected set
            {
                ReplaceChildWith(key, value);
                key = value;

                updateChildren(key, this.value);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public ValueDeclaration Value
        {
            get { return value; }
            protected set
            {
                ReplaceChildWith(this.value, value);
                this.value = value;

                updateChildren(key, this.value);
            }
        }

        /// <summary>
        /// Gets or sets children of this type
        /// </summary>
        public override IEnumerable<Node> Children
        {
            get { return children; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueDeclaration"/> class.
        /// </summary>
        public KeyValueDeclaration(KeyDeclaration key, ValueDeclaration value, TextRange textRange)
            : base(textRange)
        {
            Assume.NotNull(key, "key");
            Assume.NotNull(value, "value");

            this.key = key;
            this.value = value;

            children = new Node[2];
            updateChildren(key, value);
        }

        private void updateChildren(KeyDeclaration keyDeclaration, ValueDeclaration valueDeclaration)
        {
            children[0] = keyDeclaration;
            children[1] = valueDeclaration;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(KeyValueDeclaration other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Key, other.Key) && Equals(Value, other.Value);
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
            return Equals((KeyValueDeclaration) obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        /// <summary>
        /// Removes this instance.
        /// </summary>
        public Boolean Remove()
        {
            var objectLiteral = Parent as ObjectLiteralDeclaration;
            if (objectLiteral == null) return false;

            var declarations = objectLiteral.Elements.ToArray();
            objectLiteral.Elements = declarations.Where(declaration => !ReferenceEquals(declaration, this));

            TextRange.ChangeText("");

            return true;
        }

        #endregion
    }
}