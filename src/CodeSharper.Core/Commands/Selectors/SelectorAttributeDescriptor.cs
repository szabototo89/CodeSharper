using System;
using CodeSharper.Core.Common;
using CodeSharper.Core.ErrorHandling;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Commands.Selectors
{
    public class SelectorAttributeDescriptor : IHasValue<String>, IHasName, IEquatable<SelectorAttributeDescriptor>
    {
        /// <summary>
        /// Gets or sets the name of selector attribute descriptor
        /// </summary>
        public String Name { get; protected set; }

        /// <summary>
        /// Gets or sets the value of selector attribute descriptor
        /// </summary>
        public String Value { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorAttributeDescriptor"/> class.
        /// </summary>
        public SelectorAttributeDescriptor(String name, String value)
        {
            Assume.NotNull(name, "name");

            Name = name;
            Value = value;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
        public override Boolean Equals(Object obj)
        {
            return Equals(obj as SelectorAttributeDescriptor);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(SelectorAttributeDescriptor other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(Name, other.Name) &&
                   String.Equals(Value, other.Value);
        }
    }
}