using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Interpreter.Common
{
    public struct TypeSelectorElement : IEquatable<TypeSelectorElement>, IHasName
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        public IEnumerable<AttributeElement> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the pseudo selectors.
        /// </summary>
        public IEnumerable<ModifierElement> Modifiers { get; set; }

        /// <summary>
        /// Gets or sets the class selectors.
        /// </summary>
        public IEnumerable<ClassSelectorElement> ClassSelectors { get; set; }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(TypeSelectorElement other)
        {
            return String.Equals(Name, other.Name) && Equals(Attributes, other.Attributes) && Equals(Modifiers, other.Modifiers) && Equals(ClassSelectors, other.ClassSelectors);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is TypeSelectorElement && Equals((TypeSelectorElement) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (Attributes?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Modifiers?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ClassSelectors?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        #endregion
    }
}