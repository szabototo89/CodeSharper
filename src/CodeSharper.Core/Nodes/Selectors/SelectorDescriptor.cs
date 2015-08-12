using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSharper.Core.ErrorHandling;

namespace CodeSharper.Core.Nodes.Selectors
{
    public class SelectorDescriptor : IEquatable<SelectorDescriptor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorDescriptor"/> class.
        /// </summary>
        public SelectorDescriptor(String name, String value, Type type)
        {
            Assume.NotNull(value, nameof(value));
            Assume.NotNull(name, nameof(name));
            Assume.NotNull(type, nameof(type));

            Value = value;
            Name = name;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public String Value { get; protected set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public Type Type { get; protected set; }


        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(SelectorDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return String.Equals(Value, other.Value) && String.Equals(Name, other.Name) && Equals(Type, other.Type);
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
            return Equals((SelectorDescriptor) obj);
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
                var hashCode = (Value != null ? Value.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Type != null ? Type.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

    }
}
