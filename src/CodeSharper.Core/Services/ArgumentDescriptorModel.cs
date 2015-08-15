using System;
using System.Runtime.Serialization;
using CodeSharper.Core.Common;
using CodeSharper.Core.Common.Interfaces;

namespace CodeSharper.Core.Services
{
    [DataContract]
    public struct ArgumentDescriptorModel : IHasName, IEquatable<ArgumentDescriptorModel>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name", IsRequired = false)]
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        [DataMember(Name = "default-value", IsRequired = false)]
        public Object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the type of the argument.
        /// </summary>
        [DataMember(Name = "argument-type", IsRequired = false)]
        public String ArgumentType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is optional.
        /// </summary>
        [DataMember(Name = "optional", IsRequired = false)]
        public Boolean IsOptional { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [DataMember(Name = "position", IsRequired = false)]
        public Int32 Position { get; set; }

        #region Equality members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public Boolean Equals(ArgumentDescriptorModel other)
        {
            return String.Equals(Name, other.Name) && Equals(DefaultValue, other.DefaultValue) && String.Equals(ArgumentType, other.ArgumentType) && IsOptional == other.IsOptional && Position == other.Position;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ArgumentDescriptorModel && Equals((ArgumentDescriptorModel)obj);
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
                hashCode = (hashCode * 397) ^ (DefaultValue?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ArgumentType?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ IsOptional.GetHashCode();
                hashCode = (hashCode * 397) ^ Position.GetHashCode();
                return hashCode;
            }
        }

        #endregion

    }
}