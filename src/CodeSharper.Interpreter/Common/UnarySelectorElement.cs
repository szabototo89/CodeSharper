using System;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public class UnarySelectorElement : SelectorElementBase, IEquatable<UnarySelectorElement>
    {
        /// <summary>
        /// Gets or sets the element.
        /// </summary>
        public ElementTypeSelector ElementTypeSelector { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnarySelectorElement"/> class.
        /// </summary>
        public UnarySelectorElement(ElementTypeSelector elementTypeSelector)
        {
            ElementTypeSelector = elementTypeSelector;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(UnarySelectorElement other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(ElementTypeSelector, other.ElementTypeSelector);
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
            return Equals(obj as UnarySelectorElement);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            return ElementTypeSelector.GetHashCode();
        }

        #endregion

    }
}