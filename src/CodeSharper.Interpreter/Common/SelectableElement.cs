using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class SelectableElement : IEquatable<SelectableElement>
    {
        /// <summary>
        /// Gets a value indicating whether this instance is class element.
        /// </summary>
        public Boolean IsClassElement
        {
            get { return Name != null && Name.StartsWith("."); }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        public IEnumerable<SelectorElementAttribute> Attributes { get; protected set; }

        /// <summary>
        /// Gets or sets the pseudo selectors.
        /// </summary>
        public IEnumerable<PseudoSelector> PseudoSelectors { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableElement"/> class.
        /// </summary>
        public SelectableElement(String name, IEnumerable<SelectorElementAttribute> attributes, IEnumerable<PseudoSelector> pseudoSelectors)
        {
            Assume.NotNull(name, "name");
            Assume.NotNull(attributes, "attributes");
            Assume.NotNull(pseudoSelectors, "pseudoSelectors");

            Name = name;
            Attributes = attributes;
            PseudoSelectors = pseudoSelectors;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(SelectableElement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return String.Equals(Name, other.Name) &&
                   IsClassElement == other.IsClassElement &&
                   Attributes.SequenceEqual(other.Attributes) &&
                   PseudoSelectors.SequenceEqual(other.PseudoSelectors);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as SelectableElement);
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Attributes != null ? Attributes.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (PseudoSelectors != null ? PseudoSelectors.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsClassElement.GetHashCode();
                return hashCode;
            }
        }

        #endregion
    }
}