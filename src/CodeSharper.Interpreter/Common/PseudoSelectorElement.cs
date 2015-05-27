using System;
using System.Collections.Generic;
using System.Linq;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;
using CodeSharper.Interpreter.Visitors;

namespace CodeSharper.Interpreter.Common
{
    public struct PseudoSelectorElement : IEquatable<PseudoSelectorElement>, IHasName
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public IEnumerable<ConstantElement> Arguments { get; set; }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(PseudoSelectorElement other)
        {
            return String.Equals(Name, other.Name) && 
                   Arguments != null &&
                   Enumerable.SequenceEqual(Arguments, other.Arguments);
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
            if (!(obj is PseudoSelectorElement)) return false;
            return Equals(((PseudoSelectorElement)obj));
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
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Arguments != null ? Arguments.GetHashCode() : 0);
            }
        }

        #endregion

    }
}