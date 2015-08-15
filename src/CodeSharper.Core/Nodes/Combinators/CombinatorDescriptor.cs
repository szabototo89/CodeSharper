using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Nodes.Combinators
{
    public class CombinatorDescriptor : IEquatable<CombinatorDescriptor>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value { get; }

        /// <summary>
        /// Gets or sets the type of the combinator.
        /// </summary>
        public Type CombinatorType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CombinatorDescriptor"/> class.
        /// </summary>
        public CombinatorDescriptor(String name, String value, Type combinatorType)
        {
            Assume.NotNull(name, nameof(name));
            Assume.NotNull(value, nameof(value));
            Assume.NotNull(combinatorType, nameof(combinatorType));

            Name = name;
            Value = value;
            CombinatorType = combinatorType;
        }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(CombinatorDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return String.Equals(Name, other.Name) && String.Equals(Value, other.Value) && CombinatorType == other.CombinatorType;
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
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CombinatorDescriptor) obj);
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
                hashCode = (hashCode*397) ^ (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CombinatorType != null ? CombinatorType.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion
    }
}
