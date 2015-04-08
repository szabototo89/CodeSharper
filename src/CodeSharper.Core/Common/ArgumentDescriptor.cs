using System;
using CodeSharper.Core.Utilities;

namespace CodeSharper.Core.Common
{
    public class ArgumentDescriptor : IEquatable<ArgumentDescriptor>
    {
        /// <summary>
        /// Gets or sets a value indicating whether this argument is optional.
        /// </summary>
        public Boolean IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the default value of argument
        /// </summary>
        public Object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the type of the argument.
        /// </summary>
        public Type ArgumentType { get; set; }

        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        public String ArgumentName { get; set; }

        #region Equality members

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
                var hashCode = IsOptional.GetHashCode();
                hashCode = (hashCode * 397) ^ (DefaultValue != null ? DefaultValue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ArgumentType != null ? ArgumentType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ArgumentName != null ? ArgumentName.GetHashCode() : 0);
                return hashCode;
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
            return Equals(obj as ArgumentDescriptor);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public Boolean Equals(ArgumentDescriptor other)
        {
            return EqualityHelper.IsNullOrReferenceEqual(other, this) ??
                   String.Equals(ArgumentName, other.ArgumentName) &&
                   Equals(DefaultValue, other.DefaultValue) &&
                   IsOptional == other.IsOptional &&
                   ArgumentType == other.ArgumentType;
        }

        #endregion

    }
}